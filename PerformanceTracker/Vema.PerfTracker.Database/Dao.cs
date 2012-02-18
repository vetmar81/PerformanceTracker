using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using System.Data.Common;
using System.Reflection;

namespace Vema.PerfTracker.Database.Access
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 14:14
    /// Abstract base class for all DAO implementations.
    /// </summary>
    public abstract class Dao
    {
        /// <summary>
        /// Gets or sets the Id of this <see cref="Dao"/>.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        public long Id { get; internal set; }

        /// <summary>
        /// Creates the corresponding <see cref="DomainObject"/>.
        /// </summary>
        /// <returns>the corresponding <see cref="DomainObject"/>.</returns>
        public abstract DomainObject CreateDomainObject();

        /// <summary>
        /// Initializes a new instance of the <see cref="Dao"/> class.
        /// </summary>
        protected Dao()
        {
            Id = -1;
        }
    }
}
