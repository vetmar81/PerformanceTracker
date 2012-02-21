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
        public TeamDao Dao
        {
            get { return dao as TeamDao; }
        }

        public List<PlayerReference> References { get; internal set; }

        /// <summary>
        /// Gets the descriptor of this <see cref="Team"/>.
        /// </summary>
        public string Descriptor { get; internal set; }

        /// <summary>
        /// Gets the age group this <see cref="Team"/> covers.
        /// </summary>
        public string AgeGroup { get; internal set; }

        /// <summary>
        /// Gets / sets the valid from date.
        /// </summary>
        public DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets / sets the valid to date.
        /// </summary>
        public DateTime ValidTo { get; set; }

        internal Team() : base()
        {
            References = new List<PlayerReference>();
        }

        internal Team(TeamDao dao)
            : base(dao)
        {
            Descriptor = dao.Descriptor;
            AgeGroup = dao.AgeGroup;
            ValidFrom = dao.ValidFrom;
            ValidTo = dao.ValidTo;

            References = new List<PlayerReference>();

            if (dao.PlayerReferenceDaoList != null & dao.PlayerReferenceDaoList.Count > 0)
            {
                foreach (PlayerReferenceDao referenceDao in dao.PlayerReferenceDaoList)
                {
                    PlayerReference reference = (PlayerReference) referenceDao.CreateDomainObject();
                    reference.Team = this;

                    Player player = (Player) referenceDao.PlayerDao.CreateDomainObject();
                    reference.Player = player;

                    References.Add(reference);
                }
            } 
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
