using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database.Domain
{
    /// <summary>
    /// Markus Vetsch 14.02.2012 09:38
    /// Abstract definition for any kind of persistent database object.
    /// </summary>
    public abstract class DomainObject
    {
        private long id;
        protected Dao dao;

        /// <summary>
        /// Gets the database ID of the domain object.
        /// </summary>
        public long Id
        {
            get
            {
                return id;
            }
            internal set
            {
                id = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObject"/> class.
        /// To be used for write operations.
        /// </summary>
        protected DomainObject()
        {
            Id = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObject"/> class.
        /// To be used for read operations.
        /// </summary>
        /// <param name="dao">The underlying <see cref="Dao"/>.</param>
        protected DomainObject(Dao dao)
        {
            this.dao = dao;
            id = dao.Id;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("[{0} - Id: {1}]", GetType(), Id);
        }
    }
}
