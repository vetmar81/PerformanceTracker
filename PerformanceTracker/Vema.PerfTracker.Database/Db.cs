using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Access;
using System.Reflection;
using System.Collections;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:59
    /// Abstract definition of any database implementation.
    /// </summary>
    public abstract class Db
    {
        /// <summary>
        /// Gets or sets the configuration instance providing details about object-relational mapping.
        /// </summary>
        /// <value>
        /// The object-relational configuration instance.
        /// </value>
        internal DbConfig Config { get; set; }

        /// <summary>
        /// Constant <see cref="DateTime"/> value representing the current date
        /// for any temporal information. Any kind of object with validto date
        /// set to <see cref="CurrentDate"/> is to be treated as currently effective.
        /// </summary>
        internal static DateTime CurrentDate = new DateTime(9998, 1, 1);

        #region Abstract Method Definitions

        /// <summary>
        /// Opens the connection to the database.
        /// </summary>
        /// <returns>The associated <see cref="DbConnection"/>.</returns>
        public abstract DbConnection OpenConnection();

        /// <summary>
        /// Closes the specified <see cref="DbConnection"/> to the database.
        /// </summary>
        /// <param name="connection">The associated <see cref="DbConnection"/>.</param>
        public abstract void CloseConnection(DbConnection connection);

        /// <summary>
        /// Begins the transaction on the database.
        /// </summary>
        /// <param name="connection">The associated <see cref="DbConnection"/>.</param>
        /// <returns>
        /// The corresponding <see cref="DbTransaction"/> instance.
        /// </returns>
        public abstract DbTransaction BeginTransaction(DbConnection connection);

        /// <summary>
        /// Commits the specified <see cref="DbTransaction"/>.
        /// </summary>
        /// <param name="ta">The <see cref="DbTransaction"/> to be committed.</param>
        public abstract void Commit(DbTransaction ta);

        /// <summary>
        /// Performs a rollback of specified <see cref="DbTransaction"/>.
        /// </summary>
        /// <param name="ta">The <see cref="DbTransaction"/> to be rolled back.</param>
        public abstract void Rollback(DbTransaction ta);

        /// <summary>
        /// Builds the connection string for specific database implementation.
        /// </summary>
        /// <returns>The correct connection string for associated database.</returns>
        protected abstract string BuildConnectionString();

        /// <summary>
        /// Returns a <see cref="DbDataReader"/> object that allows iterating over all affected result records.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <param name="connection">The associated <see cref="DbConnection"/>.</param>
        /// <returns>
        /// The <see cref="DbDataReader"/> for iteration over affected result set.
        /// </returns>
        public abstract DbDataReader ExecuteReader(string sql, DbConnection connection);

        /// <summary>
        /// Executes a query with a result (e.g. from an aggregation function such as COUNT, MAX, MIN).
        /// The result represents the first column of the first row of the result.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <param name="connection">The associated <see cref="DbConnection"/>.</param>
        /// <returns>
        /// The result as <see cref="object"/>.
        /// </returns>
        public abstract object ExecuteScalar(string sql, DbConnection connection);

        /// <summary>
        /// Executes a non-query statement (e.g. INSERT, UPDATE, DELETE statements) and returns the number
        /// of rows affected by the statement.
        /// </summary>
        /// <param name="sql">The SQL expression to be executed.</param>
        /// <param name="connection">The associated <see cref="DbConnection"/>.</param>
        /// <returns>The number of rows that were affected by the statement.</returns>
        public abstract int ExecuteNonQuery(string sql, DbConnection connection);

        #endregion

        #region Load objects

        /// <summary>
        /// Loads the specified object of type <typeparamref name="T"/> by its database ID.
        /// </summary>
        /// <typeparam name="T">The requested object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="id">The database ID of the object.</param>
        /// <exception cref="PersistenceException">Thrown, if mapping information incorrect or 
        /// if database ID of specified object is not unique.</exception>
        /// <returns>The requested object of type <typeparamref name="T"/> or <c>null</c>,
        /// if object with specified ID doesn't exist.</returns>
        public virtual T LoadById<T>(long id) where T : DomainObject
        {
            // If object ID not assigned return null

            if (id == -1)
            {
                return null;
            }

            T result = null;
            Type type = typeof(T);

            string tableName = GetTableName(type);
            string idColumn = GetIdColumn(type);
            string[] columns = GetInitiallyLoadedColumns(type);

            // Build the query using the ID constraint

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            QueryConstraint idConstraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);

            DbConnection connection = null;

            try
            {
                // Check if the ID is unique

                if (IsUnique(id, type))
                {
                    connection = OpenConnection();

                    string sql = builder.CreateSelectSql(tableName, idConstraint, columns);

                    DbDataReader reader = ExecuteReader(sql, connection);

                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        result = LoadObject<T>(reader);
                        reader.Close();
                    }
                }
                else
                {
                    throw new PersistenceException(string.Format("Specified ID {0} of type {1} is not unique! Consider using method GetAll with constraints instead.",
                                                                    id, typeof(T).Name));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }

            // Load IDs only of referenced objects

            if (result != null)
            {
                LoadReferencedTypeIds(result);
            }

            return result;
        }

        /// <summary>
        /// Loads the currently valid reference object of kind <typeparamref name="T"/> representing temporal data
        /// and matching the specified <see cref="QueryConstraint"/>.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <param name="id">The database ID of the object.</param>
        /// <returns>
        /// The currently valid object of kind <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="PersistenceException">Thrown, if mapping information incorrect or
        /// if database ID of specified temporal object type <typeparamref name="T"/> is not unique.</exception>
        public T LoadCurrent<T>(long id) where T : DomainObject, ITemporal
        {
            string idColumn = GetIdColumn(typeof(T));
            QueryConstraint idConstraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);

            return LoadCurrent<T>(idConstraint);
        }

        /// <summary>
        /// Loads the currently valid reference object of kind <typeparamref name="T"/> representing temporal data
        /// and matching the specified <see cref="QueryConstraint"/>.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <param name="constraint">The <see cref="QueryConstraint"/> to be applied.</param>
        /// <returns>
        /// The currently valid object of kind <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="PersistenceException">Thrown, if mapping information incorrect or
        /// if database ID of specified temporal object type <typeparamref name="T"/> is not unique.</exception>
        public T LoadCurrent<T>(QueryConstraint constraint) where T : DomainObject, ITemporal
        {
            T result = null;
            DbTableMap map = GetMap(typeof(T));
            string tableName = map.Table;
            string[] columns = map.GetInitiallyLoadedColumns();

            QueryConstraint validityConstraint = new QueryConstraint("validto", CurrentDate, QueryOperator.Equal);
            validityConstraint.AppendConstraint(QueryOperator.And, constraint);

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectSql(tableName, validityConstraint, columns);

                DbDataReader reader = ExecuteReader(sql, connection);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    result = LoadObject<T>(reader);
                    reader.Close();
                }

                if (result != null && !IsUnique(result.Id, typeof(T)))
                {
                    throw new PersistenceException(string.Format("Specified ID {0} of type {1} is not unique! Consider using method GetAll with constraints instead.",
                                                                    result.Id, typeof(T).Name));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }                
            }

            // Load IDs only of referenced objects

            if (result != null)
            {
                LoadReferencedTypeIds(result);
            }

            return result;
        }

        /// <summary>
        /// Loads the currently valid reference object of kind <typeparamref name="T"/> representing temporal data.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <param name="parentId">The ID of the referencing parent <see cref="DomainObject"/>.</param>
        /// <param name="parentType">Type of the referencing parent <see cref="DomainObject"/>.</param>
        /// <returns>
        /// The currently valid object of kind <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="PersistenceException">Thrown, if mapping information incorrect or
        /// if database ID of specified temporal object type <typeparamref name="T"/> is not unique.</exception>
        public T LoadCurrent<T>(long parentId, Type parentType) where T : DomainObject, ITemporal
        {
            // If parent Id on default return null

            if (parentId == -1)
            {
                return null;
            }

            T result = null;
            Type childType = typeof(T);

            string parentIdColumn = GetIdColumn(parentType);
            string foreignTable = GetTableName(childType);
            string foreignKeyColumn = GetMap(childType).GetForeignKeyColumn(parentType);
            string[] columns = GetInitiallyLoadedColumns(childType);

            QueryConstraint constraint = new QueryConstraint(foreignKeyColumn, parentId, QueryOperator.Equal);
            constraint.AppendConstraint(QueryOperator.And, new QueryConstraint("validto", CurrentDate, QueryOperator.Equal));

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectSql(foreignTable, constraint, columns);

                DbDataReader reader = ExecuteReader(sql, connection);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    result = LoadObject<T>(reader);
                    reader.Close();

                    if (result != null && IsUnique(result.Id, childType))
                    {
                        throw new PersistenceException(string.Format("Specified ID {0} of type {1} is not unique! Consider using method GetAll with constraints instead.",
                                                                    parentId, childType.Name));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }

            // Load IDs only of referenced objects

            if (result != null)
            {
                LoadReferencedTypeIds(result);
            }

            return result;
        }

        /// <summary>
        /// Loads the currently valid reference object of kind <typeparamref name="T"/> representing temporal data.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <param name="parent">The parent reference <see cref="DomainObject"/>.</param>
        /// <exception cref="PersistenceException">Thrown, if mapping information incorrect or 
        /// if database ID of specified temporal object type <typeparamref name="T"/> is not unique.</exception>
        /// <returns>The currently valid object of kind <typeparamref name="T"/>.</returns>
        public T LoadCurrent<T>(DomainObject parent) where T : DomainObject, ITemporal
        {
            return LoadCurrent<T>(parent.Id, parent.GetType());
        }

        /// <summary>
        /// Loads all currently valid objects of kind <typeparamref name="T"/> representing temporal data.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <returns>The list of currently valid temporal objects of type <typeparamref name="T"/>.</returns>
        public List<T> LoadAllCurrent<T>() where T : DomainObject, ITemporal
        {
            return LoadAllCurrent<T>(null);
        }

        /// <summary>
        /// Loads all currently valid objects of kind <typeparamref name="T"/> representing temporal data.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <param name="constraint">The additional <see cref="QueryConstraint"/> further reducing the result set.</param>
        /// <returns>
        /// The list of currently valid temporal objects of type <typeparamref name="T"/>.
        /// </returns>
        public List<T> LoadAllCurrent<T>(QueryConstraint constraint) where T : DomainObject, ITemporal
        {
            QueryConstraint dateConstraint = new QueryConstraint("validto", Db.CurrentDate, QueryOperator.Equal);
            if (constraint != null)
            {
                dateConstraint.AppendConstraint(QueryOperator.And, constraint);
            }
            return LoadAll<T>(dateConstraint);
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
            Type type = typeof(T);

            // Get info about object mapping

            string table = GetTableName(type);
            string[] columns = GetInitiallyLoadedColumns(type);

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();

                // Assembly the query

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectSql(table, constraint, columns);

                // Get the result set

                DbDataReader reader = ExecuteReader(sql, connection);

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T t = LoadObject<T>(reader);

                        // Load IDs only of referenced objects

                        if (t != null)
                        {
                            LoadReferencedTypeIds(t);
                            resultList.Add(t);
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
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
            Type type = typeof(T);

            // Get info about object mapping

            string table = GetTableName(type);
            string[] columns = GetInitiallyLoadedColumns(type);

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();

                // Assemble the query respecting the where clause

                QueryBuilder builder = new QueryBuilder(QueryType.Select);

                string sql = builder.CreateSelectSql(table, null, columns);
                sql = string.Concat(sql, " where ", whereClause);

                // Get the result set

                DbDataReader reader = ExecuteReader(sql, connection);

                if (reader != null && reader.HasRows)
                {
                    // Load objects

                    while (reader.Read())
                    {
                        T t = LoadObject<T>(reader);

                        // Load IDs only of referenced objects

                        if (t != null)
                        {
                            LoadReferencedTypeIds(t);
                            resultList.Add(t);
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Loads the object from database by using the associated <see cref="DbDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits <see cref="DomainObject"/>.</typeparam>
        /// <param name="reader">The <see cref="DbDataReader"/> for access to database values.</param>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The object of type <typeparamref name="T"/>.</returns>
        internal virtual T LoadObject<T>(DbDataReader reader) where T : DomainObject
        {
            return LoadObject<T>(reader, GetMap(typeof(T)));
        }

        /// <summary>
        /// Loads the object from database by using th associated <see cref="DbDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to be loaded; inherits <see cref="DomainObject"/>.</typeparam>
        /// <param name="reader">The <see cref="DbDataReader"/> for access to database values.</param>
        /// <param name="map">The <see cref="DbTableMap"/> that specifies the object mapping to database.</param>
        /// <param name="type">The <see cref="Type"/> instance of <typeparamref name="T"/>.</param>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The object of type <typeparamref name="T"/>.</returns>
        internal virtual T LoadObject<T>(Type type, DbDataReader reader, DbTableMap map) where T : DomainObject
        {
            Dao dao = DaoFactory.CreateDao(type);
            return LoadObject<T>(dao, reader, map);
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
        internal virtual T LoadObject<T>(DbDataReader reader, DbTableMap map) where T : DomainObject
        {
            // Create the necessary DAO and then the domain object from DAO

            Dao dao = DaoFactory.CreateDao<T>();
            return LoadObject<T>(dao, reader, map);
        }

        /// <summary>
        /// Loads the object for specified type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The specified object type.</typeparam>
        /// <param name="dao">The DAO to be used for object creation.</param>
        /// <param name="reader">The <see cref="DbDataReader"/> for access to database values.</param>
        /// <param name="map">The <see cref="DbTableMap"/> that specifies the object mapping to database.</param>
        /// <returns>
        /// The object of type <typeparamref name="T"/>.
        /// </returns>
        internal virtual T LoadObject<T>(Dao dao, DbDataReader reader, DbTableMap map) where T : DomainObject
        {
            foreach (var member in map.GetInitiallyLoadedNonReferencedTypeMembers())
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                        BindingFlags.SetProperty | BindingFlags.Instance;

                PropertyInfo info = dao.GetType().GetProperty(member.Name, flags);
                object value = reader[member.Column];

                // Check for nullable property

                if (IsNullableProperty(info))
                {
                    // Check for DB null value

                    if (value == DBNull.Value)
                    {
                        info.SetValue(dao, null, null);
                    }
                    else
                    {
                        info.SetValue(dao, value, null);
                    }
                }
                else
                {
                    info.SetValue(dao, value, null);
                }               
            }

            return (T) dao.CreateDomainObject();
        }

        /// <summary>
        /// Loads value of specified column and <paramref name="type"/> from the database.
        /// </summary>
        /// <param name="id">The database ID to be looked up for given <paramref name="type"/>.</param>
        /// <param name="type">The <see cref="Type"/> to be respected.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>The column value as <see cref="object"/>.</returns>
        internal virtual object LoadSingleColumnValue(long id, Type type, string columnName)
        {
            string table = GetTableName(type);
            string idColumn = GetIdColumn(type);
            object value = null;

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            QueryConstraint constraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);
            string sql = builder.CreateSelectSql(table, constraint, columnName);

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();

                DbDataReader reader = ExecuteReader(sql, connection);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    value = reader.GetValue(0);
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }

            return value;
        }

        /// <summary>
        /// Loads the list of database IDs for the referenced object of type specified by <paramref name="refType"/> 
        /// of parent <see cref="DomainObject"/> <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="DomainObject"/>.</param>
        /// <param name="refType">Type of the referenced object.</param>
        /// <param name="refPropertyName">The name of the property describing the object reference.</param>
        /// <returns>The list of database IDs of the referenced object of type specified by <paramref name="refType"/>.</returns>
        protected virtual List<long> LoadAllReferencedTypeIds(DomainObject parent, Type refType, string refPropertyName)
        {
            List<long> idList = new List<long>();

            DbTableMap refMap = GetMap(refType);
            DbTableMap parentMap = GetMap(parent.GetType());

            string refTable = refMap.Table;
            string refIdColumn = refMap.GetIdColumnName();
            string fkColumn;

            // Flag indicating the direction of the relation
            // forward: primary key -> foreign key
            // backward: foreign key -> primary key

            bool isForwardRelation;

            // 1. Search ID reference as foreign key on related object

            if (!string.IsNullOrEmpty(refMap.GetForeignKeyColumn(parent.GetType())))
            {
                fkColumn = refMap.GetForeignKeyColumn(parent.GetType());
                isForwardRelation = true;
            }

            // 2. Search ID reference as primary key on parent object

            else if (!string.IsNullOrEmpty(parentMap.GetIdColumnName()))
            {
                fkColumn = parentMap.GetIdColumnName();
                isForwardRelation = false;
            }
            else
            {
                throw new PersistenceException(string.Format("Couldn't find foreign key Column! Parent type: {0}, Child type: {1}",
                                                            parent.GetType().Name, refType.Name));
            }

            // The ID to be looked up is in case of a forward relation
            // the ID of parent object as foreign key on the related table

            // The ID to be looked up is in case of a backward relation
            // the ID of the child object as foreign key on the parent table
            // => load foreign key ID value on parent table

            long parentId = (isForwardRelation) ? parent.Id
                                                : (long) LoadSingleColumnValue(parent.Id,
                                                                                parent.GetType(),
                                                                                parentMap.GetForeignKeyColumn(refType));

            QueryConstraint constraint = new QueryConstraint(fkColumn, parentId, QueryOperator.Equal);

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();

                // Add constraint to retrieve ID of currently valid object
                // for temporal types

                if (ImplementsInterfaceType(refType, typeof(ITemporal)))
                {
                    constraint.AppendConstraint(QueryOperator.And, new QueryConstraint("validto", CurrentDate, QueryOperator.Equal));
                }

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectSql(refTable, constraint, refIdColumn);

                DbDataReader reader = ExecuteReader(sql, connection);

                // Read all rows

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idList.Add(reader.GetInt64(0));
                    }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }

            return idList;
        }

        /// <summary>
        /// Loads the database ID of the referenced object of type specified by <paramref name="refType"/> 
        /// of parent <see cref="DomainObject"/> <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="DomainObject"/>.</param>
        /// <param name="refType">Type of the referenced object.</param>
        /// <param name="refPropertyName">The name of the property describing the object reference.</param>
        /// <returns>The datbase ID of the referenced object of type specified by <paramref name="refType"/>.</returns>
        protected virtual long LoadReferencedTypeId(DomainObject parent, Type refType, string refPropertyName)
        {
            DbTableMap refMap = GetMap(refType);
            DbTableMap parentMap = GetMap(parent.GetType());

            string refTable = refMap.Table;
            string refIdColumn = refMap.GetIdColumnName();
            string fkColumn;

            // Flag indicating the direction of the relation
            // forward: primary key -> foreign key
            // backward: foreign key -> primary key

            bool isForwardRelation;

            // 1. Search ID reference as foreign key on related object

            if (!string.IsNullOrEmpty(refMap.GetForeignKeyColumn(parent.GetType())))
            {
                fkColumn = refMap.GetForeignKeyColumn(parent.GetType());
                isForwardRelation = true;
            }

            // 2. Search ID reference as primary key on parent object

            else if (!string.IsNullOrEmpty(parentMap.GetIdColumnName()))
            {
                fkColumn = parentMap.GetIdColumnName();
                isForwardRelation = false;
            }
            else
            {
                throw new PersistenceException(string.Format("Couldn't find foreign key Column! Parent type: {0}, Child type: {1}",
                                                            parent.GetType().Name, refType.Name));
            }

            long refId = -1;

            // The ID to be looked up is in case of a forward relation
            // the ID of parent object as foreign key on the related table

            // The ID to be looked up is in case of a backward relation
            // the ID of the child object as foreign key on the parent table
            // => load foreign key ID value on parent table

            long parentId = (isForwardRelation) ? parent.Id
                                                : (long) LoadSingleColumnValue(parent.Id,
                                                                                parent.GetType(),
                                                                                parentMap.GetForeignKeyColumn(refType));            

            QueryConstraint constraint = new QueryConstraint(fkColumn, parentId, QueryOperator.Equal);
            DbConnection connection = null;
            try
            {
                connection = OpenConnection();

                // Add constraint to retrieve ID of currently valid object
                // for temporal types

                if (ImplementsInterfaceType(refType, typeof(ITemporal)))
                {
                    constraint.AppendConstraint(QueryOperator.And, new QueryConstraint("validto", CurrentDate, QueryOperator.Equal));
                }

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectSql(refTable, constraint, refIdColumn);

                DbDataReader reader = ExecuteReader(sql, connection);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    refId = reader.GetInt64(0);
                    reader.Close();
                }

                if (refId != -1)
                {
                    if (!IsUnique(refId, refType))
                    {
                        throw new PersistenceException(string.Format("Specified ID {0} of type {1} is not unique!",
                                                                        refId, refType.Name));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }   
            }

            return refId;
        }

        /// <summary>
        /// Loads the database IDs of all object references in <see cref="DomainObject"/> <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">The <see cref="DomainObject"/>, which to load referenced object IDs for.</param>
        protected virtual void LoadReferencedTypeIds(DomainObject obj)
        {
            DbTableMap map = GetMap(obj.GetType());

            foreach (DbMemberMap member in map.GetReferencedTypes())
            {
                Type parentType = obj.GetType();
                Type refType = Type.GetType(member.Type);

                // Use reflection to determine the property and set / get property values
                

                BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty |
                                            BindingFlags.SetProperty | BindingFlags.Instance;

                PropertyInfo info = parentType.GetProperty(member.Name, flags);

                // If property is a list

                if (ImplementsInterfaceType(info.PropertyType, typeof(IList)))
                {
                    List<long> idList = LoadAllReferencedTypeIds(obj, refType, member.Name);
                    IList refList = (IList) info.GetValue(obj, null);

                    foreach (long id in idList)
                    {
                        Dao refDao = DaoFactory.CreateDao(refType);
                        refDao.Id = id;

                        DomainObject refObj = refDao.CreateDomainObject();
                        refList.Add(refObj);
                    }
                }
                else
                {
                    long refId = LoadReferencedTypeId(obj, refType, member.Name);

                    // If ID of referenced type couldn't be loaded skip this reference

                    if (refId != -1)
                    {
                        Dao refDao = DaoFactory.CreateDao(refType);
                        refDao.Id = refId;

                        DomainObject refObj = refDao.CreateDomainObject();
                        info.SetValue(obj, refObj, null);
                        // check if property is a list
                    }    
                }                                  
            }
        }        

        #endregion        

        #region Save / Update objects

        /// <summary>
        /// Saves the specified bulk of instances with type <typeparamref name="T"/> to the database.
        /// </summary>
        /// <typeparam name="T">The kind of object to be saved; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="tList">The set of objects to be saved.</param>
        public virtual void BulkSaveObject<T>(IEnumerable<T> tList) where T : DomainObject
        {
            Type type = typeof(T);
            DbTableMap map = GetMap(typeof(T));

            DbConnection connection = null;
            DbTransaction ta = null;
            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                foreach (T t in tList)
                {
                    // Generate next ID;
                    // Throw exception, if this fails

                    long nextId = GenerateNextId(type, map, ta);

                    if (nextId == -1)
                    {
                        throw new PersistenceException(string.Format("Couldn't save object of type {0} - no ID could be created!",
                                                                        type.Name));
                    }

                    // Assign generated ID

                    t.Id = nextId;

                    // Update valid from / valid to for temporal object

                    if (ImplementsInterfaceType(type, typeof(ITemporal)))
                    {
                        ITemporal temporal = (ITemporal) t;
                        temporal.ValidFrom = DateTime.Today;
                        temporal.ValidTo = CurrentDate;
                    }

                    IEnumerable<Pair<string, object>> columnValuePairs = CreateColumnValuePairs<T>(t, map);
                    QueryBuilder builder = new QueryBuilder(QueryType.Insert);
                    string sql = builder.CreateInsertSql(map.Table, columnValuePairs);

                    // Insert the object

                    ExecuteNonQuery(sql, connection);
                }

                Commit(ta);
            }
            catch (Exception)
            {
                // Rollback all in case an exception was raised

                if (ta != null)
                {
                    Rollback(ta);
                }
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        /// <summary>
        /// Saves the specified object of type <typeparamref name="T"/> to the database.
        /// </summary>
        /// <typeparam name="T">The kind of object to be saved; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="t">The object to be saved.</param>
        public virtual void SaveObject<T>(T t) where T : DomainObject
        {
            Type type = typeof(T);
            DbTableMap map = GetMap(typeof(T));

            // Generate next ID; throw exception, if this fails

            long nextId = GenerateNextId(type, map);

            if (nextId == -1)
            {
                throw new PersistenceException(string.Format("Couldn't save object of type {0} - no ID could be created!",
                                                                type.Name));
            }

            // Assign the generated ID

            t.Id = nextId;

            // Update valid from / valid to for temporal object

            if (ImplementsInterfaceType(type, typeof(ITemporal)))
            {
                ITemporal temporal = (ITemporal) t;
                temporal.ValidFrom = DateTime.Today;
                temporal.ValidTo = CurrentDate;
            }

            IEnumerable<Pair<string, object>> columnValuePairs = CreateColumnValuePairs<T>(t, map);
            QueryBuilder builder = new QueryBuilder(QueryType.Insert);
            string sql = builder.CreateInsertSql(map.Table, columnValuePairs);

            DbConnection connection = null;
            DbTransaction ta = null;

            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                ExecuteNonQuery(sql, connection);

                Commit(ta);
            }
            catch (Exception)
            {
                // Rollback, if an exception was raised

                if (ta != null)
                {
                    Rollback(ta);
                }
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        /// <summary>
        /// Updates the specified bulk of instances with type <typeparamref name="T"/> to the database.
        /// </summary>
        /// <typeparam name="T">The kind of object to be updated; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="tList">The set of objects to be updated.</param>
        public virtual void BulkUpdateObject<T>(IEnumerable<T> tList) where T : DomainObject
        {
            Type type = typeof(T);
            DbTableMap map = GetMap(typeof(T));
            string idColumn = GetIdColumn(type);

            DbConnection connection = null;
            DbTransaction ta = null;

            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                foreach (T t in tList)
                {
                    IEnumerable<Pair<string, object>> columnValuePairs;

                    if (ImplementsInterfaceType(type, typeof(ITemporal)))
                    {
                        // Update valid to only for temporal object

                        ITemporal temporal = (ITemporal) t;
                        temporal.ValidTo = DateTime.Today;

                        columnValuePairs = CreateColumnValuePairs(temporal);
                    }
                    else
                    {
                        // Assemble update values by table mapping

                        columnValuePairs = CreateColumnValuePairs<T>(t, map);
                    }   

                    // Use object ID as constraint for the statement

                    QueryConstraint idConstraint = new QueryConstraint(idColumn, t.Id, QueryOperator.Equal);
                    QueryBuilder builder = new QueryBuilder(QueryType.Update);
                    string sql = builder.CreateUpdateSql(map.Table, columnValuePairs, idConstraint);

                    // Update the existing object

                    ExecuteNonQuery(sql, connection);
                }

                // Commit the bulk update

                Commit(ta);
            }
            catch (Exception)
            {
                // Rollback all in case an exception raised

                if (ta != null)
                {
                    Rollback(ta);
                }
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        /// <summary>
        /// Updates the specified object of type <typeparamref name="T"/> to the database.
        /// </summary>
        /// <typeparam name="T">The kind of object to be updated; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="t">The object to be updated.</param>
        public virtual void UpdateObject<T>(T t) where T : DomainObject
        {
            Type type = typeof(T);
            DbTableMap map = GetMap(type);
            string idColumn = GetIdColumn(type);

            IEnumerable<Pair<string, object>> columnValuePairs;

            if (ImplementsInterfaceType(type, typeof(ITemporal)))
            {
                // Update valid to only for temporal object

                ITemporal temporal = (ITemporal) t;
                temporal.ValidTo = DateTime.Today;

                columnValuePairs = CreateColumnValuePairs(temporal);
            }
            else
            {
                // Assemble update values by table mapping

                columnValuePairs = CreateColumnValuePairs<T>(t, map);
            }            

            // Use object ID as constraint for the statement

            QueryConstraint idConstraint = new QueryConstraint(idColumn, t.Id, QueryOperator.Equal);
            QueryBuilder builder = new QueryBuilder(QueryType.Update);
            string sql = builder.CreateUpdateSql(map.Table, columnValuePairs, idConstraint);

            DbConnection connection = null;
            DbTransaction ta = null;

            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                // Update the existing object

                ExecuteNonQuery(sql, connection);

                // Commit the update

                Commit(ta);
            }
            catch (Exception)
            {
                if (ta != null)
                {
                    // Rollback update in case an exception raised

                    Rollback(ta);
                }
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        #endregion

        #region Delete objects

        /// <summary>
        /// Deletes the specified bulk of instances with type <typeparamref name="T"/> and all of their references from the database.
        /// </summary>
        /// <typeparam name="T">The kind of object to be deleted; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="tList">The set of objects to be deleted.</param>
        public virtual void BulkDeleteObject<T>(IEnumerable<T> tList) where T : DomainObject
        {
            Type type = typeof(T);
            DbTableMap map = GetMap(type);
            string idColumn = GetIdColumn(type);

            // First delete all object references;
            // any failures inside this code will throw an exception

            foreach (T t in tList)
            {
                DeleteAllObjectReferences<T>(t, map);
            }

            DbConnection connection = null;
            DbTransaction ta = null;

            // After deletion of all references delete now the all parent records

            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                foreach (T t in tList)
                {
                    // Use object ID as constraint for the delete SQL statement

                    QueryConstraint idConstraint = new QueryConstraint(idColumn, t.Id, QueryOperator.Equal);
                    QueryBuilder builder = new QueryBuilder(QueryType.Delete);
                    string sql = builder.CreateDeleteSql(map.Table, idConstraint);

                    // Delete object

                    ExecuteNonQuery(sql, connection);
                }
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
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        /// <summary>
        /// Deletes the specified object of type <typeparamref name="T"/> and all referencing entries from the database.
        /// </summary>
        /// <typeparam name="T">The kind of object to be deleted; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="t">The object to be deleted.</param>
        public virtual void DeleteObject<T>(T t) where T : DomainObject
        {
            Type type = typeof(T);
            DbTableMap map = GetMap(type);
            string idColumn = GetIdColumn(type);

            // Use object ID as constraint for the delete SQL statement

            QueryConstraint idConstraint = new QueryConstraint(idColumn, t.Id, QueryOperator.Equal);
            QueryBuilder builder = new QueryBuilder(QueryType.Delete);
            string sql = builder.CreateDeleteSql(map.Table, idConstraint);

            // First delete all object references;
            // any failures inside this code will throw an exception

            DeleteAllObjectReferences<T>(t, map);

            // After deletion of all references delete now the parent record

            DbConnection connection = null;
            DbTransaction ta = null;

            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                ExecuteNonQuery(sql, connection);
            }
            catch (Exception)
            {
                // Rollback in case an exception was raised

                if (ta != null)
                {
                    Rollback(ta);
                }
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        /// <summary>
        /// Deletes all object reference records based on object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to delete all references for; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="t">The object.</param>
        /// <param name="map">The <see cref="DbTableMap"/> providing database mapping information.</param>
        internal virtual void DeleteAllObjectReferences<T>(T t, DbTableMap map) where T : DomainObject
        {
            Type type = t.GetType();
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField;

            foreach (DbMemberMap member in map.GetReferencedTypes())
            {
                // Only forward references to be deleted,
                // i.e. related objects have to deleted prior 
                // to their parent object for cascading deletion

                if (!member.IsForeignKey)
                {
                    // Get necessary values / information via reflection and
                    // database mapping information

                    PropertyInfo info = type.GetProperty(member.Name, flags);
                    Type refType = info.PropertyType;
                    DbTableMap refMap = GetMap(refType);
                    DbMemberMap refIdMember = refMap.GetIdMember();

                    object refObj = info.GetValue(t, null);

                    // Check if property implements IList (1-to-many relation)

                    if (ImplementsInterfaceType(refType, typeof(IList)))
                    {
                        IList objList = (IList) refObj;
                        List<long> idList = new List<long>();

                        foreach (object obj in objList)
                        {
                            PropertyInfo refIdInfo = refType.GetProperty(refIdMember.Name, flags);
                            long refId = (long) refIdInfo.GetValue(obj, null);

                            idList.Add(refId);
                        }

                        BulkDeleteObjectRecords(idList, refType, refMap);
                    }
                    else
                    {
                        PropertyInfo refIdInfo = refType.GetProperty(refIdMember.Name, flags);
                        long refId = (long) refIdInfo.GetValue(refObj, null);

                        DeleteObjectRecord(refId, refType, refMap, null);
                    }     
                }       
            }
        }

        /// <summary>
        /// Deletes a bulk of object records from the database,
        /// which are identified by the database IDs in <paramref name="idList"/>.
        /// </summary>
        /// <param name="idList">The set of database IDs to be deleted.</param>
        /// <param name="type">The <see cref="Type"/> of the object to be deleted.</param>
        /// <param name="map">The <see cref="DbTableMap"/> providing database mapping information.</param>
        internal void BulkDeleteObjectRecords(IEnumerable<long> idList, Type type, DbTableMap map)
        {
            DbConnection connection = null;
            DbTransaction ta = null;

            try
            {
                connection = OpenConnection();
                ta = BeginTransaction(connection);

                foreach (long id in idList)
                {
                    DeleteObjectRecord(id, type, map, ta);
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
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }
        }

        /// <summary>
        /// Deletes the object record with specified ID and <paramref name="type"/>.
        /// </summary>
        /// <param name="id">The database ID of the object to be deleted.</param>
        /// <param name="type">The <see cref="Type"/> of the object to be deleted.</param>
        /// <param name="map">The <see cref="DbTableMap"/> providing database mapping information.</param>
        /// <param name="ta">The <see cref="DbTransaction"/>. If <c>null</c>, a new database connection will be opened,
        /// which might lead to exceptions in case of any other open connection.</param>
        internal void DeleteObjectRecord(long id, Type type, DbTableMap map, DbTransaction ta)
        {
            QueryConstraint idConstraint = new QueryConstraint(map.GetIdColumnName(), id, QueryOperator.Equal);
            QueryBuilder builder = new QueryBuilder(QueryType.Delete);
            string sql = builder.CreateDeleteSql(map.Table, idConstraint);

            // If no transaction affiliated, no open connection assumed

            DbConnection connection = null;

            if (ta == null)
            {
                try
                {
                    connection = OpenConnection();
                    ta = BeginTransaction(connection);

                    ExecuteNonQuery(sql, connection);

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
                    CloseConnection(connection);
                }
            }
            else
            {
                ExecuteNonQuery(sql, ta.Connection);
            }
        }

        #endregion

        #region Database Helpers

        /// <summary>
        /// Determines whether the specified ID is unique for objects of <paramref name="type"/>.
        /// </summary>
        /// <param name="id">The ID to ve evaluated for uniqueness.</param>
        /// <param name="type">The type of object to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified ID is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUnique(long id, Type type)
        {
            return GetCount(id, type) == 1;
        }

        /// <summary>
        /// Gets the next ID in sequence.
        /// </summary>
        /// <param name="type">The affected <see cref="Type"/>.</param>
        /// <param name="map">The <see cref="DbTableMap"/> providing information about database mapping.</param>
        /// <returns>the next ID in sequence.</returns>
        internal virtual long GenerateNextId(Type type, DbTableMap map)
        {
            return GenerateNextId(type, map, null);
        }

        /// <summary>
        /// Gets the next ID in sequence.
        /// </summary>
        /// <param name="type">The affected <see cref="Type"/>.</param>
        /// <param name="map">The <see cref="DbTableMap"/> providing information about database mapping.</param>
        /// <param name="ta">The <see cref="DbTransaction"/> for ID creation. If <paramref name="ta"/> is <c>null</c>,
        /// a new connection to the database is to be created, which might result in an exception, if there is already
        /// an open connection.</param>
        /// <returns>the next ID in sequence.</returns>
        internal virtual long GenerateNextId(Type type, DbTableMap map, DbTransaction ta)
        {
            long id = -1;
            string sql = map.HasSequence ?
                            QueryBuilder.CreateNextSequenceValueSql(map)
                            : QueryBuilder.GetNextIdValueSql(map);

            DbConnection connection = null;

            if (ta == null)
            {
                try
                {
                    connection = OpenConnection();
                    id = (long) ExecuteScalar(sql, connection);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (connection != null)
                    {
                        CloseConnection(connection);
                    }
                }
            }
            else
            {
                id = (long) ExecuteScalar(sql, ta.Connection);
            }

            return id;
        }

        /// <summary>
        /// Creates the set of column / value combinations to be used in an SQL statement 
        /// for the specified <paramref name="temporal"/> object.
        /// In here only the valid to date is respected.
        /// </summary>
        /// <param name="temporal">The temporal object, which column / value combinations shall be created for.</param>
        /// <returns>The set of column / value combinations.</returns>
        internal virtual IEnumerable<Pair<string, object>> CreateColumnValuePairs(ITemporal temporal)
        {
            List<Pair<string, object>> columnValuePairList = new List<Pair<string, object>>();
            columnValuePairList.Add(new Pair<string, object>("validto", temporal.ValidTo));

            return columnValuePairList;
        }

        /// <summary>
        /// Creates the set of column / value combinations to be used in an SQL statement for specified instance <typeparamref name="T"/>
        /// by processing the property values of the instance.
        /// </summary>
        /// <typeparam name="T">The instance type to be respected; inherits <see cref="DomainObject"/>.</typeparam>
        /// <param name="t">The instance, which column / value combinations shall be created for.</param>
        /// <param name="map">The <see cref="DbTableMap"/> providing mapping information to database.</param>
        /// <returns>The set of column / value combinations.</returns>
        internal virtual IEnumerable<Pair<string, object>> CreateColumnValuePairs<T>(T t, DbTableMap map) where T : DomainObject
        {
            Type type = typeof(T);
            List<Pair<string, object>> columnValuePairList = new List<Pair<string, object>>();

            // Respect public instance properties

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | 
                                    BindingFlags.Instance | BindingFlags.GetProperty;

            // Analyze only property members not describing a referenced domain object type

            foreach (DbMemberMap member in map.GetNonReferencedTypeMembers())
            {
                string column = member.Column;
                object value = null;

                PropertyInfo info = type.GetProperty(member.Name, flags);

                if (info != null)
                {
                    value = info.GetValue(t, null);
                }

                columnValuePairList.Add(new Pair<string, object>(column, value));
            }
            foreach (DbMemberMap member in map.GetReferencedTypes())
            {
                if (member.IsForeignKey)
                {
                    string column = member.Column;

                    PropertyInfo info = type.GetProperty(member.Name, flags);
                    object refObj = info.GetValue(t, null);

                    PropertyInfo refIdInfo = refObj.GetType().GetProperty("Id");
                    long refId = (long) refIdInfo.GetValue(refObj, null);

                    columnValuePairList.Add(new Pair<string, object>(column, refId));
                }
            }

            return columnValuePairList;
        }

        /// <summary>
        /// Gets the count of objects of <paramref name="type"/> with specified ID.
        /// </summary>
        /// <param name="id">The id to be evaluated.</param>
        /// <param name="type">The <see cref="Type"/> to be investigated.</param>
        /// <returns>
        /// The count of database entries of type <paramref name="type"/> providing the specidfied ID.
        /// </returns>
        protected int GetCount(long id, Type type)
        {
            string tableName = GetTableName(type);
            string idColumn = GetIdColumn(type);
            int count = -1;

            DbConnection connection = null;

            try
            {
                connection = OpenConnection();
                string sql = string.Format("select count({0}) from {1} where id = {2}", idColumn, tableName, id);
                count = Convert.ToInt32(ExecuteScalar(sql, connection));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    CloseConnection(connection);
                }
            }

            return count;
        }

        #endregion

        #region Object Mapping Information / General Helpers

        /// <summary>
        /// Determines whether the specified <see cref="PropertyInfo"/> describes a <see cref="Nullable"/> type.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/> to evaluate.</param>
        /// <returns>
        ///   <c>true</c> the specified <see cref="PropertyInfo"/> describes
        ///   a <see cref="Nullable"/> type; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsNullableProperty(PropertyInfo info) 
        {
            if (!info.PropertyType.IsGenericType)
            {
                return false;
            }

            return info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Determines, whether <paramref name="checkType"/> implements the specified <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="checkType">The <see cref="Type"/> to be checked.</param>
        /// <param name="interfaceType">The <see cref="Type"/> of the interface.</param>
        /// <returns><c>true</c>, if <paramref name="checkType"/> 
        /// implements <paramref name="interfaceType"/>; otherwise <c>false</c> is returned.</returns>
        protected bool ImplementsInterfaceType(Type checkType, Type interfaceType)
        {
            return checkType.GetInterfaces().Any(i => i == interfaceType);
        }

        /// <summary>
        /// Determines whether the specified object <paramref name="type"/>
        /// has references to other <see cref="DomainObject"/> types according
        /// to the defined object mapping to database.
        /// </summary>
        /// <param name="type">The type to evaluated.</param>
        /// <exception cref="PersistenceException">Thrown, if no mapping or was defined for specified <paramref name="type"/>.</exception>
        /// <returns>
        ///   <c>true</c> if the specified object <paramref name="type"/> references
        /// to other <see cref="DomainObject"/>; otherwise, <c>false</c>.
        /// </returns>
        internal bool HasReferencedType(Type type)
        {
            return GetMap(type).HasReferencedTypes();
        }

        /// <summary>
        /// Gets the referenced type <see cref="DbMemberMap"/> instances for specified type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to evaluate referenced types for.</param>
        /// <returns>The set of <see cref="DbMemberMap"/> providing information about referenced types.</returns>
        internal IEnumerable<DbMemberMap> GetReferencedTypeMembers(Type type)
        {
            return GetMap(type).GetReferencedTypes();
        }

        /// <summary>
        /// Gets the name of the columns to be loaded by default for specified object type <paramref name="type"/>,
        /// as specified in the object mapping to database.
        /// </summary>
        /// <exception cref="PersistenceException">Thrown, if no mapping or no columns
        /// were defined for specified type <paramref name="type"/>.</exception>
        /// <returns>The name of the columns to be loaded by default 
        /// for specified object type <paramref name="type"/>.</returns>
        protected string[] GetInitiallyLoadedColumns(Type type)
        {
            string[] columns = GetMap(type).GetInitiallyLoadedColumns();

            if (columns == null || columns.Length == 0)
            {
                throw new PersistenceException(string.Format("No columns configured for initially loading in mapping for type {0}!",
                                                                type.FullName), type.Name);
            }

            return columns;
        }

        /// <summary>
        /// Gets the table name of specified object type <paramref name="type"/>, 
        /// that is specified in the object mapping to database.
        /// </summary>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <paramref name="type"/>.</exception>
        /// <returns>The database table name mapping the specified object type <paramref name="type"/> on database.</returns>
        protected string GetTableName(Type type)
        {
            return GetMap(type).Table;
        }

        /// <summary>
        /// Gets the ID column of specified object type <paramref name="type"/>, that is specified
        /// in the object mapping to database.
        /// </summary>
        /// <exception cref="PersistenceException">Thrown, if no mapping or no ID column was defined in object mapping 
        /// of type <paramref name="type"/> to database.</exception>
        /// <returns>The name of the ID column of specified object type <paramref name="type"/>.</returns>
        protected string GetIdColumn(Type type)
        {
            string idColumn = GetMap(type).GetIdColumnName();

            if (string.IsNullOrEmpty(idColumn))
            {
                throw new PersistenceException(string.Format("No id column defined in mapping for type {0}!",
                                                                type.FullName), type.Name);
            }

            return idColumn;
        }

        /// <summary>
        /// Gets the <see cref="DbTableMap"/> specifying the object mapping to database.
        /// </summary>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <paramref name="type"/>.</exception>
        /// <returns>The appropriate <see cref="DbTableMap"/> specifying the object mapping.</returns>
        internal DbTableMap GetMap(Type type)
        {
            DbTableMap map = Config.GetMap(type);
            if (map == null)
            {
                throw new PersistenceException(string.Format("No table mapping found for class {0}!", type.FullName), type.Name);
            }

            return map;
        }

        #endregion
    }
}
