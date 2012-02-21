using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    public class MeasurementDao : Dao
    {
        public PlayerReferenceDao PlayerReferenceDao { get; set; }

        public FeatureCategoryDao CategoryDao { get; set; }
        public FeatureSubCategoryDao SubCategoryDao { get; set; }

        public MeasurementUnit Unit { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementDao"/> class.
        /// Use <see cref="DaoFactory.CreateDao&lt;T&gt;"/> to create appropriate 
        /// DAO for specified <see cref="DomainObject"/>.
        /// </summary>
        internal MeasurementDao()
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
            return new Measurement(this);
        }

        
        #endregion
    }
}
