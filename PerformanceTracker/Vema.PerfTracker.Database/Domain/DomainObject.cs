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
        protected readonly Dao dao;

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
        protected DomainObject(Dao dao)
        {
            this.dao = dao;
        }

        /// <summary>
        /// Creates the <see cref="DomainObject"/> suitable to the specified <see cref="Dao"/>.
        /// </summary>
        /// <param name="dao">The <see cref="Dao"/> instance.</param>
        /// <returns>The suitable <see cref="DomainObject"/> instance.</returns>
        internal static DomainObject CreateObject(Dao dao)
        {
            if (dao is PlayerDao)
            {
                return new Player((PlayerDao) dao);
            }
            else if (dao is PlayerDataHistoryDao)
            {
                return new PlayerDataHistory((PlayerDataHistoryDao) dao);
            }
            else if (dao is PlayerReferenceDao)
            {
                return new PlayerReference((PlayerReferenceDao) dao);
            }
            else if (dao is TeamDao)
            {
                return new Team((TeamDao) dao);
            }
            else if (dao is FeatureCategoryDao)
            {
                return new FeatureCategory((FeatureCategoryDao) dao);
            }
            else if (dao is FeatureSubCategoryDao)
            {
                return new FeatureSubCategory((FeatureSubCategoryDao) dao);
            }
            else if (dao is MeasurementDao)
            {
                return new Measurement((MeasurementDao) dao);
            }
            else
            {
                throw new PersistenceException(string.Format("Can't create domain object from DAO type {0}!",   
                                                                dao.GetType().FullName));
            }
        }
    }
}
