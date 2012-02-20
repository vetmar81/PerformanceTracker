using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:33
    /// Encapsulates the information about object-relation mapping to the database for a single member (property).
    /// </summary>
    internal class DbMemberMap
    {
        private readonly string namespaceQualifier;

        #region Properties

        /// <summary>
        /// Gets the corresponding database column.
        /// </summary>
        internal string Column { get; private set; }

        /// <summary>
        /// Gets the member name (property name).
        /// </summary>
        internal string Name { get; private set; }

        /// <summary>
        /// Gets the type (used for references to certain types).
        /// </summary>
        internal string Type { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this member shall be initially loaded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this member shall be initially loaded; otherwise, <c>false</c>.
        /// </value>
        internal bool IsInitiallyLoaded { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this member represents a foreign key.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this member represents a foreign key; otherwise, <c>false</c>.
        /// </value>
        internal bool IsForeignKey { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this member defines a referenced type.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this this member defines a referenced type; otherwise, <c>false</c>.
        /// </value>
        internal bool IsReferencedType
        {
            get { return !string.IsNullOrEmpty(Type); }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DbMemberMap"/> class.
        /// </summary>
        /// <param name="node">The affiliated <see cref="XmlNode"/>.</param>
        /// <param name="namespaceQualifier">The namespace qualifier containing the mapping class.</param>
        internal DbMemberMap(XmlNode node, string namespaceQualifier)
        {
            this.namespaceQualifier = namespaceQualifier;

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
                Column = XmlHelper.HasAttribute(node, "column") ? XmlHelper.GetStringValue(node, "column") : string.Empty;
                Name = XmlHelper.HasAttribute(node, "name") ? XmlHelper.GetStringValue(node, "name") : string.Empty;

                // Store fully qualified type inf (namespace.class) 

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
