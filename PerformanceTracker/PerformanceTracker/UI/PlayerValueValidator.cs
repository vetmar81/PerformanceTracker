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
    internal static class PlayerValueValidator
    {
        /// <summary>
        /// Determines whether the specified <paramref name="name"/> represents a valid input.
        /// </summary>
        /// <param name="name">The name to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="name"/> represents a valid input; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidString(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        /// <summary>
        /// Determines whether the specified <paramref name="height"/> represents a valid input.
        /// </summary>
        /// <param name="name">The height to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="height"/> represents a valid input; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidInteger(string height)
        {
            int value;
            return int.TryParse(height, NumberStyles.Integer, null, out value) && value > 0;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="weight"/> represents a valid input.
        /// </summary>
        /// <param name="name">The weight to be evaluated.</param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="weight"/> represents a valid input; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsValidDouble(string weight)
        {
            double value;
            return double.TryParse(weight, NumberStyles.Float, null, out value) && value > 0;
        }
    }
}
