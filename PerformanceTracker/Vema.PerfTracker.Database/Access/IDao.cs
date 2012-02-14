using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Access
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 00:44
    /// Basic interface for any data access object.
    /// </summary>
    public interface IDao
    {
        /// <summary>
        /// Gets or sets the Id of this <see cref="IDao"/>.
        /// </summary>.
        /// <value>
        /// The id.
        /// </value>
        long Id { get; set; }

        /// <summary>
        /// Saves this <see cref="IDao"/>.
        /// </summary>
        void Save();

        /// <summary>
        /// Loads this <see cref="IDao"/>.
        /// </summary>
        void Load();

        /// <summary>
        /// Updates this <see cref="IDao"/>.
        /// </summary>
        void Update();

        /// <summary>
        /// Deletes this <see cref="IDao"/>.
        /// </summary>
        void Delete();
    }
}
