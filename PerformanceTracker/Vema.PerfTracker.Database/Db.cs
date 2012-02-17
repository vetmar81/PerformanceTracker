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

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:59
    /// Abstract definition of any database implementation.
    /// </summary>
    public abstract class Db
    {
        internal DbConfig Config { get; set; }

        internal static DateTime CurrentDate = new DateTime(9998, 1, 1);

        #region Abstract Method Definitions

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

        #endregion

        #region Object Loading

        /// <summary>
        /// Loads the specified object of type <typeparamref name="T"/> by its database ID.
        /// </summary>
        /// <typeparam name="T">The requested object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <param name="id">The database ID of the object.</param>
        /// <exception cref="PersistenceException">Thrown, if mapping information incorrect or 
        /// if database ID of specified object is not unique.</exception>
        /// <returns>The requested object of type <typeparamref name="T"/> or <c>null</c>,
        /// if object with specified ID doesn't exist.</returns>
        public T LoadById<T>(long id) where T : DomainObject
        {
            T result = null;
            Type type = typeof(T);

            string tableName = GetTableName(type);
            string idColumn = GetIdColumn(type);
            string[] columns = GetInitiallyLoadedColumns(type);

            // Build the query using the ID constraint

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            QueryConstraint idConstraint = new QueryConstraint(idColumn, id, QueryOperator.Equal);

            try
            {
                // Check if the ID is unique

                if (IsUnique(id, type))
                {
                    OpenConnection();

                    string sql = builder.CreateSelectQuery(tableName, idConstraint, columns);

                    DbDataReader reader = ExecuteReader(sql);

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
                CloseConnection();
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
            T result = null;
            Type childType = typeof(T);

            string parentIdColumn = GetIdColumn(parentType);
            string foreignTable = GetTableName(childType);
            string foreignKeyColumn = GetMap(childType).GetForeignKeyColumn(parentType);
            string[] columns = GetInitiallyLoadedColumns(childType);

            QueryConstraint constraint = new QueryConstraint(foreignKeyColumn, parentId, QueryOperator.Equal);
            constraint.AppendConstraint(QueryOperator.And, new QueryConstraint("validto", CurrentDate, QueryOperator.Equal));

            try
            {
                OpenConnection();

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectQuery(foreignTable, constraint, columns);

                DbDataReader reader = ExecuteReader(sql);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    result = LoadObject<T>(reader);
                    reader.Close();

                    if (!IsUnique(result.Id, childType))
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
                CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Loads the currently valid reference object of kind <typeparamref name="T"/> representing temporal data.
        /// </summary>
        /// <typeparam name="T">The kind of temporal object;
        /// inherits <see cref="DomainObject"/> and implements <see cref="ITemporal"/>.</typeparam>
        /// <param name="parent">The parent reference <see cref="DomainObject"/>.
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

            try
            {
                OpenConnection();

                // Assembly the query

                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectQuery(table, constraint, columns);

                // Get the result set

                DbDataReader reader = ExecuteReader(sql);

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        T t = LoadObject<T>(reader);
                        resultList.Add(t);
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
            Type type = typeof(T);

            // Get info about object mapping

            string table = GetTableName(type);
            string[] columns = GetInitiallyLoadedColumns(type);

            try
            {
                OpenConnection();

                // Assemble the query respecting the where clause

                QueryBuilder builder = new QueryBuilder(QueryType.Select);

                string sql = builder.CreateSelectQuery(table, null, columns);
                sql = string.Concat(sql, " where ", whereClause);

                // Get the result set

                DbDataReader reader = ExecuteReader(sql);

                if (reader != null && reader.HasRows)
                {
                    // Load objects

                    while (reader.Read())
                    {
                        T t = LoadObject<T>(reader);
                        resultList.Add(t);
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
                CloseConnection();
            }

            return resultList;
        }

        #endregion        

        public void LoadProperty<T>(T obj, string name, DbDataReader reader) where T : DomainObject
        {
            Dao dao = DaoFactory.CreateDao<T>();
            LoadProperty<T>(obj, dao, name, reader);
        }

        public void LoadProperty<T>(T obj, Dao dao, string propertyName, DbDataReader reader) where T : DomainObject
        {
            dao.LoadMember(obj, propertyName, reader);
        }  

        /// <summary>
        /// Determines whether the specified ID is unique for objects of <paramref name="type"/>.
        /// </summary>
        /// <param name="id">The ID to ve evaluated for uniqueness.</param>
        /// <returns>
        ///   <c>true</c> if the specified ID is unique; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUnique(long id, Type type)
        {
            return GetCount(id, type) == 1;
        }

        #region Object Mapping Information / General Helpers

        /// <summary>
        /// Gets the count of objects of <paramref name="type"/> with specified ID.
        /// </summary>
        /// <param name="id">The id to be evaluated.</param>
        /// <returns>The count of database entries of type <paramref name="type"/> providing the specidfied ID.</returns>
        private int GetCount(long id, Type type)
        {
            string tableName = GetTableName(type);
            string idColumn = GetIdColumn(type);
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
            return LoadObject<T>(reader, GetMap(typeof(T)));
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

        /// <summary>
        /// Determines whether the specified object <paramref name="type"/>
        /// has references to other <see cref="DomainObject"/> types according
        /// to the defined object mapping to database.
        /// </summary>
        /// <param name="type">The type to evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified object <paramref name="type"/> references
        /// to other <see cref="DomainObject"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="PersistenceException">Thrown, if no mapping or was defined for specified type <typeparamref name="T"/>.</exception>
        private bool HasReferencedType(Type type)
        {
            return GetMap(type).HasReferencedTypes();
        }

        /// <summary>
        /// Gets the referenced type <see cref="DbMemberMap"/> instances for specified type <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to evaluate referenced types for.</param>
        /// <returns>The set of <see cref="DbMemberMap"/> providing information about referenced types.</returns>
        private IEnumerable<DbMemberMap> GetReferencedTypeMembers(Type type)
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
        private string[] GetInitiallyLoadedColumns(Type type)
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
        private string GetTableName(Type type)
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
        private string GetIdColumn(Type type)
        {
            string idColumn = GetMap(type).GetIdColumn();

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
        /// <typeparam name="T">The affected object type; inherits from <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="PersistenceException">Thrown, if no mapping was defined for specified type <typeparamref name="T"/>.</exception>
        /// <returns>The appropriate <see cref="DbTableMap"/> specifying the object mapping.</returns>
        private DbTableMap GetMap(Type type)
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
