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

        #region Team Operations

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

        /// <summary>
        /// Saves a new <see cref="Team"/> with specified descriptor and age group to the database.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="ageGroup">The age group.</param>
        internal void SaveTeam(string descriptor, string ageGroup)
        {
            Team team = CreateTeam(descriptor, ageGroup);
            teamService.Save(team);
        }

        /// <summary>
        /// Loads the current team for specified <see cref="descriptor"/>.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <returns>The <see cref="Team"/> currently valid and associated with the <paramref name="descriptor"/>.</returns>
        internal Team LoadCurrentTeam(string descriptor)
        {
            return teamService.LoadCurrent(descriptor);
        }

        /// <summary>
        /// Loads and returns all <see cref="Player"/> being part of the specified <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> to load all <see cref="Player"/> for.</param>
        /// <returns>The list of <see cref="Player"/> that belong to the specified <see cref="Team"/>.</returns>
        internal List<Player> LoadAllPlayersOfTeam(Team team)
        {
            return teamService.LoadCurrentPlayers(team);
        }

        /// <summary>
        /// Updates the team definition for specified <paramref name="descriptor"/> and <paramref name="ageGroup"/>.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="ageGroup">The age group.</param>
        internal void UpdateTeam(string descriptor, string ageGroup)
        {
            Team team = LoadCurrentTeam(descriptor);
            team = UpdateTeam(team, descriptor, ageGroup);

            teamService.Update(team);
        }

        /// <summary>
        /// Determines, if a currently valid team definition for 
        /// specified <paramref name="descriptor"/> exists on database.
        /// </summary>
        /// <param name="descriptor">The descriptor to evaluated.</param>
        /// <returns><c>true</c>, if a currently valid team definition
        /// for the <paramref name="descriptor"/> exists; otherwise <c>false</c>.</returns>
        internal bool ExistsCurrentTeam(string descriptor)
        {
            return teamService.LoadCurrent(descriptor) != null;
        }

        /// <summary>
        /// Updates the <paramref name="team"/> with specified values for 
        /// <paramref name="descriptor"/> and <paramref name="ageGroup"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> to be updated.</param>
        /// <param name="descriptor">The updated descriptor.</param>
        /// <param name="ageGroup">The updated age group.</param>
        /// <returns>The updated <see cref="Team"/>.</returns>
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

        /// <summary>
        /// Creates the team domain object for given <paramref name="descriptor"/> and <see cref="ageGroup"/>.
        /// </summary>
        /// <param name="descriptor">The descriptor.</param>
        /// <param name="ageGroup">The age group.</param>
        /// <returns>The <see cref="Team"/> as domain object.</returns>
        private Team CreateTeam(string descriptor, string ageGroup)
        {
            TeamDao dao = (TeamDao) DaoFactory.CreateDao<Team>();
            dao.Descriptor = descriptor;
            dao.AgeGroup = ageGroup;

            return (Team) dao.CreateDomainObject();
        }

        #endregion

        #region Player Operations

        internal void LoadPlayerReferences(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                LoadPlayerReference(player);
            }
        }

        internal void LoadPlayerReference(Player player)
        {
            playerService.LoadPlayerReference(player);
        }

        internal void SavePlayer(Player player)
        {
            playerService.Save(player);
        }

        internal void InvalidatePlayerHistory(Player player)
        {
            playerService.UpdateHistory(player);
        }

        internal void SavePlayerList(IEnumerable<Player> players)
        {
            playerService.SaveAll(players);
        }

        internal void InvalidatePlayerReferenceList(IEnumerable<PlayerReference> playerReferences)
        {
            database.BulkUpdateObject<PlayerReference>(playerReferences);
        }

        internal void LoadCurrentHistory(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                LoadCurrentHistory(player);
            }
        }

        internal void LoadCurrentHistory(Player player)
        {
            playerService.LoadPlayerHistory(player);
        }

        internal PlayerDataHistory GetCurrentHistory(Player player)
        {
            return playerService.GetCurrentHistory(player);
        }

        internal List<PlayerDataHistory> LoadCompleteHistory(Player player)
        {
            return playerService.LoadCompleteHistory(player);
        }

        internal List<Measurement> LoadMeasurements(Player player)
        {
            return measurementService.LoadAllForPlayer(player);
        }

        internal Player LoadPlayer(long playerId)
        {
            return playerService.LoadById(playerId);
        }

        internal Player LoadPlayerCompletely(long playerId)
        {
            return playerService.LoadById(playerId, true);
        }

        internal List<FeatureCategory> LoadAllCategories()
        {
            return featureCategoryService.LoadAll();
        }

        #endregion

        #region Measurement Operations

        internal void SaveMeasurement(Measurement measurement)
        {
            measurementService.Save(measurement);
        }

        internal void SaveAllMeasurements(IEnumerable<Measurement> measurements)
        {
            measurementService.SaveAll(measurements);
        }

        internal void UpdateMeasurement(Measurement measurement)
        {
            measurementService.Update(measurement);
        }

        internal void UpdateAllMeasurement(IEnumerable<Measurement> measurements)
        {
            measurementService.UpdateAll(measurements);
        }

        internal void DeleteMeasurement(Measurement measurement)
        {
            measurementService.Delete(measurement);
        }

        internal void DeleteAllMeasurement(IEnumerable<Measurement> measurements)
        {
            measurementService.DeleteAll(measurements);
        }

        #endregion

    }
}
