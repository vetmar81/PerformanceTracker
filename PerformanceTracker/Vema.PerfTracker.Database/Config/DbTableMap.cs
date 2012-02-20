using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    /// <summary>
    /// Markus Vetsch, 20.02.2012 00:41
    /// Class respresenting the definition of an object-relational mapping from a database table to a object.
    /// </summary>
    internal class DbTableMap
    {
        private string namespaceQualifier;
        private string className;

        private static string idMember = "Id";

        #region Properties

        /// <summary>
        /// Gets the database table name.
        /// </summary>
        internal string Table { get; private set; }

        /// <summary>
        /// Gets the database schema or <see cref="string.Empty"/>, if no schema defined.
        /// The schema information is extracted from the table qualifier ([schema name].[table name]).
        /// </summary>
        internal string Schema
        {
            get
            {
                if (Table.IndexOf(".") > 0)
                {
                    return Table.Substring(0, Table.IndexOf("."));
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the fully qualified ([namespace].[type name]) class name.
        /// </summary>
        internal string Class
        {
            get { return string.Concat(namespaceQualifier, ".", className); }
        }

        /// <summary>
        /// Gets a value indicating whether this table definition provides a sequence (auto-increment).
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this table definition provides a sequence; otherwise, <c>false</c>.
        /// </value>
        internal bool HasSequence
        {
            get { return !string.IsNullOrEmpty(Sequence); }
        }

        /// <summary>
        /// Gets the sequence name.
        /// </summary>
        internal string Sequence { get; private set; }

        /// <summary>
        /// Gets the list of <see cref="DbMemberMap"/> instances, providing mapping details for members.
        /// </summary>
        internal List<DbMemberMap> Members { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DbTableMap"/> class.
        /// </summary>
        /// <param name="node">The affiliated <see cref="XmlNode"/>.</param>
        internal DbTableMap(XmlNode node)
        {
            Members = new List<DbMemberMap>();

            Init(node);
        }

        #region Utility Methods - retrieve mapping infos

        /// <summary>
        /// Gets the <see cref="DbMemberMap"/> instance describing the database ID.
        /// </summary>
        /// <returns>the <see cref="DbMemberMap"/> instance describing the database ID.</returns>
        internal DbMemberMap GetIdMember()
        {
            // LINQ expression to find member name "id"

            return Members.Find(m => m.Name == idMember);
        }

        /// <summary>
        /// Gets the name of the database column mapping to specified member <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The member name.</param>
        /// <returns>The name of the database column mapping to specified member.</returns>
        internal string GetColumnForMemberName(string name)
        {
            // LINQ expression to find member mapping to specified name 

            return Members.Find(m => m.Name == name).Column;
        }

        /// <summary>
        /// Gets the name of the database column mapping to the id member.
        /// </summary>
        /// <returns>The name of the database column mapping to the id member.</returns>
        internal string GetIdColumnName()
        {
            return GetColumnForMemberName(idMember);
        }

        /// <summary>
        /// Gets the names of all database columns describing references to other persistent object types.
        /// </summary>
        /// <returns>The name of all database columns describing references to other persistent object types.</returns>
        internal string[] GetNonReferenceTypeColumns()
        {
            // LINQ expression to find members defining references to other types,
            // select their column names and return them in an array

            return Members.Where(m => !m.IsReferencedType).Select(m => m.Column).ToArray();
        }

        /// <summary>
        /// Gets the names of all database columns describing a member, that shall initially be loaded on loading
        /// the persistent object.
        /// </summary>
        /// <returns>The names of all database columns, that shall initially be loaded.</returns>
        internal string[] GetInitiallyLoadedColumns()
        {
            // LINQ expression to find members that shall be initially loaded,
            // select their column names and return them in an array

            return Members.Where(m => m.IsInitiallyLoaded && !string.IsNullOrEmpty(m.Column)).Select(m => m.Column).ToArray();
        }

        /// <summary>
        /// Gets the <see cref="DbMemberMap"/> instances describing a member, that shall initially be loaded on loading
        /// the persistent object and that doesn't defined a reference to another persistent object type.
        /// </summary>
        /// <returns>The set of <see cref="DbMemberMap"/> instances to be initially loaded 
        /// and not describing references to other persistent object types.</returns>
        internal IEnumerable<DbMemberMap> GetInitiallyLoadedNonReferencedTypeMembers()
        {
            // LINQ expression to get only the initially loaded members not describing type references

            return GetNonReferencedTypeMembers().Where(m => m.IsInitiallyLoaded);
        }

        /// <summary>
        /// Gets all <see cref="DbMemberMap"/> instances not describing references to other persistent object types.
        /// </summary>
        /// <returns>All <see cref="DbMemberMap"/> instances not describing references to other persistent object types.</returns>
        internal IEnumerable<DbMemberMap> GetNonReferencedTypeMembers()
        {
            // LINQ expression to get only members not describing type references

            return Members.Where(m => !m.IsReferencedType);
        }

        /// <summary>
        /// Gets all <see cref="DbMemberMap"/> instances describing references to other persistent object types.
        /// </summary>
        /// <returns>All <see cref="DbMemberMap"/> instances describing references to other persistent object types.</returns>
        internal IEnumerable<DbMemberMap> GetReferencedTypes()
        {
            // LINQ expression to get only members describing type references

            return Members.Where(m => m.IsReferencedType);
        }

        /// <summary>
        /// Gets the <see cref="DbMemberMap"/> instance describing the other persistent object <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of the referenced persistent object.</param>
        /// <returns>The <see cref="DbMemberMap"/> instance describing the other
        /// persistent object <paramref name="type"/>.</returns>
        internal DbMemberMap GetReferencedTypeMember(Type type)
        {
            return Members.Find(m => m.IsReferencedType && m.Type == type.Name);
        }

        /// <summary>
        /// Gets the database column name for specified <paramref name="type"/> describing a foreign key column.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to find the foreign key column for.</param>
        /// <returns>The name of the database column describing a foreign key column 
        /// or <c>null</c>, if no such member definition found.</returns>
        internal string GetForeignKeyColumn(Type type)
        {
            // LINQ expression for finding the correct type and respecting foreign key columns only

            DbMemberMap foreignKeyMember = Members.Find(m => m.Type == type.FullName && m.IsForeignKey);

            if (foreignKeyMember == null)
            {
                return null;
            }

            return foreignKeyMember.Column;
        }

        /// <summary>
        /// Determines whether this <see cref="DbTableMap"/> describes any references to other persistent object types.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this <see cref="DbTableMap"/> describes any references
        ///   to other persistent object types.; otherwise, <c>false</c>.
        /// </returns>
        internal bool HasReferencedTypes()
        {
            // LINQ expression to find any members describing references to other persistent object types.

            return Members.Any(m => m.IsReferencedType);
        }

        #endregion

        /// <summary>
        /// Initalizes the configuration values of given <paramref name="node"/>.
        /// </summary>
        /// <param name="node">The affiliated <see cref="XmlNode"/>.</param>
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
