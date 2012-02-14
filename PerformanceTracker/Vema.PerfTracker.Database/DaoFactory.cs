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
        /// Creates the corresponding <see cref="IDao"/> instance.
        /// </summary>
        /// <typeparam name="T">Any kind of <see cref="DomainObject"/>.</typeparam>
        /// <exception cref="DaoException">Thrown, if type of <see cref="DomainObject"/> is unknown.</exception>
        /// <returns>The corresponding <see cref="IDao"/> instance.</returns>
        internal static IDao CreateDao<T>() where T : DomainObject
        {
            string type = typeof(T).Name;

            switch (type)
            {
                case "Player":
                    return new PlayerDao();
                case "Team":
                    return new TeamDao();
                case "Measurement":
                    return new MeasurementDao();
                default:
                    throw new DaoException(type, "Unknown type for DAO creation!");
            }
        }
    }
}
