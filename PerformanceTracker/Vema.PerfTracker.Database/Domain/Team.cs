using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:47
    /// Domain object representing a <see cref="Team"/> consisting of <see cref="Player"/> instances.
    /// </summary>
    public class Team : DomainObject, ITemporal
    {
        public List<PlayerReference> PlayerReferences { get; internal set; }

        /// <summary>
        /// Gets the descriptor of this <see cref="Team"/>.
        /// </summary>
        public string Descriptor { get; internal set; }

        /// <summary>
        /// Gets the age group this <see cref="Team"/> covers.
        /// </summary>
        public string AgeGroup { get; internal set; }

        /// <summary>
        /// Gets the valid from date.
        /// </summary>
        public DateTime ValidFrom { get; internal set; }

        /// <summary>
        /// Gets the valid to date.
        /// </summary>
        public DateTime ValidTo { get; internal set; }

        internal Team() : base()
        {
            PlayerReferences = new List<PlayerReference>();
        }

        internal Team(TeamDao dao)
            : base(dao)
        {
            Descriptor = dao.Descriptor;
            AgeGroup = dao.AgeGroup;
            ValidFrom = dao.ValidFrom;
            ValidTo = dao.ValidTo;

            PlayerReferences = new List<PlayerReference>();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}], Descriptor: '{2}', AgeGroup: '{3}', ValidFrom: {4}, ValidTo: {5}",
                                    GetType().Name, Id, Descriptor, AgeGroup, ValidFrom.ToString(), ValidTo.ToString());
        }
    }
}
