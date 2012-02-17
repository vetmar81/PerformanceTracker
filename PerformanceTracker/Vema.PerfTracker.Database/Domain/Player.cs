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
        public PlayerReference PlayerReference { get; internal set; }

        internal PlayerDataHistory DataHistory { get; set; }

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
        public DateTime Birthday { get; internal set; }

        /// <summary>
        /// Gets the weight of the <see cref="Player"/> in kg.
        /// </summary>
        public double Weight 
        {
            get { return DataHistory.Weight; }
        }

        /// <summary>
        /// Gets the height of the <see cref="Player"/> in cm.
        /// </summary>
        public int Height
        {
            get { return DataHistory.Height; }
        }

        internal Player()
            : base()
        { }

        internal Player(PlayerDao dao)
            : base(dao)
        {
            FirstName = dao.FirstName;
            LastName = dao.LastName;
            Country = dao.Country;
            Birthday = dao.Birthday;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}], Name: '{2} {3}', Birthday: '{4}', Country: '{5}'",
                                    GetType().Name, Id, FirstName, LastName, Birthday.ToString("yyyy-MM-dd"), Country);
        }
    }
}
