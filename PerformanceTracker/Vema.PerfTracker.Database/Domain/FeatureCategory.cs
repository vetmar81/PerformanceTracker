using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 09:56
    /// Specifies a certain performance measurement <see cref="FeatureCategory"/>.
    /// </summary>
    public class FeatureCategory : DomainObject
    {
        public FeatureCategoryDao Dao
        {
            get
            {
                return dao as FeatureCategoryDao;
            }
        }

        public List<FeatureSubCategory> SubCategories { get; set; }

        /// <summary>
        /// Gets the nice name of the <see cref="FeatureCategory"/> for display purposes.
        /// </summary>
        /// <value>
        /// The the nice name for display purposes.
        /// </value>
        public string NiceName { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="FeatureCategory"/> is sub category.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this <see cref="FeatureCategory"/> is sub category; otherwise, <c>false</c>.
        /// </value>
        public bool IsSubCategory
        {
            get { return false; }
        }

        internal FeatureCategory() : base()
        {
            SubCategories = new List<FeatureSubCategory>();
        }

        internal FeatureCategory(FeatureCategoryDao dao)
            : base(dao)
        {
            if (!string.IsNullOrEmpty(dao.NiceName))
            {
                NiceName = dao.NiceName.Trim();
            }

            SubCategories = new List<FeatureSubCategory>();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}], NiceName: '{2}', IsSubCategory: {3}",
                                    GetType().Name, Id, NiceName, IsSubCategory);
        }
    }
}
