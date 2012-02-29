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
        private static SortedList<string, CountryCodeItem> countryItems;

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
        internal List<CountryCodeItem> Countries { get { return countryItems.Values.ToList(); } }

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationConfig"/> class from being created.
        /// Use <see cref="ApplicationConfig.Instance"/> instead for get access to the singleton.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        private ApplicationConfig(string filePath)
        {
            countryItems = new SortedList<string, CountryCodeItem>();
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
        /// Gets the associated <see cref="CountryCodeItem"/> by code.
        /// </summary>
        /// <param name="code">The code to look up for.</param>
        /// <returns>The <see cref="CountryCodeItem"/> or <c>null</c>, if no match for given code found.</returns>
        internal CountryCodeItem GetByCode(string code)
        {
            CountryCodeItem item;
            countryItems.TryGetValue(code, out item);
            return item;
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
                    countryItems.Add(item.Code, item);
                }
            }
        }

        /// <summary>
        /// Markus Vetsch, 27.02.2012 16:59
        /// Helper class for country codes and names.
        /// </summary>
        internal class CountryCodeItem
        {
            private readonly string code;
            private readonly string name;

            /// <summary>
            /// Gets the country code.
            /// </summary>
            internal string Code { get { return code; } }

            /// <summary>
            /// Initializes a new instance of the <see cref="CountryCodeItem"/> class.
            /// </summary>
            /// <param name="node">The node.</param>
            internal CountryCodeItem(XmlNode node)
            {
                this.code = node.SelectSingleNode("Code").InnerText;
                this.name = node.SelectSingleNode("Name").InnerText.ToLower();
                name = string.Concat(name.Substring(0, 1).ToUpper(), name.Substring(1));
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return string.Format("{0} - {1}", name, code);
            }
        }
    }
}
