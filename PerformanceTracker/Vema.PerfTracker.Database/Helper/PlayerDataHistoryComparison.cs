using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 10:52
    /// Compares two database objects of type <see cref="PlayerDataHistory"/> for content equality.
    /// </summary>
    internal class PlayerDataHistoryComparison : TemporalComparison<PlayerDataHistory>
    {
        /// <summary>
        /// Determines whether the specified objects of type <see cref="PlayerDataHistory"/> are equal
        /// regarding their content (property values).
        /// </summary>
        /// <param name="previous">The previous object.</param>
        /// <param name="current">The current object.</param>
        /// <returns>
        ///   <c>true</c> if the specified objects of type <see cref="PlayerDataHistory"/> are equal
        ///   by means of their property value; otherwise, <c>false</c>.
        /// </returns>
        internal override bool IsEqual(PlayerDataHistory previous, PlayerDataHistory current)
        {
            bool test = (previous.Height == current.Height);
            test &= (previous.Weight == current.Weight);
            test &= (previous.Remark == current.Remark);

            return test;
        }
    }
}
