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
        private readonly string tableName;

        internal string Column { get; private set; }
        internal string Property { get; private set; }
        internal string Type { get; private set; }
        internal bool IsInitiallyLoaded { get; private set; }
        internal bool IsForeignKey { get; private set; }

        internal bool IsReferencedType
        {
            get { return !string.IsNullOrEmpty(Type); }
        }

        internal DbMemberMap(XmlNode node, string tableName)
        {
            this.tableName = tableName;

            Init(node);
        }

        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Column = XmlHelper.HasAttribute(node, "column") ? XmlHelper.GetStringValue(node, "column") : string.Empty;
                Property = XmlHelper.HasAttribute(node, "property") ? XmlHelper.GetStringValue(node, "property") : string.Empty;
                Type = XmlHelper.HasAttribute(node, "type") ? XmlHelper.GetStringValue(node, "type") : string.Empty;
                IsForeignKey = XmlHelper.HasAttribute(node, "isForeignKey") ? XmlHelper.GetBooleanValue(node, "isForeignKey") : false;
                IsInitiallyLoaded = (XmlHelper.HasAttribute(node, "initiallyLoaded")) ?
                                        XmlHelper.GetBooleanValue(node, "initiallyLoaded") : true;
            }
        }
   } 
}
