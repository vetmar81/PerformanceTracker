using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbMemberMap
    {
        private readonly string namespaceQualifier;

        internal string Column { get; private set; }
        internal string Name { get; private set; }
        internal string Type { get; private set; }
        internal bool IsInitiallyLoaded { get; private set; }
        internal bool IsForeignKey { get; private set; }

        internal bool IsReferencedType
        {
            get { return !string.IsNullOrEmpty(Type); }
        }

        internal DbMemberMap(XmlNode node, string namespaceQualifier)
        {
            this.namespaceQualifier = namespaceQualifier;

            Init(node);
        }

        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Column = XmlHelper.HasAttribute(node, "column") ? XmlHelper.GetStringValue(node, "column") : string.Empty;
                Name = XmlHelper.HasAttribute(node, "name") ? XmlHelper.GetStringValue(node, "name") : string.Empty;
                Type = XmlHelper.HasAttribute(node, "type") ?
                    string.Concat(namespaceQualifier, ".", XmlHelper.GetStringValue(node, "type")) 
                    : string.Empty;
                IsForeignKey = XmlHelper.HasAttribute(node, "isForeignKey") ? XmlHelper.GetBooleanValue(node, "isForeignKey") : false;
                IsInitiallyLoaded = (XmlHelper.HasAttribute(node, "initiallyLoaded")) ?
                                        XmlHelper.GetBooleanValue(node, "initiallyLoaded") : true;
            }
        }
   } 
}
