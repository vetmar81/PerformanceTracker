using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class FeatureCategoryDao : Dao
    {
        public string NiceName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureCategoryDao"/> class.
        /// Use <see cref="DaoFactory.CreateDao&ltT&gt"/> to create appropriate DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal FeatureCategoryDao()
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
            return new FeatureCategory(this);
        }

        #endregion
    }
}
