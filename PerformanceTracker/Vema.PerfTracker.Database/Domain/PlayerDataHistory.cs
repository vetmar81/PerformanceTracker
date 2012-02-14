using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:46
    /// Domain object for additional temporally relevant player data.
    /// </summary>
    public class PlayerDataHistory : DomainObject
    {
        private Player player;

        /// <summary>
        /// Gets the weight of this entry.
        /// </summary>
        internal double Weight { get; private set; }

        /// <summary>
        /// Gets the height of this entry.
        /// </summary>
        internal int Height { get; private set; }

        /// <summary>
        /// Gets the time stamp of this entry.
        /// </summary>
        internal DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Gets the remark linked to this entry.
        /// </summary>
        internal string Remark { get; private set; }
    }
}
