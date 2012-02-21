using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 10:48
    /// Compares two database objects of type <typeparamref name="T"/> for content equality.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be compared; 
    /// must inherit <see cref="DomainObject"/> and implement <see cref="ITemporal"/>.</typeparam>
    internal abstract class TemporalComparison<T> where T : DomainObject, ITemporal
    {
        /// <summary>
        /// Determines whether the specified objects of type <typeparamref name="T"/> are equal
        /// regarding their content (property values).
        /// </summary>
        /// <param name="previous">The previous object.</param>
        /// <param name="current">The current object.</param>
        /// <returns>
        ///   <c>true</c> if the specified objects of type <typeparamref name="T"/> are equal
        ///   by means of their property value; otherwise, <c>false</c>.
        /// </returns>
        internal abstract bool IsEqual(T previous, T current);
    }
}
