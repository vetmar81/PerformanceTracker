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
        public PlayerDao PlayerDao { get; set; }
        public TeamDao TeamDao { get; set; }
        public PlayerReferenceDao PlayerReferenceDao { get; set; }

        public FeatureCategoryDao CategoryDao { get; set; }
        public FeatureSubCategoryDao SubCategoryDao { get; set; }

        public MeasurementUnit Unit { get; set; }
        public double Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Remark { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementDao"/> class.
        /// <summary>
        /// Use <see cref="DaoFactory.CreateDao&ltT&gt"/> to create appropriate 
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
