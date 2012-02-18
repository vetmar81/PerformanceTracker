using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class PlayerReferenceDao : Dao
    {
        public PlayerDao PlayerDao { get; set; }
        public TeamDao TeamDao { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerReferenceDao"/> class.
        /// <summary>
        /// Use <see cref="DaoFactory.CreateDao&ltT&gt"/> to create appropriate DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal PlayerReferenceDao()
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
            return new PlayerReference(this);
        }

        #endregion
    }
}
