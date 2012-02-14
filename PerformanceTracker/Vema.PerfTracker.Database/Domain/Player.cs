using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:45
    /// Domain object representing a single <see cref="Player"/>.
    /// </summary>
    public class Player : DomainObject
    {
        private PlayerDataHistory dataHistory;
        private List<PlayerReference> playerReferences;

        /// <summary>
        /// Gets the first name of the <see cref="Player"/>.
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        /// Gets the last name of the <see cref="Player"/>.
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        /// Gets the country of the <see cref="Player"/>.
        /// </summary>
        public string Country { get; internal set; }

        /// <summary>
        /// Gets the date of birth of the <see cref="Player"/>.
        /// </summary>
        public DateTime DateOfBirth { get; internal set; }

        /// <summary>
        /// Gets the weight of the <see cref="Player"/> in kg.
        /// </summary>
        public double Weight 
        {
            get { return dataHistory.Weight; }
        }

        /// <summary>
        /// Gets the height of the <see cref="Player"/> in cm.
        /// </summary>
        public int Height
        {
            get { return dataHistory.Height; }
        }

        internal Player()
            : base()
        { }

        internal Player(PlayerDao dao)
            : base(dao)
        {
        }
    }
}
