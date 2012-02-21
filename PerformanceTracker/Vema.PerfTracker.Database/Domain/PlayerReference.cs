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
    public class PlayerReference : DomainObject, ITemporal
    {
        public PlayerReferenceDao Dao
        {
            get { return dao as PlayerReferenceDao; }
        }

        /// <summary>
        /// Gets the list of <see cref="Measurements"/> linked to this <see cref="PlayerReference"/>.
        /// </summary>
        public List<Measurement> Measurements { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Team"/> linked to this <see cref="PlayerReference"/>.
        /// </summary>
        public Team Team { get; internal set; }

        /// <summary>
        /// Gets the <see cref="Player"/> linked to this <see cref="PlayerReference"/>.
        /// </summary>
        public Player Player { get; internal set; }

        /// <summary>
        /// Gets / sets the valid from date.
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets / sets the valid to date.
        /// </summary>
        public DateTime ValidTo { get;  set; }

        internal PlayerReference() : base()
        {
            Measurements = new List<Measurement>();
        }

        internal PlayerReference(PlayerReferenceDao dao)
            : base(dao)
        {
            ValidFrom = dao.ValidFrom;
            ValidTo = dao.ValidTo;

            Measurements = new List<Measurement>();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}], PlayerId: {2}, TeamId: {3}, ValidFrom: '{4}' ValidTo: '{5}'",
                                GetType().Name, Id, Player.Id, Team.Id, ValidFrom.ToString(), ValidTo.ToString());
        }
    }
}
