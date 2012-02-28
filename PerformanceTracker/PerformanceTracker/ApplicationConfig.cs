using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerformanceTracker
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:37
    /// Class that encapsulates and provides relevant application configuration values.
    /// </summary>
    internal class ApplicationConfig
    {
        /// <summary>
        /// Gets the Singleton instance.
        /// Has to be initialized first by calling <see cref="ApplicationConfig.Load(string)"/>
        /// </summary>
        internal static ApplicationConfig Instance { get; private set; }

        #region Configuration Values

        /// <summary>
        /// Gets the file path to the database configuration file.
        /// </summary>
        internal string DbConfigPath { get; private set; }

        /// <summary>
        /// Gets the width of the main user interface.
        /// </summary>
        /// <value>
        /// The width of the GUI.
        /// </value>
        internal int GuiWidth { get; private set; }

        /// <summary>
        /// Gets the height of the main user interface.
        /// </summary>
        /// <value>
        /// The height of the GUI.
        /// </value>
        internal int GuiHeight { get; private set; }

        /// <summary>
        /// Gets the list of countries.
        /// </summary>
        internal List<string> Countries { get; private set; }

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationConfig"/> class from being created.
        /// Use <see cref="ApplicationConfig.Instance"/> instead for get access to the singleton.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        private ApplicationConfig(string filePath)
        {
            Countries = new List<string>();
            LoadXml(filePath);
        }

        /// <summary>
        /// Loads the application configuration from specified file in <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The file path of the configuration file.</param>
        internal static void Load(string filePath)
        {
            if (Instance == null)
            {
                Instance = new ApplicationConfig(filePath);
            }
        }

        /// <summary>
        /// Loads the XML values from specified file in <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The file path of the XML file.</param>
        private void LoadXml(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.DocumentElement;

            XmlNode dbConfigNode = root.SelectSingleNode("DatabaseConfig");
            DbConfigPath = Path.Combine(PerformanceTracker.StartupPath, dbConfigNode.InnerText);

            XmlNode guiResolutionNode = root.SelectSingleNode("GuiLayout/Resolution");
            GuiWidth = XmlHelper.GetIntValue(guiResolutionNode, "width");
            GuiHeight = XmlHelper.GetIntValue(guiResolutionNode, "height");

            XmlNodeList countryCodeNodes = root.SelectNodes("CountryCodes/CountryCode");

            if (countryCodeNodes != null)
            {
                foreach (XmlNode countryCodeNode in countryCodeNodes)
                {
                    CountryCodeItem item = new CountryCodeItem(countryCodeNode);
                    Countries.Add(item.ToString());
                }
            }
        }

        /// <summary>
        /// Markus Vetsch, 27.02.2012 16:59
        /// Helper class for country codes and names.
        /// </summary>
        private class CountryCodeItem
        {
            private readonly string code;
            private readonly string name;

            /// <summary>
            /// Initializes a new instance of the <see cref="CountryCodeItem"/> class.
            /// </summary>
            /// <param name="node">The node.</param>
            internal CountryCodeItem(XmlNode node)
            {
                this.code = node.SelectSingleNode("Code").InnerText;
                this.name = node.SelectSingleNode("Name").InnerText;
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return string.Format("{0} - {1}", code, name);
            }
        }
    }
}
