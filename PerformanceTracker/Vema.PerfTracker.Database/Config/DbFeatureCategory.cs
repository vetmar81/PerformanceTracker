using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbFeatureCategory
    {
        internal int Id { get; private set; }
        internal string NiceName { get; private set; }
        internal IList<DbFeatureSubCategory> SubCategories { get; private set; }

        internal DbFeatureCategory(XmlNode node)
        {
            SubCategories = new List<DbFeatureSubCategory>();
            Init(node);
        }

        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Id = int.Parse(node.Attributes.GetNamedItem("id").Value);
                NiceName = node.Attributes.GetNamedItem("name").Value;

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
