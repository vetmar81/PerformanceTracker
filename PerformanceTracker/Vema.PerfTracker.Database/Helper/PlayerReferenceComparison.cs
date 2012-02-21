using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 10:53
    /// Compares two database objects of type <see cref="PlayerReference"/> for content equality.
    /// </summary>
    internal class PlayerReferenceComparison : TemporalComparison<PlayerReference>
    {
        /// <summary>
        /// Determines whether the specified objects of type <see cref="PlayerReference"/> are equal
        /// regarding their content (property values).
        /// </summary>
        /// <param name="previous">The previous object.</param>
        /// <param name="current">The current object.</param>
        /// <returns>
        ///   <c>true</c> if the specified objects of type <see cref="PlayerReference"/> are equal
        ///   by means of their property value; otherwise, <c>false</c>.
        /// </returns>
        internal override bool IsEqual(PlayerReference previous, PlayerReference current)
        {
            bool test = (previous.Player.Id == current.Player.Id);
            test &= (previous.Team.Id == current.Team.Id);

            return test;
        }
    }
}
