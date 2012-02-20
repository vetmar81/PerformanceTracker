using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class FeatureSubCategoryDao : Dao
    {
        public FeatureCategoryDao CategoryDao { get; set; }
        public string NiceName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureSubCategoryDao"/> class.
        /// Use <see cref="DaoFactory.CreateDao&lt;T&gt;"/> to create appropriate DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal FeatureSubCategoryDao() 
            : base()
        { 
        }

        #region Dao Members

        /// <summary>
        /// Creates the corresponding <see cref="DomainObject"/>.
        /// </summary>
        /// <returns>
        /// the corresponding <see cref="DomainObject"/>.
        /// </returns>
        public override DomainObject CreateDomainObject()
        {
            return new FeatureSubCategory(this);
        }

        #endregion
    }
}
