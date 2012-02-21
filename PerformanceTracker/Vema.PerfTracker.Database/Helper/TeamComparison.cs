using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 10:53
    /// Compares two database objects of type <see cref="Team"/> for content equality.
    /// </summary>
    internal class TeamComparison : TemporalComparison<Team>
    {
        /// <summary>
        /// Determines whether the specified objects of type <see cref="Team"/> are equal
        /// regarding their content (property values).
        /// </summary>
        /// <param name="previous">The previous object.</param>
        /// <param name="current">The current object.</param>
        /// <returns>
        ///   <c>true</c> if the specified objects of type <see cref="Team"/> are equal
        ///   by means of their property value; otherwise, <c>false</c>.
        /// </returns>
        internal override bool IsEqual(Team previous, Team current)
        {
            bool test = (previous.Descriptor == current.Descriptor);
            test &= (previous.AgeGroup == current.AgeGroup);

            return test;
        }
    }
}
