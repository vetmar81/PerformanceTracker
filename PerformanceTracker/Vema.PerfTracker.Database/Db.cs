using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:59
    /// Abstract definition of any database implementation.
    /// </summary>
    public abstract class Db
    {
        internal DbConfig Config { get; set; }

        protected static DateTime CurrentDate = new DateTime(9998, 1, 1);

        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        public abstract void OpenConnection();

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public abstract void CloseConnection();

        /// <summary>
        /// Begins the transaction on the database.
        /// </summary>
        /// <returns>The corresponding <see cref="DbTransation"/> instance.</returns>
        public abstract DbTransaction BeginTransaction();

        /// <summary>
        /// Commits the specified <see cref="DbTransation"/>.
        /// </summary>
        /// <param name="ta">The <see cref="DbTransation"/> to be committed.</param>
        public abstract void CommitTransaction(DbTransaction ta);

        /// <summary>
        /// Builds the connection string for specific database implementation.
        /// </summary>
        /// <returns>The correct connection string for associated database.</returns>
        protected abstract string BuildConnectionString();

        /// <summary>
        /// Returns a <see cref="DbDataReader"/> object that allows iterating over all affected result records.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <returns>The <see cref="DbDataReader"/> for iteration over affected result set.</returns>
        public abstract DbDataReader ExecuteReader(string sql);

        /// <summary>
        /// Executes a query with a result (e.g. from an aggregation function such as COUNT, MAX, MIN).
        /// The result represents the first column of the first row of the result.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <returns>The result as <see cref="object"/>.</returns>
        public abstract object ExecuteScalar(string sql);

        /// <summary>
        /// Executes a non-query statement (e.g. INSERT, UPDATE, DELETE statements) and returns the number
        /// of rows affected by the statement.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <returns>The number of rows that were affected by the statement.</returns>
        public abstract int ExecuteNonQuery(string sql);

        /// <summary>
        /// Load the requested object type by specfied unique ID from database.
        /// </summary>
        /// <typeparam name="T">The object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="id">The database ID of the object.</param>
        /// <returns>The requested object or <c>null</c>, if no object for specified ID found.</returns>
        public T LoadById<T>(long id) where T : DomainObject
        {
            T result = null;

            string tableName = GetTableName<T>();
            string idColumn = GetIdColumn<T>();
            string[] columns = GetInitiallyLoadedColumns<T>();

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            QueryConstraint idConstraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);

            try
            {
                if (IsUnique<T>(id))
                {
                    OpenConnection();

                    string query = builder.CreateSelectQuery(tableName, idConstraint, columns);

                    DbDataReader reader = ExecuteReader(query);

                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        result = LoadObject<T>(reader);
                        reader.Close();
                    }
                }

                //if (map.HasReferencedTypes())
                //{
                //    foreach (var referenceTypeMember in map.GetReferencedTypes())
                //    {
                //        DbTableMap refMap = Config.GetMap(referenceTypeMember.Type);

                //        if (refMap == null)
                //        {
                //            throw new PersistenceException(string.Format("No table mapping found for class {0}!", type), type);
                //        }

                //        string refTable = refMap.Table;
                //        string foreignKeyColumn = refMap.GetForeignKeyColumn(referenceTypeMember.Type);

                //        builder = new QueryBuilder(QueryType.Select);
                //        constraint = new QueryConstraint(foreignKeyColumn, result.Id, QueryOperator.Equal);

                //        reader = ExecuteReader(builder.CreateSelectQuery(refTable, constraint, refMap.GetInitiallyLoadedColumns()));

                //        while (reader.Read())
                //        {

                //        }
                //    }
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        //public T LoadById<T>(long id, IEnumerable<Pair<QueryOperator, QueryConstraint>> constraints) where T : DomainObject
        //{
        //    T result = null;

        //    string tableName = GetTableName<T>();
        //    string idColumn = GetIdColumn<T>();
        //    string[] columns = GetInitiallyLoadedColumns<T>();

        //    QueryBuilder builder = new QueryBuilder(QueryType.Select);
        //    QueryConstraint idConstraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);

        //    if (constraints != null && constraints.Count() > 0)
        //    {
        //        idConstraint.AppendConstraints(constraints);
        //    }

        //    try
        //    {
        //        if (IsUnique<T>(id))
        //        {
        //            OpenConnection();

        //            string query = builder.CreateSelectQuery(tableName, idConstraint, columns);

        //            DbDataReader reader = ExecuteReader(query);

        //            if (reader != null && reader.HasRows)
        //            {
        //                reader.Read();
        //                result = LoadObject<T>(reader);
        //                reader.Close();
        //            }
        //        }

        //        //if (map.HasReferencedTypes())
        //        //{
        //        //    foreach (var referenceTypeMember in map.GetReferencedTypes())
        //        //    {
        //        //        DbTableMap refMap = Config.GetMap(referenceTypeMember.Type);

        //        //        if (refMap == null)
        //        //        {
        //        //            throw new PersistenceException(string.Format("No table mapping found for class {0}!", type), type);
        //        //        }

        //        //        string refTable = refMap.Table;
        //        //        string foreignKeyColumn = refMap.GetForeignKeyColumn(referenceTypeMember.Type);

        //        //        builder = new QueryBuilder(QueryType.Select);
        //        //        constraint = new QueryConstraint(foreignKeyColumn, result.Id, QueryOperator.Equal);

        //        //        reader = ExecuteReader(builder.CreateSelectQuery(refTable, constraint, refMap.GetInitiallyLoadedColumns()));

        //        //        while (reader.Read())
        //        //        {

        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Load the requested object type by specfied ID and <see cref="QueryConstraint"/> from database.
        ///// The <see cref="QueryOperator"/> defines the condition for the constraint.
        ///// </summary>
        ///// <typeparam name="T">The object type; inherits from <see cref="DomainObject"/>.</typeparam>
        ///// <param name="id">The database ID of the object.</param>
        ///// <param name="op">The <see cref="QueryOperator"/> to be used for the <see cref="QueryConstraint"/>.</param>
        ///// <param name="constraint">The <see cref="QueryConstraint"/> to filter the restrict the result further.</param>
        ///// <returns>
        ///// The requested object or <c>null</c>, if no object for specified ID found.
        ///// </returns>
        //public T LoadById<T>(long id, QueryOperator op, QueryConstraint constraint) where T : DomainObject
        //{
        //    T result = null;

        //    string tableName = GetTableName<T>();
        //    string idColumn = GetIdColumn<T>();
        //    string[] columns = GetInitiallyLoadedColumns<T>();

        //    QueryBuilder builder = new QueryBuilder(QueryType.Select);
        //    QueryConstraint idConstraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);

        //    if (constraint != null)
        //    {
        //        idConstraint.AppendConstraint(op, constraint);
        //    }

        //    try
        //    {
        //        if (IsUnique<T>(id))
        //        {
        //            OpenConnection();

        //            string query = builder.CreateSelectQuery(tableName, idConstraint, columns);

        //            DbDataReader reader = ExecuteReader(query);

        //            if (reader != null && reader.HasRows)
        //            {
        //                reader.Read();
        //                result = LoadObject<T>(reader);
        //                reader.Close();
        //            }
        //        }



        //        //if (map.HasReferencedTypes())
        //        //{
        //        //    foreach (var referenceTypeMember in map.GetReferencedTypes())
        //        //    {
        //        //        DbTableMap refMap = Config.GetMap(referenceTypeMember.Type);

        //        //        if (refMap == null)
        //        //        {
        //        //            throw new PersistenceException(string.Format("No table mapping found for class {0}!", type), type);
        //        //        }

        //        //        string refTable = refMap.Table;
        //        //        string foreignKeyColumn = refMap.GetForeignKeyColumn(referenceTypeMember.Type);

        //        //        builder = new QueryBuilder(QueryType.Select);
        //        //        constraint = new QueryConstraint(foreignKeyColumn, result.Id, QueryOperator.Equal);

        //        //        reader = ExecuteReader(builder.CreateSelectQuery(refTable, constraint, refMap.GetInitiallyLoadedColumns()));

        //        //        while (reader.Read())
        //        //        {

        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }

        //    return result;
        //}

        public void LoadProperty<T>(T obj, string name, DbDataReader reader) where T : DomainObject
        {
            Dao dao = DaoFactory.CreateDao<T>();
            LoadProperty<T>(obj, dao, name, reader);
        }

        public void LoadProperty<T>(T obj, Dao dao, string propertyName, DbDataReader reader) where T : DomainObject
        {
            dao.LoadProperty(obj, propertyName, reader);
        }       

        /// <summary>
        /// Loads all objects of specified type <typeparamref name="T"/> from database.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <returns>The complete <see cref="List{T}"/> of objects of specified type.</returns>
        public List<T> LoadAll<T>() where T : DomainObject
        {
            return LoadAll<T>(QueryConstraint.Empty);
        }

        /// <summary>
        /// Loads all objects of specified type <typeparamref name="T"/> from database,
        /// that match the specified set <see cref="QueryConstraint"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="constraints">The set of <see cref="QueryConstraint"/> and the corresponding <see cref="QueryOperator"/>.</param>
        /// <returns>
        /// The <see cref="List{T}"/> of objects of specified type matching the set of <see cref="QueryConstraint"/>.
        /// </returns>
        public List<T> LoadAll<T>(IEnumerable<Pair<QueryOperator, QueryConstraint>> constraints) where T : DomainObject
        {
            QueryConstraint resultConstraint = new QueryConstraint();
            resultConstraint.AppendConstraints(constraints);

            return LoadAll<T>(resultConstraint);
        }

        /// <summary>
        /// Loads all objects of specified type <typeparamref name="T"/> from database,
        /// that match the specified <see cref="QueryConstraint"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="constraint">The constraint reducing the result set.</param>
        /// <returns>
        /// The <see cref="List{T}"/> of objects of specified type matching the <see cref="QueryConstraint"/>.
        /// </returns>
        public List<T> LoadAll<T>(QueryConstraint constraint) where T : DomainObject
        {
            List<T> resultList = new List<T>();

            // Get info about object mapping

            string table = GetTableName<T>();
            string[] columns = GetInitiallyLoadedColumns<T>();

            try
            {
                OpenConnection();

                // Assembly the query

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string query = builder.CreateSelectQuery(table, constraint, columns);

                // Get the result set

                DbDataReader reader = ExecuteReader(query);

                while (reader.Read())
                {
                    T t = LoadObject<T>(reader);
                    resultList.Add(t);
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return resultList;
        }

        /// <summary>
        /// Loads all objects of specified type <typeparamref name="T"/> from database,
        /// that match the <paramref name="whereClause"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="whereClause">The where clause reducing the result set.</param>
        /// <returns>
        /// The <see cref="List{T}"/> of objects of specified type matching the <paramref name="whereClause"/>.
        /// </returns>
        public List<T> LoadAll<T>(string whereClause) where T : DomainObject
        {
            List<T> resultList = new List<T>();

            // Get info about object mapping

            string table = GetTableName<T>();
            string[] columns = GetInitiallyLoadedColumns<T>();

            try
            {
                OpenConnection();

                // Assemble the query respecting the where clause

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string query = builder.CreateSelectQuery(table, null, columns);
                query = string.Concat(query, " where ", whereClause);

                // Get the result set

                DbDataReader reader = ExecuteReader(query);

                while (reader.Read())
                {
                    T t = LoadObject<T>(reader);
                    resultList.Add(t);
                }

                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return resultList;
        }

        /// <summary>
        /// Determines whether the specified ID is unique for objects of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be evaluated; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="id">The ID to ve evaluated for uniqueness.</param>
        /// <returns>
        ///   <c>true</c> if the specified ID is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUnique<T>(long id) where T : DomainObject
        {
            return GetCount<T>(id) == 1;
        }

        /// <summary>
        /// Gets the count of objects of type <typeparamref name="T"/> with specified ID.
        /// </summary>
        /// <typeparam name="T">The object type to be evaluated; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="id">The id to be evaluated.</param>
        /// <returns>The count of database entries of type <typeparamref name="T"/> providing the specidfied ID.</returns>
        private int GetCount<T>(long id) where T : DomainObject
        {
            string tableName = GetTableName<T>();
            string idColumn = GetIdColumn<T>();
            int count = -1;
            try
            {
                OpenConnection();

                count = Convert.ToInt32(ExecuteScalar(string.Format("select count({0}) from {1} where id = {2}", idColumn, tableName, id)));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }

            return count;
        }

        /// <summary>
        /// Loads the object from database by using the associated <see cref="DbDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits <see cref="DomainObject"/>.</typeparam>
        /// <param name="reader">The <see cref="DbDataReader"/> for access to database values.</param>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The object of type <typeparamref name="T"/>.</returns>
        private T LoadObject<T>(DbDataReader reader) where T : DomainObject
        {
            return LoadObject<T>(reader, GetMap<T>());
        }

        /// <summary>
        /// Loads the object from database by using the associated <see cref="DbDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits <see cref="DomainObject"/>.</typeparam>
        /// <param name="reader">The <see cref="DbDataReader"/> for access to database values.</param>
        /// <param name="map">The <see cref="DbTableMap"/> that specifies the object mapping to database.</param>
        /// <returns>
        /// The object of type <typeparamref name="T"/>.
        /// </returns>
        private T LoadObject<T>(DbDataReader reader, DbTableMap map) where T : DomainObject
        {
            // Create the necessary DAO and then the domain object from DAO

            Dao dao = DaoFactory.CreateDao<T>();
            dao.AssignPersistenceInfo(map);
            dao.Load(reader);

            return (T) DomainObject.CreateObject(dao);
        }

        #region Object Mapping Information

        /// <summary>
        /// Gets the name of the columns to be loaded by default for specified object type <typeparamref name="T"/>,
        /// as specified in the object mapping to database.
        /// </summary>
        /// <typeparam name="T">The affected object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="PersistenceException">Thrown, if no mapping or no columns were defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The name of the columns to be loaded by default for specified object type <typeparamref name="T"/>.</returns>
        private string[] GetInitiallyLoadedColumns<T>() where T : DomainObject
        {
            string type = typeof(T).FullName;
            string[] columns = GetMap<T>().GetInitiallyLoadedColumns();

            if (columns == null || columns.Length == 0)
            {
                throw new PersistenceException(string.Format("No columns configured for initially loading in mapping for type {0}!", type), type);
            }

            return columns;
        }

        /// <summary>
        /// Gets the table name of specified object type <typeparamref name="T"/>, 
        /// that is specified in the object mapping to database.
        /// </summary>
        /// <typeparam name="T">The affected object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The database table name mapping the specified object type <typeparamref name="T"/> on database.</returns>
        private string GetTableName<T>() where T : DomainObject
        {
            return GetMap<T>().Table;
        }

        /// <summary>
        /// Gets the ID column of specified object type <typeparamref name="T"/>, that is specified
        /// in the object mapping to database.
        /// </summary>
        /// <typeparam name="T">The affected object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="PersistenceException">Thrown, if no mapping or no ID column was defined in object mapping 
        /// of type <typeparamref name="T"/> to database.</exception>
        /// <returns>The name of the ID column of specified object type <typeparamref name="T"/>.</returns>
        private string GetIdColumn<T>() where T : DomainObject
        {
            string type = typeof(T).FullName;
            string idColumn = GetMap<T>().GetIdColumn();

            if (string.IsNullOrEmpty(idColumn))
            {
                throw new PersistenceException(string.Format("No id column defined in mapping for type {0}!", type), type);
            }

            return GetMap<T>().GetIdColumn();
        }

        /// <summary>
        /// Gets the <see cref="DbTableMap"/> specifiy the object mapping to database.
        /// </summary>
        /// <typeparam name="T">The affected object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The appropriate <see cref="DbTableMap"/> specifying the object mapping.</returns>
        private DbTableMap GetMap<T>() where T : DomainObject
        {
            string type = typeof(T).FullName;
            DbTableMap map = Config.GetMap(type);

            if (map == null)
            {
                throw new PersistenceException(string.Format("No table mapping found for class {0}!", type), type);
            }

            return map;
        }

        #endregion
    }
}
