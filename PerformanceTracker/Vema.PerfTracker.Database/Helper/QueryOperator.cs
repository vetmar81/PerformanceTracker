using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Enumeration of different operators to be used in an SQL query.
    /// </summary>
    public enum QueryOperator
    {
        /// <summary>
        /// Represents the 'AND' operator
        /// </summary>
        And,
        /// <summary>
        /// Represents the 'OR' operator
        /// </summary>
        Or,
        /// <summary>
        /// Represents the '&lt' operator
        /// </summary>
        Smaller,
        /// <summary>
        /// Represents the '&lt=' operator
        /// </summary>
        SmallerEqual,
        /// <summary>
        /// Represents the '=' operator
        /// </summary>
        Equal,
        /// <summary>
        /// Represents the '&lt&gt' operator
        /// </summary>
        NotEqual,
        /// <summary>
        /// Represents the '&gt' operator
        /// </summary>
        Bigger,
        /// <summary>
        /// Represents the '&gt=' operator
        /// </summary>
        BiggerEqual,
        /// <summary>
        /// Represents the 'LIKE' operator
        /// </summary>
        Like,
        /// <summary>
        /// Represents no operator
        /// </summary>
        None
    }
}
