using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class PlayerDataHistoryDao : Dao
    {
        public PlayerDao PlayerDao { get; set; }

        public double Weight { get; set; }
        public int Height { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDataHistoryDao"/> class.
        /// <summary>
        /// Use <see cref="DaoFactory.CreateDao&ltT&gt"/> to create appropriate DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal PlayerDataHistoryDao()
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
            return new PlayerDataHistory(this);
        }

        #endregion
    }
}
