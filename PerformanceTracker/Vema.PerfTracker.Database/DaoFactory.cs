using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerfTracker.Database
{
    /// <summary>
    /// Markus Vetsch 14.02.2012 11:16
    /// Abstract factory, that creates <see cref="IDao"/> instances based on the type of <see cref="DomainObject"/>.
    /// </summary>
    internal static class DaoFactory
    {
        /// <summary>
        /// Creates the corresponding <see cref="Dao"/> instance.
        /// </summary>
        /// <typeparam name="T">Any kind of <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="DaoException">Thrown, if type of <see cref="DomainObject"/> is unknown.</exception>
        /// <returns>The corresponding <see cref="Dao"/> instance.</returns>
        internal static Dao CreateDao<T>() where T : DomainObject
        {
            string type = typeof(T).FullName;

            switch (type)
            {
                case "Vema.PerfTracker.Database.Domain.Player":
                    return new PlayerDao();
                case "Vema.PerfTracker.Database.Domain.Team":
                    return new TeamDao();
                case "Vema.PerfTracker.Database.Domain.Measurement":
                    return new MeasurementDao();
                case "Vema.PerfTracker.Database.Domain.PlayerReference":
                    return new PlayerReferenceDao();
                case "Vema.PerfTracker.Database.Domain.PlayerDataHistory":
                    return new PlayerDataHistoryDao();
                case "Vema.PerfTracker.Database.Domain.FeatureCategory":
                    return new FeatureCategoryDao();
                case "Vema.PerfTracker.Database.Domain.FeatureSubCategory":
                    return new FeatureSubCategoryDao();
                default:
                    throw new PersistenceException(type, "Unknown type for DAO creation!");
            }
        }
    }
}
