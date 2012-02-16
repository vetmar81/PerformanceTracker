using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 22:58
    /// Utility class for simple evaluation and retrieval of XML values.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Determines whether the specified <see cref="XmlNode"/> has the requested attribute.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/> to be evaluated.</param>
        /// <param name="attribute">The attribute to be verified.</param>
        /// <returns>
        ///   <c>true</c> if the specified node has the requested attribute; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(XmlNode node, string attribute)
        {
            return (node.Attributes != null && node.Attributes.GetNamedItem(attribute) != null);
        }

        /// <summary>
        /// Determines whether the specified <see cref="XmlNode"/> provides the requested direct child element.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/> to be evaluated.</param>
        /// <param name="element">The element to be verified.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="XmlNode"/> provides the requested child element; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasDirectChildElement(XmlNode node, string element)
        {
            return node.SelectSingleNode(element) != null;
        }

        /// <summary>
        /// Gets the value from the specified attribute as <see cref="string"/>.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/> providing the attribute.</param>
        /// <param name="attribute">The attribute providing the value.</param>
        /// <returns>The attribute value as <see cref="string"/> or <c>null</c>,
        /// if the attribute wasn't found.</returns>
        public static string GetStringValue(XmlNode node, string attribute)
        {
            if (HasAttribute(node, attribute))
            {
                return node.Attributes.GetNamedItem(attribute).Value;
            }

            return null;
        }

        /// <summary>
        /// Gets the value from the specified attribute as <see cref="bool"/>.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/> providing the attribute.</param>
        /// <param name="attribute">The attribute providing the value.</param>
        /// <returns>The attribute value as <see cref="bool"/>.</returns>
        /// <exception cref="ArgumentException">Thrown, if the original attribute <see cref="string"/> 
        /// value couldn't be converted into a <see cref="bool"/>.</exception>
        public static bool GetBooleanValue(XmlNode node, string attribute)
        {
            bool value;
            string attributeValue = GetStringValue(node, attribute);
            if (!bool.TryParse(attributeValue, out value))
            {
                throw new ArgumentException(string.Format("Value of XmlAttribute {0} in XmlNode {1} isn't a valid boolean value!",
                                                            attribute, node.Name),
                                            attributeValue);
            }

            return value;
        }

        /// <summary>
        /// Gets the value from the specified attribute as <see cref="double"/>.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/> providing the attribute.</param>
        /// <param name="attribute">The attribute providing the value.</param>
        /// <returns>The attribute value as <see cref="double"/>.</returns>
        /// <exception cref="ArgumentException">Thrown, if the original attribute <see cref="string"/> 
        /// value couldn't be converted into a <see cref="double"/>.</exception>
        public static double GetDoubleValue(XmlNode node, string attribute)
        {
            double value;
            string attributeValue = GetStringValue(node, attribute);
            if (!double.TryParse(attributeValue, out value))
            {
                throw new ArgumentException(string.Format("Value of XmlAttribute {0} in XmlNode {1} isn't a valid double value!",
                                                            attribute, node.Name), 
                                            attributeValue);
            }

            return value;
        }

        /// <summary>
        /// Gets the value from the specified attribute as <see cref="int"/>.
        /// </summary>
        /// <param name="node">The <see cref="XmlNode"/> providing the attribute.</param>
        /// <param name="attribute">The attribute providing the value.</param>
        /// <returns>The attribute value as <see cref="int"/>.</returns>
        /// <exception cref="ArgumentException">Thrown, if the original attribute <see cref="string"/> 
        /// value couldn't be converted into a <see cref="int"/>.</exception>
        public static int GetIntValue(XmlNode node, string attribute)
        {
            int value;
            string attributeValue = GetStringValue(node, attribute);
            if (!int.TryParse(attributeValue, out value))
            {
                throw new ArgumentException(string.Format("Value of XmlAttribute {0} in XmlNode {1} isn't a valid integer value!",
                                                            attribute, node.Name),
                                            attributeValue);
            }

            return value;
        }
    }
}
