﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Config
{
    internal class DbConfig
    {
        private Dictionary<string, DbTableMap> tableMaps;

        internal string DatabaseName { get; private set; }
        internal string ServerName { get; private set; }
        internal string User { get; private set; }
        internal string Password { get; private set; }
        internal int Port { get; private set; }

        internal IList<DbFeatureCategory> FeatureCategories { get; private set; }

        internal DbConfig(string configPath)
        {
            tableMaps = new Dictionary<string, DbTableMap>();
            FeatureCategories = new List<DbFeatureCategory>();

            Init(configPath);
        }

        internal DbTableMap GetMap(Type type)
        {
            DbTableMap map;
            tableMaps.TryGetValue(type.FullName, out map);
            return map;
        }

        private void Init(string configPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

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
    }
}
