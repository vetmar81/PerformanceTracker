using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    public class DbTableMap
    {
        private string namespaceQualifier;
        private string className;

        internal string Table { get; private set; }
        internal string Schema
        {
            get {return Table.Substring(0, Table.IndexOf(".")); }
        }
        internal string Class
        {
            get { return string.Concat(namespaceQualifier, ".", className); }
        }

        internal bool HasSequence
        {
            get { return !string.IsNullOrEmpty(Sequence); }
        }
        internal string Sequence { get; private set; }

        internal List<DbMemberMap> Members { get; private set; }

        internal DbTableMap(XmlNode node)
        {
            Members = new List<DbMemberMap>();

            Init(node);
        }

        internal string GetColumnForProperty(string propertyName)
        {
            return Members.Single(m => m.Name == propertyName).Column;
        }

        internal string GetIdColumn()
        {
            return GetColumnForProperty("Id");
        }

        internal string[] GetNonReferenceTypeColumns()
        {
            return Members.Where(m => !m.IsReferencedType).Select(m => m.Column).ToArray();
        }

        internal string[] GetInitiallyLoadedColumns()
        {
            return Members.Where(m => m.IsInitiallyLoaded && !string.IsNullOrEmpty(m.Column)).Select(m => m.Column).ToArray();
        }

        internal IEnumerable<DbMemberMap> GetInitiallyLoadedNonReferencedTypeMembers()
        {
            return GetNonReferencedTypeMembers().Where(m => m.IsInitiallyLoaded);
        }

        internal IEnumerable<DbMemberMap> GetNonReferencedTypeMembers()
        {
            return Members.Where(m => !m.IsReferencedType);
        }

        internal IEnumerable<DbMemberMap> GetReferencedTypes()
        {
            return Members.Where(m => m.IsReferencedType);
        }

        internal DbMemberMap GetReferencedTypeMember(Type type)
        {
            return Members.Find(m => m.IsReferencedType && m.Type == type.Name);
        }

        internal string GetForeignKeyColumn(Type type)
        {
            DbMemberMap foreignKeyMember = Members.Find(m => m.Type == type.FullName && m.IsForeignKey);

            if (foreignKeyMember == null)
            {
                return null;
            }

            return foreignKeyMember.Column;
        }

        internal bool HasReferencedTypes()
        {
            return Members.Where(m => m.IsReferencedType).Count() > 0;
        }

        private void Init(XmlNode node)
        {
            if (node.Attributes != null)
            {
                Table = XmlHelper.GetStringValue(node, "name");
                namespaceQualifier = XmlHelper.GetStringValue(node, "namespace");
                className = XmlHelper.GetStringValue(node, "class");
                Sequence = (XmlHelper.HasAttribute(node, "sequence")) ?
                            XmlHelper.GetStringValue(node, "sequence") : string.Empty;

                XmlNodeList memberNodes = node.SelectNodes("Member");

                if (memberNodes != null)
                {
                    foreach (XmlNode memberNode in memberNodes)
                    {
                        Members.Add(new DbMemberMap(memberNode, namespaceQualifier));
                    }
                }
            }
        }
    }
}
