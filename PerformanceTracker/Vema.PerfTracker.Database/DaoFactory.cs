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
    /// Abstract factory, that creates <see cref="Dao"/> instances based on the type of <see cref="DomainObject"/>.
    /// </summary>
    public static class DaoFactory
    {
        /// <summary>
        /// Creates the corresponding <see cref="Dao"/> instance for specified <paramref name="type"/>.
        /// The type is supposed to be any inheritor of <see cref="DomainObject"/>. Otherwise, an
        /// exception will be thrown.
        /// </summary>
        /// <param name="type">The type of the .</param>
        /// <returns>
        /// The corresponding <see cref="Dao"/> instance.
        /// </returns>
        /// <exception cref="PersistenceException">Thrown, if <paramref name="type"/> is not supported.</exception>
        internal static Dao CreateDao(Type type)
        {
            string typeQualifier = type.FullName;

            switch (typeQualifier)
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
                    throw new PersistenceException(typeQualifier, "Unsupported type for DAO creation!");
            }
        }

        /// <summary>
        /// Creates the corresponding <see cref="Dao"/> instance.
        /// </summary>
        /// <typeparam name="T">Any kind of <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="PersistenceException">Thrown, if type of <see cref="DomainObject"/> is unknown.</exception>
        /// <returns>The corresponding <see cref="Dao"/> instance.</returns>
        public static Dao CreateDao<T>() where T : DomainObject
        {
            return CreateDao(typeof(T));
        }
    }
}
