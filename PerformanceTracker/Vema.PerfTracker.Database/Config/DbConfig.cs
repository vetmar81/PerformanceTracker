using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbConfig
    {
        private Dictionary<string, DbTableMapping> tableMappings;

        internal string DatabaseName { get; private set; }
        internal string ServerName { get; private set; }
        internal string User { get; private set; }
        internal string Password { get; private set; }
        internal int Port { get; private set; }

        internal IList<DbFeatureCategory> FeatureCategories { get; private set; }

        internal DbConfig(string configPath)
        {
            FeatureCategories = new List<DbFeatureCategory>();

            Init(configPath);
        }

        private void Init(string configPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNode databaseNode = doc.SelectSingleNode("Database");

            if (databaseNode != null && databaseNode.Attributes != null)
            {
                //TODO: Password encryption / decryption

                DatabaseName = databaseNode.Attributes.GetNamedItem("name").Value;
                ServerName = databaseNode.Attributes.GetNamedItem("server").Value;
                User = databaseNode.Attributes.GetNamedItem("user").Value;
                Password = databaseNode.Attributes.GetNamedItem("password").Value;
                Port = int.Parse(databaseNode.Attributes.GetNamedItem("port").Value);

                XmlNodeList tableMappingNodes = databaseNode.SelectNodes("TableMappings/TableMapping");

                if (tableMappingNodes != null)
                {
                    foreach (XmlNode tableMappingNode in tableMappingNodes)
                    {
                        DbTableMapping mapping = new DbTableMapping(tableMappingNode);
                        tableMappings.Add(mapping.Class, mapping);
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
    }
}
