using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data.Common;
using System.Data;
using System.Xml;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch 14.02.2012 01:16
    /// Concrete database implementation for PostGres database.
    /// </summary>
    public class PgDb : Db
    {
        #region Private Fields

        private NpgsqlConnection connection;

        private readonly string user;
        private readonly string password;
        private readonly string server;
        private readonly int port;
        private readonly string database;

        #endregion

        private PgDb(DbConfig config)
        {
            Config = config;

            this.user = config.User;
            this.password = config.Password;
            this.server = config.ServerName;
            this.database = config.DatabaseName;
            this.port = config.Port;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PgDb"/> class from being created.
        /// Use <see cref="PgDb.Create"/> instead for instansiation.
        /// </summary>
        /// <param name="user">The database user.</param>
        /// <param name="password">The database password.</param>
        /// <param name="port">The communication port.</param>
        /// <param name="server">The host name or IP address of the database server.</param>
        /// <param name="database">The database name.</param>
        private PgDb(string user, string password, string server, int port, string database)
        {
            this.user = user;
            this.password = password;
            this.server = server;
            this.port = port;
            this.database = database;
        }

        #region Instance Creation

        /// <summary>
        /// Creates the <see cref="PgDb"/> from config file in file path.
        /// </summary>
        /// <param name="configPath">The config file path.</param>
        /// <returns>The initialized <see cref="PgDb"/> instance.</returns>
        public static PgDb Create(string configPath)
        {
            DbConfig config = new DbConfig(configPath);
            return new PgDb(config);
        }

        /// <summary>
        /// Creates the <see cref="PgDb"/> instance with specified settings.
        /// The database server is supposed to be running on local machine for correct initialization.
        /// </summary>
        /// <param name="name">The database name.</param>
        /// <param name="user">The database user.</param>
        /// <param name="password">The database password.</param>
        /// <param name="port">The communication port.</param>
        /// <returns>The initialized <see cref="PgDb"/> instance.</returns>
        public static PgDb Create(string name, string user, string password, int port)
        {
            return Create("localhost", name, user, password, port);
        }

        /// <summary>
        /// Creates the <see cref="PgDb"/> instance with specified settings.
        /// </summary>
        /// <param name="name">The database name.</param>
        /// <param name="server">The host name or IP address of the database server.</param>
        /// <param name="user">The database user.</param>
        /// <param name="password">The database password.</param>
        /// <param name="port">The communication port.</param>
        /// <returns>
        /// The initialized <see cref="PgDb"/> instance.
        /// </returns>
        public static PgDb Create(string server, string name, string user, string password, int port)
        {
            return new PgDb(user, password, server, port, name);
        }

        #endregion

        #region Method overrides

        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        public override void OpenConnection()
        {
            connection = new NpgsqlConnection(BuildConnectionString());
            connection.Open();
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public override void CloseConnection()
        {
            connection.Close();
        }

        /// <summary>
        /// Begins the transaction on the database.
        /// </summary>
        /// <returns>
        /// The corresponding <see cref="DbTransaction"/> instance.
        /// </returns>
        public override DbTransaction BeginTransaction()
        {
            return connection.BeginTransaction(IsolationLevel.Serializable);
        }

        /// <summary>
        /// Commits the specified <see cref="DbTransaction"/>.
        /// </summary>
        /// <param name="ta">The <see cref="DbTransaction"/> to be committed.</param>
        public override void Commit(DbTransaction ta)
        {
            ta.Commit();
        }

        /// <summary>
        /// Performs a rollback of specified <see cref="DbTransaction"/>.
        /// </summary>
        /// <param name="ta">The <see cref="DbTransaction"/> to be rolled back.</param>
        public override void Rollback(DbTransaction ta)
        {
            ta.Rollback();
        }

        /// <summary>
        /// Returns a <see cref="DbDataReader"/> object that allows iterating over all affected result records.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <returns>The <see cref="DbDataReader"/> for iteration over affected result set.</returns>
        public override DbDataReader ExecuteReader(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            return command.ExecuteReader();
        }

        /// <summary>
        /// Executes a query with a result (e.g. from an aggregation function such as COUNT, MAX, MIN).
        /// The result represents the first column of the first row of the result.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <returns>The result as <see cref="object"/>.</returns>
        public override object ExecuteScalar(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            return command.ExecuteScalar();
        }

        /// <summary>
        /// Executes a non-query statement (e.g. INSERT, UPDATE, DELETE statements) and returns the number
        /// of rows affected by the statement.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <returns>The number of rows that were affected by the statement.</returns>
        public override int ExecuteNonQuery(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            return command.ExecuteNonQuery();
        }


        /// <summary>
        /// Builds the connection string for specific database implementation.
        /// </summary>
        /// <returns>
        /// The correct connection string for associated database.
        /// </returns>
        protected override string BuildConnectionString()
        {
            return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};",
                                    server, port, database, user, password);
        }

        #endregion

        public void InsertCategoryItems()
        {
            InsertCategories();
            InsertSubCategories();
        }

        private void InsertCategories()
        {
            DbTableMap categoryMap = Config.GetMap(typeof(FeatureCategory));            

            Dictionary<int, List<Pair<string, object>>> categoryDict = new Dictionary<int, List<Pair<string, object>>>();

            foreach (DbFeatureCategory category in Config.FeatureCategories)
            {
                categoryDict.Add(category.Id, new List<Pair<string, object>>());

                string idColumn = categoryMap.GetIdColumnName();
                string nicenameColumn = categoryMap.GetColumnForMemberName("NiceName");

                categoryDict[category.Id].Add(new Pair<string, object>(idColumn, category.Id));
                categoryDict[category.Id].Add(new Pair<string, object>(nicenameColumn, category.NiceName));
            }

            DbTransaction ta = null;

            try
            {
                OpenConnection();
                ta = BeginTransaction();

                foreach (List<Pair<string, object>> pairs in categoryDict.Values)
                {
                    QueryBuilder builder = new QueryBuilder(QueryType.Insert);
                    string sql = builder.CreateInsertSql(categoryMap.Table, pairs);

                    ExecuteNonQuery(sql);
                }

                Commit(ta);
            }
            catch (Exception)
            {
                if (ta != null)
                {
                    ta.Rollback();
                }
                throw;
            }
            finally
            {
                CloseConnection();
            }            
        }

        private void InsertSubCategories()
        {
            DbTableMap subCategoryMap = Config.GetMap(typeof(FeatureSubCategory));
            Dictionary<string, List<Pair<string, object>>> subCategoryDict = new Dictionary<string, List<Pair<string, object>>>();

            foreach (DbFeatureCategory category in Config.FeatureCategories)
            {
                foreach (DbFeatureSubCategory subCategory in category.SubCategories)
                {
                    string key = string.Format("{0}-{1}", category.Id, subCategory.Id);

                    subCategoryDict.Add(key, new List<Pair<string, object>>());

                    string idColumn = subCategoryMap.GetIdColumnName();
                    string fkColumn = subCategoryMap.GetForeignKeyColumn(typeof(FeatureCategory));
                    string niceNameColumn = subCategoryMap.GetColumnForMemberName("NiceName");

                    subCategoryDict[key].Add(new Pair<string, object>(idColumn, subCategory.Id));
                    subCategoryDict[key].Add(new Pair<string, object>(fkColumn, category.Id));
                    subCategoryDict[key].Add(new Pair<string, object>(niceNameColumn, subCategory.NiceName));
                }
            }

            DbTransaction ta = null;

            try
            {
                OpenConnection();
                ta = BeginTransaction();

                foreach (List<Pair<string, object>> pairs in subCategoryDict.Values)
                {
                    QueryBuilder builder = new QueryBuilder(QueryType.Insert);
                    string sql = builder.CreateInsertSql(subCategoryMap.Table, pairs);

                    ExecuteNonQuery(sql);
                }

                Commit(ta);
            }
            catch (Exception)
            {
                if (ta != null)
                {
                    Rollback(ta);
                }
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
