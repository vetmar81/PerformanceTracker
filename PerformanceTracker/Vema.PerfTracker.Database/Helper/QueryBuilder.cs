using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Config;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 15:12
    /// Utility class that facilities assembly of simple SQL queries.
    /// </summary>
    internal class QueryBuilder
    {
        /// <summary>
        /// Separator character for queries
        /// </summary>
        internal static char Separator = ',';

        private readonly StringBuilder builder;
        private readonly QueryType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        /// <param name="type">The requested <see cref="QueryType"/>.</param>
        internal QueryBuilder(QueryType type)
        {
            this.type = type;
            builder = new StringBuilder();

            Init();
        }

        #region Static Utility methods

        /// <summary>
        /// Creates the necessary SQL statement to create the next value in a sequence definition.
        /// </summary>
        /// <param name="map">The <see cref="DbTableMap"/> providing the relevant information.</param>
        /// <returns>The appropriate SQL statement.</returns>
        internal static string CreateNextSequenceValueSql(DbTableMap map)
        {
            return (map.HasSequence) ? string.Format("select nextval('{0}.{1}')", map.Schema, map.Sequence) : string.Empty;
        }

        /// <summary>
        /// Gets the SQL statement to generate the next unoccupied ID value for tables without auto-increment.
        /// </summary>
        /// <param name="map">The <see cref="DbTableMap"/> providing the relevant information.</param>
        /// <returns>The appropriate SQL statement.</returns>
        internal static string GetNextIdValueSql(DbTableMap map)
        {
            string idColumn = map.GetIdColumn();
            return string.Format("select max({0}} + 1 from {1}", idColumn, map.Table);
        }

        /// <summary>
        /// Formats the specified value into its expected <see cref="string"/> representation for query integration.
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <returns>The formatted value.</returns>
        internal static string FormatValue(object value)
        {
            return FormatValue(value, false);
        }

        /// <summary>
        /// Formats the specified value into its expected <see cref="string"/> representation for query integration.
        /// </summary>
        /// <param name="value">The value to be formatted.</param>
        /// <param name="useDateAndTime">if set to <c>true</c> date and time parts of any <see cref="DateTime"/>
        /// parameter are respected; otherwise only date part to be formatted.</param>
        /// <returns>
        /// The formatted value.
        /// </returns>
        internal static string FormatValue(object value, bool useDateAndTime)
        {
            // TODO: Support for further data types? (e.g. BLOB)

            if (value.GetType() == typeof(DateTime))
            {
                string dateTimeString;

                if (useDateAndTime)
                {
                    dateTimeString = ((DateTime) value).ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                else
                {
                    dateTimeString = ((DateTime) value).ToString("yyyy-MM-dd");
                }

                return string.Format("'{0}'", dateTimeString);
            }
            else if (value.GetType() == typeof(string))
            {
                return string.Format("'{0}'", value.ToString());
            }
            else
            {
                return value.ToString();
            }
        }

        #endregion         

        #region Query assembly methods

        /// <summary>
        /// Creates a SQL query to select all columns / records from a table.
        /// </summary>
        /// <param name="tableName">Name of the table to select records for.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Select"/>.</exception>
        /// <returns>The SQL query.</returns>
        internal string CreateSelectSql(string tableName)
        {
            return CreateSelectSql(tableName, null);
        }

        /// <summary>
        /// Creates a SQL query to select some columns matching the specified <see cref="QueryConstraint"/> from a table.
        /// </summary>
        /// <param name="tableName">Name of the table to select records for.</param>
        /// <param name="constraint">The <see cref="QueryConstraint"/> to be applied for record selection. 
        /// If <paramref name="constraint"/> is <c>null</c>, no constraints are respected.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Select"/>.</exception>
        /// <returns>
        /// The SQL query.
        /// </returns>
        internal string CreateSelectSql(string tableName, QueryConstraint constraint)
        {
            return CreateSelectSql(tableName, constraint, null);
        }

        /// <summary>
        /// Creates a SQL query to select some columns matching the specified <see cref="QueryConstraint"/> from a table.
        /// </summary>
        /// <param name="tableName">Name of the table to select records for.</param>
        /// <param name="constraint">The <see cref="QueryConstraint"/> to be applied for record selection.
        /// If <paramref name="constraint"/> is <c>null</c>, no constraints are respected.</param>
        /// <param name="columns">The columns to be respected within selection.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Select"/>.</exception>
        /// <returns>
        /// The SQL query.
        /// </returns>
        internal string CreateSelectSql(string tableName, QueryConstraint constraint, params string[] columns)
        {
            // Throw exception, if this method is called for wrong query type

            if (type != QueryType.Select)
            {
                throw new InvalidOperationException(string.Format("Can't create select SQL statement for {0}!",
                                                                    Enum.GetName(typeof(QueryType), type)));
            }

            StringBuilder columnBuilder = new StringBuilder();

            // Define table / columns to be respected

            if (columns == null || columns.Length == 0)
            {
                // No columns specified => select all columns

                builder.Append(string.Format(" * from {0}", tableName));
            }
            else
            {
                // Define table and columns to respected

                foreach (var column in columns)
                {
                    columnBuilder.Append(column).Append(Separator);
                }
                builder.Append(columnBuilder.ToString().Trim(Separator));
                builder.AppendLine(string.Format(" from {0} ", tableName));
            }

            // No constraint specified => select all records

            if (constraint != null && !string.IsNullOrEmpty(constraint.Get()))
            {
                builder.Append(string.Format(" where {0}", constraint.Get()));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates a SQL query to insert values into a specified table.
        /// </summary>
        /// <param name="tableName">Name of the table to insert values for.</param>
        /// <param name="columnValuePairs">The set of column / value pairs defining,
        /// which value shall be inserted into which column.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Insert"/>.</exception>
        /// <returns>The SQL query.</returns>
        internal string CreateInsertSql(string tableName, IEnumerable<Pair<string, object>> columnValuePairs)
        {
            // Throw exception, if this method is called for wrong query type

            if (type != QueryType.Insert)
            {
                throw new InvalidOperationException(string.Format("Can't create insert SQL statement for {0}!",
                                                                    Enum.GetName(typeof(QueryType), type)));
            }

            // Define table

            builder.AppendLine(tableName);

            StringBuilder columnBuilder = new StringBuilder();
            StringBuilder valueBuilder = new StringBuilder();

            // Define columns

            builder.Append("(");
            foreach (var pair in columnValuePairs)
            {
                columnBuilder.Append(string.Format("{0}{1}", pair.Left, Separator));
            }
            string columns = columnBuilder.ToString().Trim(Separator);
            builder.Append(columns);
            builder.Append(") values (");

            // Define values to be inserted

            foreach (var pair in columnValuePairs)
            {
                valueBuilder.Append(string.Format("{0}{1}", FormatValue(pair.Right), Separator));
            }
            string values = valueBuilder.ToString().Trim(Separator);
            builder.Append(values);
            return builder.Append(")").ToString();
        }

        /// <summary>
        /// Creates a SQL query to update a single record.
        /// </summary>
        /// <param name="tableName">Name of the table to update.</param>
        /// <param name="updateColumnValues">The column values to be updated.</param>
        /// <param name="constraint">The <see cref="QueryConstraint"/> defining update constraints.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Update"/>.</exception>
        /// <returns>The SQL query.</returns>
        internal string CreateUpdateSql(string tableName, IEnumerable<Pair<string, object>> updateColumnValues, QueryConstraint constraint)
        {
            // Throw exception, if this method is called for wrong query type

            if (type != QueryType.Update)
            {
                throw new InvalidOperationException(string.Format("Can't create update SQL statement for {0}!",
                                                                    Enum.GetName(typeof(QueryType), type)));
            }

            // Define table

            builder.AppendLine(tableName);
            builder.Append(" set ");

            StringBuilder updateValueBuilder = new StringBuilder();

            // Define column values to be updated

            foreach (var updateColumnValue in updateColumnValues)
            {
                updateValueBuilder.Append(string.Format("{0} = {1}{2}",
                                                        updateColumnValue.Left,
                                                        FormatValue(updateColumnValue.Right),
                                                        Separator)
                                            );
            }
            builder.AppendLine(updateValueBuilder.ToString().Trim(Separator));

            // Get the constraints, append them to where clause and return

            return builder.Append(string.Format(" where {0}", constraint.Get())).ToString();
        }

        /// <summary>
        /// Creates an SQL query to delete all records from specified table.
        /// </summary>
        /// <param name="tableName">Name of the table to delete all records for.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Delete"/>.</exception>
        /// <returns>The SQL query.</returns>
        internal string CreateDeleteSql(string tableName)
        {
            return CreateDeleteSql(null);
        }

        /// <summary>
        /// Creates an SQL query to delete records from specified table matching the
        /// specified <see cref="QueryConstraint"/>.
        /// </summary>
        /// <param name="tableName">Name of the table to delete records for..</param>
        /// <param name="constraint">The <see cref="QueryConstraint"/> providing constraints.</param>
        /// <exception cref="InvalidOperationException">Thrown, if this method is 
        /// called for a <see cref="QueryType"/> other than <see cref="QueryType.Delete"/>.</exception>
        /// <returns>The SQL query.</returns>
        internal string CreateDeleteSql(string tableName, QueryConstraint constraint)
        {
            // Throw exception, if this method is called for wrong query type

            if (type != QueryType.Delete)
            {
                throw new InvalidOperationException(string.Format("Can't create delete SQL statement for {0}!",
                                                                    Enum.GetName(typeof(QueryType), type)));
            }

            if (constraint == null)
            {
                // Defining no constraints tries to delete all records => DELETE FROM [TABLE]

                return builder.Append(tableName).ToString();
            }

            // Append constraints to where clause and return

            return builder.Append(string.Format(" where {0}", constraint.Get())).ToString();
        }  

        #endregion    

        /// <summary>
        /// Basic init of query structure.
        /// </summary>
        private void Init()
        {
            switch (type)
            {
                case QueryType.Select:
                    builder.Append("select ");
                    break;
                case QueryType.Insert:
                    builder.Append("insert into ");
                    break;
                case QueryType.Update:
                    builder.Append("update ");
                    break;
                case QueryType.Delete:
                    builder.Append("delete from");
                    break;
            }
        }
    }
}
