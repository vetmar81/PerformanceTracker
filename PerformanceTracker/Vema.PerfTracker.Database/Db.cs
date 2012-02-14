using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:59
    /// Abstract definition of any database implementation.
    /// </summary>
    public abstract class Db
    {
        public DbConnection Connection {get; protected set;}

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
    }
}
