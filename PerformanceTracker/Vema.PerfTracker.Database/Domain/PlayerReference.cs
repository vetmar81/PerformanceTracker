using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:48
    /// Domain object representing a player reference 
    /// beteween a <see cref="Player"/> and a <see cref="Team"/> instance.
    /// </summary>
    public class PlayerReference : DomainObject
    {
        private Player player;
        private Team team;

        /// <summary>
        /// Gets a value indicating whether this <see cref="PlayerReference"/> is current.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this <see cref="PlayerReference"/> is current, i.e. no newer reference exists; otherwise, <c>false</c>.
        /// </value>
        public bool IsCurrent { get; internal set; }

        internal PlayerReference() : base()
        { 
        }

        internal PlayerReference(PlayerReferenceDao dao)
            : base(dao)
        { }
    }
}
