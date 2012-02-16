using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 09:56
    /// Specifies a certain performance measurement <see cref="FeatureSubCategory"/>.
    /// </summary>
    public class FeatureSubCategory : DomainObject
    {
        /// <summary>
        /// Gets the nice name of the <see cref="FeatureSubCategory"/> for display purposes.
        /// </summary>
        /// <value>
        /// The the nice name for display purposes.
        /// </value>
        public string NiceName { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="FeatureCategory"/> is a sub category.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this <see cref="FeatureCategory"/> is sub category; otherwise, <c>false</c>.
        /// </value>
        public bool IsSubCategory { get { return true; } }

        internal FeatureSubCategory() : base()
        {
        }

        internal FeatureSubCategory(FeatureSubCategoryDao dao)
            : base(dao)
        { }
    }
}
