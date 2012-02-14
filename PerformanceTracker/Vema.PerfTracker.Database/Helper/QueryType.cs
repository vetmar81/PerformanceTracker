using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Enumeration for different types of SQL query statements.
    /// </summary>
    internal enum QueryType
    {
        /// <summary>
        /// Represents a SELECT FROM statement.
        /// </summary>
        Select,
        /// <summary>
        /// Represents a INSERT INTO statement
        /// </summary>
        Insert,
        /// <summary>
        /// Represents a UPDATE statement.
        /// </summary>
        Update,
        /// <summary>
        /// Represents a DELETE FROM statement.
        /// </summary>
        Delete
    }
}
