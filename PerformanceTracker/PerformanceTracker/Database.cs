using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database;
using Vema.PerfTracker.Database.Service;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Access;

namespace Vema.PerformanceTracker
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:09
    /// Client implementation for database access. Provides all the methods to
    /// retrieve and save requested data to the underlying database.
    /// </summary>
    internal class Database
    {
        /// <summary>
        /// Gets the Singleton instance. 
        /// Call <see cref="Database.Initialize(string)"/> for initialization. 
        /// </summary>
        internal static Database Instance { get; private set; }

        #region Private Fields

        private readonly Db database;       // database implementation      

        // database access services

        private readonly PlayerService playerService;
        private readonly TeamService teamService;
        private readonly MeasurementService measurementService;
        private readonly FeatureCategoryService featureCategoryService;

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="Database"/> class from being created.
        /// Use <see cref="Database.Instance"/> to access the Singleton.
        /// </summary>
        /// <param name="filePath">The file path providing the database configuration file.</param>
        private Database(string filePath)
        {
            database = PgDb.Create(filePath);

            // Init services

            playerService = PlayerService.GetInstance(database);
            teamService = TeamService.GetInstance(database);
            measurementService = MeasurementService.GetInstance(database);
            featureCategoryService = FeatureCategoryService.GetInstance(database);
        }

        /// <summary>
        /// Initializes the database access by reading configuration file specified
        /// by the <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The path of the configuration file.</param>
        internal static void Initialize(string filePath)
        {
            if (Instance == null)
            {
                Instance = new Database(filePath);
            }
        }

        /// <summary>
        /// Loads all team descriptors from <see cref="Team"/> instances, that are
        /// currently valid on the database.
        /// </summary>
        /// <returns>The list of team descriptors.</returns>
        internal IList<string> LoadAllTeamDescriptors()
        {
            List<Team> teams = teamService.LoadAllCurrent();
            return teams.Select(team => team.Descriptor).ToList();
        }

        internal void SaveTeam(string descriptor, string ageGroup)
        {
            Team team = CreateTeam(descriptor, ageGroup);
            teamService.Save(team);
        }

        internal Team LoadCurrentTeam(string descriptor)
        {
            return teamService.LoadCurrent(descriptor);
        }

        internal void InvalidateTeam(string descriptor, string ageGroup)
        {
            Team team = LoadCurrentTeam(descriptor);
            team = UpdateTeam(team, descriptor, ageGroup);

            teamService.Update(team);
        }

        internal bool ExistsTeam(string descriptor)
        {
            return teamService.LoadCurrent(descriptor) != null;
        }

        private Team UpdateTeam(Team team, string descriptor, string ageGroup)
        {
            if (team.Dao != null)
            {
                TeamDao dao = team.Dao;
                dao.Descriptor = descriptor;
                dao.AgeGroup = ageGroup;

                team = (Team) dao.CreateDomainObject();
            }

            return team;
        }

        private Team CreateTeam(string descriptor, string ageGroup)
        {
            TeamDao dao = (TeamDao) DaoFactory.CreateDao<Team>();
            dao.Descriptor = descriptor;
            dao.AgeGroup = ageGroup;

            return (Team) dao.CreateDomainObject();
        }
    }
}
