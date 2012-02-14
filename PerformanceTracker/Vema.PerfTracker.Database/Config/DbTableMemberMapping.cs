using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbTableMemberMapping
    {
        internal string Column { get; private set; }
        internal string Property { get; private set; }
        internal bool IsInitiallyLoaded { get; private set; }

        internal DbTableMemberMapping(XmlNode node)
        {
            Init(node);
        }

        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Column = node.Attributes.GetNamedItem("column").Value;
                Property = node.Attributes.GetNamedItem("property").Value;
                IsInitiallyLoaded = bool.Parse(node.Attributes.GetNamedItem("initialLoad").Value);
            }
        }
   } 
}
