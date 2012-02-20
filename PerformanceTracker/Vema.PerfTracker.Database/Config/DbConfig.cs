using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;
using System.Xml.Schema;
using System.IO;

namespace Vema.PerfTracker.Database.Config
{
    /// <summary>
    /// Markus 14.02.2012 00:08
    /// Configuration class providing details about object-relational mapping to database
    /// </summary>
    internal class DbConfig
    {
        private Dictionary<string, DbTableMap> tableMaps;

        #region Properties

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        internal string DatabaseName { get; private set; }

        /// <summary>
        /// Gets the resovled name or the IP-Adress of the server.
        /// </summary>
        /// <value>
        /// The resolved name or the IP-Adress of the server.
        /// </value>
        internal string ServerName { get; private set; }

        /// <summary>
        /// Gets the user for database login.
        /// </summary>
        internal string User { get; private set; }

        /// <summary>
        /// Gets the password for database login.
        /// </summary>
        internal string Password { get; private set; }

        /// <summary>
        /// Gets the port for database login.
        /// </summary>
        internal int Port { get; private set; }

        /// <summary>
        /// Gets the defined list of <see cref="DbFeatureCategory"/> items.
        /// </summary>
        internal IList<DbFeatureCategory> FeatureCategories { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConfig"/> class.
        /// </summary>
        /// <param name="filePath">The path to the configuration file..</param>
        internal DbConfig(string filePath)
        {
            tableMaps = new Dictionary<string, DbTableMap>();
            FeatureCategories = new List<DbFeatureCategory>();

            Init(filePath);
        }

        /// <summary>
        /// Gets the associated <see cref="DbTableMap"/> providing details 
        /// about single mapping of specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to retrieve mapping details for.</param>
        /// <returns>The <see cref="DbTableMap"/> providing mapping details.</returns>
        internal DbTableMap GetMap(Type type)
        {
            DbTableMap map;
            tableMaps.TryGetValue(type.FullName, out map);
            return map;
        }

        /// <summary>
        /// Initializes the configuration values.
        /// </summary>
        /// <param name="filePath">The path to the configuration file.</param>
        private void Init(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode databaseNode = doc.SelectSingleNode("Database");

            if (databaseNode != null && databaseNode.Attributes != null)
            {
                //TODO: Password encryption / decryption

                DatabaseName = XmlHelper.GetStringValue(databaseNode, "name");
                ServerName = XmlHelper.GetStringValue(databaseNode, "server");
                User = XmlHelper.GetStringValue(databaseNode, "user");
                Password = XmlHelper.GetStringValue(databaseNode, "password");
                Port = XmlHelper.GetIntValue(databaseNode, "port");

                XmlNodeList tableMapNodes = databaseNode.SelectNodes("TableMaps/TableMap");

                if (tableMapNodes != null)
                {
                    foreach (XmlNode tableMapNode in tableMapNodes)
                    {
                        DbTableMap map = new DbTableMap(tableMapNode);
                        tableMaps.Add(map.Class, map);
                    }
                }

                XmlNodeList featureCategoryNodes = databaseNode.SelectNodes("FeatureCategories/FeatureCategory");

                if (featureCategoryNodes != null)
                {
                    foreach (XmlNode featureCategoryNode in featureCategoryNodes)
                    {
                        FeatureCategories.Add(new DbFeatureCategory(featureCategoryNode));
                    }
                }
            }
        }

        /// <summary>
        /// Validates the XML file in specified <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The file path of the XML file.</param>
        private void ValidateXml(string filePath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);

            using (TextReader reader = File.OpenText(filePath))
            {
                using (XmlReader validator = XmlReader.Create(reader, settings))
                {
                    while (validator.Read())
                    { 
                    }

                    validator.Close();
                }

                reader.Close();
            }
        }

        /// <summary>
        /// Event handler for any validation event, that might occur while parsing the XML file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Xml.Schema.ValidationEventArgs"/> instance containing the event data.</param>
        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception;
                string message = e.Message;

                if (e.Severity == XmlSeverityType.Error)
                {
                    throw new XmlException("Invalid XML file!", ex);
                }
            }
        }
    }
}
