using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Vema.PerformanceTracker.UI
{
    /// <summary>
    /// Markus Vetsch, 28.02.2012 22:45
    /// Helper class for validation of input values for a player.
    /// </summary>
    internal static class InputValueValidator
    {
        /// <summary>
        /// Determines whether the specified <paramref name="value"/> represents a valid input.
        /// </summary>
        /// <param name="value">The name to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> represents a valid input; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidString(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> represents a valid positive <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> represents 
        ///   a valid positive <see cref="int"/>; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidPositiveInteger(string value)
        {
            int intValue;
            return IsValidInteger(value, out intValue) && intValue > 0;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> represents a valid <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value to be evaluated.</param>
        /// <param name="intValue">The converted <see cref="int"/> value.</param>
        /// <returns>
        ///   <c>true</c> the specified <paramref name="value"/>
        ///   represents a valid <see cref="int"/>; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidInteger(string value, out int intValue)
        {
            return int.TryParse(value, NumberStyles.Integer, null, out intValue);
        }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> represents a valid input.
        /// </summary>
        /// <param name="name">The weight to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="value"/> represents a valid input; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidPositiveDouble(string value)
        {
            double outValue;
            return IsValidDouble(value, out outValue) && outValue > 0;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="value"/> represents a valid <see cref="double"/>.
        /// </summary>
        /// <param name="value">The value to be evaluated.</param>
        /// <param name="doubleValue">The converted <see cref="double"/> value.</param>
        /// <returns>
        ///   <c>true</c> the specified <paramref name="value"/>
        ///   represents a valid <see cref="double"/>; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidDouble(string value, out double doubleValue)
        {
            return double.TryParse(value, NumberStyles.Float, null, out doubleValue);
        }
    }
}
