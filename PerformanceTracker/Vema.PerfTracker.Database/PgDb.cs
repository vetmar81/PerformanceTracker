using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;
using System.Data.Common;
using System.Data;
using System.Xml;
using Vema.PerfTracker.Database.Config;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch 14.02.2012 01:16
    /// Concrete database implementation for PostGres database.
    /// </summary>
    public class PgDb : Db
    {
        #region Private Fields

        private readonly string user;
        private readonly string password;
        private readonly string server;
        private readonly int port;
        private readonly string database;

        #endregion

        private PgDb(DbConfig config)
        {
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

        #region Inherited Methods

        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        public override void OpenConnection()
        {
            Connection = new NpgsqlConnection(BuildConnectionString());
            Connection.Open();
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public override void CloseConnection()
        {
            Connection.Close();
        }

        /// <summary>
        /// Begins the transaction on the database.
        /// </summary>
        /// <returns>
        /// The corresponding <see cref="DbTransation"/> instance.
        /// </returns>
        public override DbTransaction BeginTransaction()
        {
            return Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        /// <summary>
        /// Commits the specified <see cref="DbTransation"/>.
        /// </summary>
        /// <param name="ta">The <see cref="DbTransation"/> to be committed.</param>
        public override void CommitTransaction(DbTransaction ta)
        {
            ta.Commit();
        }

        /// <summary>
        /// Builds the connection string for specific database implementation.
        /// </summary>
        /// <returns>
        /// The correct connection string for associated database.
        /// </returns>
        protected override string BuildConnectionString()
        {
            return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};Integrated Security=true",
                                    server, port, database, user, password);
        }

        #endregion
    }
}
