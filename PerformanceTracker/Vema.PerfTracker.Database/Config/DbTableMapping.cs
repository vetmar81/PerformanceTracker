using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbTableMapping
    {
        private string namespaceQualifier;
        private string className;

        internal string Table { get; private set; }
        internal string Class
        {
            get { return string.Concat(namespaceQualifier, className); }
        }

        internal string Sequence { get; private set; }

        internal IList<DbTableMemberMapping> MemberMappings { get; private set; }

        internal DbTableMapping(XmlNode node)
        {
            MemberMappings = new List<DbTableMemberMapping>();

            Init(node);
        }

        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Table = node.Attributes.GetNamedItem("name").Value;
                namespaceQualifier = node.Attributes.GetNamedItem("namespace").Value;
                className = node.Attributes.GetNamedItem("class").Value;
                Sequence = (HasAttribute(node, "sequence")) ? node.Attributes.GetNamedItem("sequence").Value : string.Empty;

                XmlNodeList memberNodes = node.SelectNodes("Member");

                if (memberNodes != null)
                {
                    foreach (XmlNode memberNode in memberNodes)
                    {
                        MemberMappings.Add(new DbTableMemberMapping(memberNode));
                    }
                }
            }
        }

        private bool HasAttribute(XmlNode node, string name)
        {
            return node.Attributes != null && node.Attributes.GetNamedItem(name) != null;
        }
    }
}
