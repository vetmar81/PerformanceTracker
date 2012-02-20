using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:31
    /// Represents any feature sub category definition within database configuration file.
    /// </summary>
    internal class DbFeatureSubCategory
    {
        /// <summary>
        /// Gets the id of the feature sub category.
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
        /// Initializes a new instance of the <see cref="DbFeatureSubCategory"/> class.
        /// </summary>
        /// <param name="node">The affiliated <see cref="XmlNode"/>.</param>
        internal DbFeatureSubCategory(XmlNode node)
        {
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
            }
        }
    }
}
