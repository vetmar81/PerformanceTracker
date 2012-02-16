using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch 14.02.2012 13:21
    /// Helper class for building constraints for a query in a generic way.
    /// The idea is to create either an empty <see cref="QueryConstraint"/> using the default constructor 
    /// and then add other <see cref="QueryConstraint"/> afterwards using the <see cref="QueryConstraint.AppendConstraint"/> 
    /// or <see cref="QueryConstraint.AppendConstraints"/> methods. As an alternative, a <see cref="QueryConstraint"/> using the
    /// parameterized constructor may be used and then further <see cref="QueryConstraint"/> may be appended.
    /// </summary>
    public class QueryConstraint
    {
        private readonly string columnName;
        private readonly object constraintValue;
        private readonly QueryOperator op;

        private StringBuilder constraintBuilder;

        public static QueryConstraint Empty = new QueryConstraint();

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryConstraint"/> class.
        /// </summary>
        public QueryConstraint() : this(string.Empty, null, QueryOperator.None)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryConstraint"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column the constraint applies to.</param>
        /// <param name="constraintValue">The constraint value.</param>
        /// <param name="op">The <see cref="QueryOperator"/> used by the constraint.</param>
        public QueryConstraint(string columnName, object constraintValue, QueryOperator op)
        {
            constraintBuilder = new StringBuilder();

            this.columnName = columnName;
            this.constraintValue = constraintValue;
            this.op = op;

            if (!string.IsNullOrEmpty(ToString()))
            {
                AppendConstraint(this);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(columnName) || this.op == QueryOperator.None || constraintValue == null)
            {
                return string.Empty;
            }

            return string.Format("{0} {1} {2}", columnName, GetOperatorChar(this.op), QueryBuilder.FormatValue(constraintValue));
        }

        /// <summary>
        /// Gets the entire constraint sequence as <see cref="string"/>.
        /// </summary>
        /// <returns>The entire constraint sequence.</returns>
        public string Get()
        {
            return constraintBuilder.ToString();
        }

        /// <summary>
        /// Appends the specified set of <see cref="QueryConstraint"/> instances linked
        /// by the associated <see cref="QueryOperator"/>.
        /// </summary>
        /// <param name="constraints">The combination of <see cref="QueryConstraint"/> instances 
        /// and a linking <see cref="QueryOperator"/>.</param>
        public void AppendConstraints(IEnumerable<Pair<QueryOperator, QueryConstraint>> constraints)
        {
            foreach (var constraint in constraints)
            {
                AppendConstraint(constraint.Left, constraint.Right);
            }
        }

        /// <summary>
        /// Appends a single constraint without any operator.
        /// </summary>
        /// <param name="constraint">The affected <see cref="QueryConstraint"/>.</param>
        public void AppendConstraint(QueryConstraint constraint)
        {
            AppendConstraint(QueryOperator.None, constraint);
        }

        /// <summary>
        /// Appends a single <see cref="QueryConstraint"/> instances linked
        /// by the associated <see cref="QueryOperator"/>.
        /// </summary>
        /// <param name="op">The op.</param>
        /// <param name="constraint">The constraint.</param>
        public void AppendConstraint(QueryOperator op, QueryConstraint constraint)
        {
            if (op == QueryOperator.None)
            {
                constraintBuilder.AppendLine(constraint.ToString());
            }
            else
            {
                constraintBuilder.AppendLine(string.Format(" {0} {1}", GetOperatorChar(op), constraint.ToString()));
            }           
        }

        /// <summary>
        /// Acts as facade for a new <see cref="QueryConstraint"/> and appends it.
        /// </summary>
        /// <param name="linkOp">The operator linking the <see cref="QueryConstraint"/>.</param>
        /// <param name="columnName">Name of the column the constraint applies to.</param>
        /// <param name="constraintValue">The constraint value.</param>
        /// <param name="op">The <see cref="QueryOperator"/> used by the constraint.</param>
        public void AppendConstraint(QueryOperator linkOp, string columnName, object constraintValue, QueryOperator constraintOperator)
        {
            AppendConstraint(linkOp, new QueryConstraint(columnName, constraintValue, constraintOperator));
        }

        /// <summary>
        /// Gets the operator character as <see cref="string"/> for integration into the <see cref="QueryConstraint"/>.
        /// </summary>
        /// <param name="op">The <see cref="QueryOperator"/> to retrieve the character for.</param>
        /// <returns>The corresponding character.</returns>
        private static string GetOperatorChar(QueryOperator op)
        {
            switch (op)
            {
                case QueryOperator.Equal:
                    return "=";
                case QueryOperator.NotEqual:
                    return "<>";
                case QueryOperator.Smaller:
                    return "<";
                case QueryOperator.SmallerEqual:
                    return "<=";
                case QueryOperator.Bigger:
                    return ">";
                case QueryOperator.BiggerEqual:
                    return ">=";
                case QueryOperator.Like:
                    return "like";
                case QueryOperator.And:
                    return "and";
                case QueryOperator.Or:
                    return "or";
            }

            return string.Empty;
        }
    }
}
