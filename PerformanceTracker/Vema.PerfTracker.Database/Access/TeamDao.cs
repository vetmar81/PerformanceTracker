using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class TeamDao : Dao
    {
        public List<PlayerReferenceDao> PlayerReferenceDaoList { get; private set; }

        public string Descriptor { get; private set; }
        public string AgeGroup { get; private set; }

        public DateTime ValidFrom { get; private set; }
        public DateTime ValidTo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDao"/> class.
        /// Use <see cref="DaoFactory.CreateDao&lt;T&gt;"/> to create appropriate DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal TeamDao()
            : base()
        { }

        #region Dao Members

        /// <summary>
        /// Creates the corresponding <see cref="DomainObject"/>.
        /// </summary>
        /// <returns>
        /// the corresponding <see cref="DomainObject"/>.
        /// </returns>
        public override DomainObject CreateDomainObject()
        {
            return new Team(this);
        }

        #endregion
    }
}
