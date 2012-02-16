using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbFeatureSubCategory
    {
        internal int Id { get; private set; }
        internal string NiceName { get; private set; }

        internal DbFeatureSubCategory(XmlNode node)
        {
            Init(node);
        }

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
