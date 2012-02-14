using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:47
    /// Domain object representing a <see cref="Team"/> consisting of <see cref="Player"/> instances.
    /// </summary>
    public class Team : DomainObject
    {
        private List<PlayerReference> playerReferences;

        /// <summary>
        /// Gets the descriptor of this <see cref="Team"/>.
        /// </summary>
        public string Descriptor { get; internal set; }

        /// <summary>
        /// Gets the age group this <see cref="Team"/> covers.
        /// </summary>
        public string AgeGroup { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Team"/> is flagged as deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this <see cref="Team"/> is flagged as deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; internal set; }
    }
}
