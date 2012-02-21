using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch, 16.02.2012 23:36
    /// Abstract definiton for any kind of temporal data.
    /// </summary>
    public interface ITemporal
    {
        /// <summary>
        /// Gets the valid from date.
        /// </summary>
        DateTime ValidFrom { get; set; }

        /// <summary>
        /// Gets the valid to date.
        /// </summary>
        DateTime ValidTo { get; set; }
    }
}
