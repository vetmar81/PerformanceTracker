using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    /// <summary>
    /// Markus Vetsch 14.02.2012 00:29
    /// Represents any feature category definition within database configuration file. 
    /// </summary>
    internal class DbFeatureCategory
    {
        /// <summary>
        /// Gets the id of the feature category.
        /// </summary>
        internal int Id { get; private set; }

        /// <summary>
        /// Gets the nice name for display purposes.
        /// </summary>
        /// <value>
        /// The nice name.
        /// </value>
        internal string NiceName { get; private set; }

        /// <summary>
        /// Gets the sub categories of current feature category.
        /// </summary>
        internal IList<DbFeatureSubCategory> SubCategories { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbFeatureCategory"/> class.
        /// </summary>
        /// <param name="node">The affiliated <see cref="XmlNode"/>.</param>
        internal DbFeatureCategory(XmlNode node)
        {
            SubCategories = new List<DbFeatureSubCategory>();
            Init(node);
        }

        /// <summary>
        /// Initalizes the configuration values of given <paramref name="node"/>.
        /// </summary>
        /// <param name="node">The affiliated <see cref="XmlNode"/>.</param>
        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Id = XmlHelper.GetIntValue(node, "id");
                NiceName = XmlHelper.GetStringValue(node, "name");

                XmlNodeList subCategoryNodeList = node.SelectNodes("SubCategory");

                if (subCategoryNodeList != null)
                {
                    foreach (XmlNode subCategoryNode in subCategoryNodeList)
                    {
                        SubCategories.Add(new DbFeatureSubCategory(subCategoryNode));
                    }
                }
            }
        }
    }
}
