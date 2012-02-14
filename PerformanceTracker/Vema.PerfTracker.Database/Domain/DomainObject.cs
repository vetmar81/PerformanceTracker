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
        protected readonly IDao dao;

        /// <summary>
        /// Gets the database ID of the domain object.
        /// </summary>
        public long Id
        {
            get
            {
                if (dao == null)
                {
                    return -1;
                }

                return dao.Id;
            }
            internal set
            {
                Id = value;
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
        /// <param name="dao">The underlying <see cref="IDao"/>.</param>
        protected DomainObject(IDao dao)
        {
            this.dao = dao;
        }        
    }
}
