using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class PlayerDao : Dao
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime Birthday { get; set; }

        public PlayerReferenceDao ReferenceDao { get; set; }
        public PlayerDataHistoryDao DataHistoryDao { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDao"/> class.
        /// <summary>
        /// Use <see cref="DaoFactory.CreateDao&ltT&gt"/> to create appropriate DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal PlayerDao()
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
            return new Player(this);
        }

        #endregion
    }
}
