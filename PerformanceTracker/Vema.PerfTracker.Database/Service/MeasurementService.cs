using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Helper;
using System.Data.Common;

namespace Vema.PerfTracker.Database.Service
{
    /// <summary>
    /// Markus Vetsch, 20.02.2012 17:15
    /// Service class providing database manipulations methods for 
    /// <see cref="Measurement"/> instances such as inserting, updating and deleting
    /// </summary>
    public class MeasurementService : BaseService<Measurement>
    {
        private static MeasurementService instance;     // Singleton

        /// <summary>
        /// Prevents a default instance of the <see cref="MeasurementService"/> class from being created.
        /// Use <see cref="MeasurementService.GetInstance(Db)"/> to access the singleton instance instead.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        private MeasurementService(Db database)
            : base(database)
        { 
        }

        /// <summary>
        /// Gets the singleton <see cref="MeasurementService"/> instance.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        /// <returns>The singleton <see cref="MeasurementService"/>.</returns>
        public static MeasurementService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new MeasurementService(database);
            }

            return instance;
        }

        #region Service functions - Load

        /// <summary>
        /// Loads a <see cref="Measurement"/> by the specified database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Measurement"/>.</param>
        /// <param name="loadReferences">if set to <c>true</c> referencing objects are eager loaded;
        /// Otherwise, referencing objects have to be lazy-loaded instead using <see cref="MeasurementService.LoadReferences(Measurement)"/>.</param>
        /// <returns>The requested <see cref="Measurement"/> or <c>null</c>, if not match for specified ID fount.</returns>
        public Measurement LoadById(long id, bool loadReferences)
        {
            Measurement measurement = base.LoadById(id);
            if (loadReferences)
            {
                LoadReferences(measurement);
            }

            return measurement;
        }

        /// <summary>
        /// Loads a <see cref="Measurement"/> by the specified database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Measurement"/>.</param>
        /// <returns>The requested <see cref="Measurement"/>. Referenced objects are not loaded by default.
        /// Consider using <see cref="MeasurementService.LoadById(long, bool)"/> for eager loading instead.</returns>
        public override Measurement LoadById(long id)
        {
            return LoadById(id, false);
        }

        /// <summary>
        /// Loads all available <see cref="Measurement"/> objects from the database.
        /// </summary>
        /// <param name="loadReferences">if set to <c>true</c> referencing objects are eager loaded;
        /// Otherwise, referencing objects have to be lazy-loaded instead using <see cref="MeasurementService.LoadReferences(Measurement)"/>.</param>
        /// <returns>The requested list of <see cref="Measurement"/> instances.</returns>
        public List<Measurement> LoadAll(bool loadReferences)
        {
            List<Measurement> measurements = base.LoadAll();
            if (loadReferences)
            {
                measurements.ForEach(measurement => LoadReferences(measurement));
            }

            return measurements;
        }

        /// <summary>
        /// Loads all available <see cref="Measurement"/> objects from the database.
        /// </summary>
        /// <returns>The requested list of <see cref="Measurement"/> instances. Referenced objects are not loaded by default.
        /// Consider using <see cref="MeasurementService.LoadAll(bool)"/> for eager loading instead.</returns>
        public override List<Measurement> LoadAll()
        {
            return LoadAll(false);
        }

        /// <summary>
        /// Loads all <see cref="Measurement"/> objects linked to the specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to load all <see cref="Measurement"/> objects for.</param>
        /// <returns>The list of <see cref="Measurement"/> instances that belong to the specified <paramref name="player"/>.</returns>
        public List<Measurement> LoadAllForPlayer(Player player)
        {
            PlayerService.GetInstance(database).LoadPlayerReference(player);
            return LoadAllMeasurements(player.Reference.Id);
        }

        /// <summary>
        /// Loads all <see cref="Measurement"/> objects linked to the specified <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> to load all <see cref="Measurement"/> objects for.</param>
        /// <returns>The list of <see cref="Measurement"/> instances that belong to the specified <paramref name="team"/>.</returns>
        public List<Measurement> LoadAllForTeam(Team team)
        {
            TeamService.GetInstance(database).LoadReferences(team);
            List<Measurement> measurements = new List<Measurement>();

            foreach (PlayerReference reference in team.References)
            {
                measurements.AddRange(LoadAllMeasurements(reference.Id));
            }

            return measurements;
        }

        /// <summary>
        /// Loads all <see cref="Measurement"/> in relation to a certain <paramref name="date"/>.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/> value to be respected.</param>
        /// <param name="before">if set to <c>true</c> <see cref="Measurement"/> instances 
        /// registered before (exclusive) the specified <paramref name="date"/> are respected; otherwise only <see cref="Measurement"/> instances
        /// after (inclusive) the specified <paramref name="date"/> will be respected.</param>
        /// <returns>All <see cref="Measurement"/> instances matching the specified date condition.</returns>
        public List<Measurement> LoadAllByDate(DateTime date, bool before)
        {
            DbTableMap map = database.GetMap(typeof(Measurement));
            string timeStampColumn = map.GetColumnForMemberName("Timestamp");

            QueryOperator constraintOperator = (before) ? QueryOperator.Smaller : QueryOperator.BiggerEqual;
            QueryConstraint constraint = new QueryConstraint(timeStampColumn, date, constraintOperator);

            return database.LoadAll<Measurement>(constraint);
        }

        /// <summary>
        /// Loads all the <see cref="Measurement"/> instances that were registered within the specified time range.
        /// </summary>
        /// <param name="fromDate">The lower margin of the time range; the margin is included into the search.</param>
        /// <param name="toDate">The upper margin of the time range; the margin is included into the search.</param>
        /// <returns>All the <see cref="Measurement"/> instances within specified time range.</returns>
        public List<Measurement> LoadAllWithin(DateTime fromDate, DateTime toDate)
        {
            DbTableMap map = database.GetMap(typeof(Measurement));
            string timeStampColumn = map.GetColumnForMemberName("Timestamp");

            QueryConstraint constraint = new QueryConstraint(timeStampColumn, fromDate, QueryOperator.BiggerEqual);
            constraint.AppendConstraint(QueryOperator.And, timeStampColumn, toDate, QueryOperator.SmallerEqual);

            return database.LoadAll<Measurement>(constraint);
        }

        /// <summary>
        /// Loads all the <see cref="Measurement"/> instances for specified <paramref name="player"/> 
        /// that were registered within the specified time range.
        /// </summary>
        /// <param name="fromDate">The lower margin of the time range; the margin is included into the search.</param>
        /// <param name="toDate">The upper margin of the time range; the margin is included into the search.</param>
        /// <param name="player">The <see cref="Player"/> to load all <see cref="Measurement"/> objects for.</param>
        /// <returns>All the <see cref="Measurement"/> instances within specified time range.</returns>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="player"/> is <c>null</c>.</exception>
        public List<Measurement> LoadAllWithin(DateTime fromDate, DateTime toDate, Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            PlayerService.GetInstance(database).LoadPlayerReference(player);

            DbTableMap map = database.GetMap(typeof(Measurement));
            string playerRefIdColumn = map.GetForeignKeyColumn(typeof(PlayerReference));
            string timestampColumn = map.GetColumnForMemberName("Timestamp");

            QueryConstraint constraint = new QueryConstraint(playerRefIdColumn, player.Reference.Id, QueryOperator.Equal);
            constraint.AppendConstraint(QueryOperator.And, new QueryConstraint(timestampColumn, fromDate, QueryOperator.BiggerEqual));
            constraint.AppendConstraint(QueryOperator.And, new QueryConstraint(timestampColumn, toDate, QueryOperator.SmallerEqual));

            return database.LoadAll<Measurement>(constraint);
        }

        /// <summary>
        /// Loads all the <see cref="Measurement"/> instances for specified <paramref name="team"/> 
        /// that were registered within the specified time range.
        /// </summary>
        /// <param name="fromDate">The lower margin of the time range; the margin is included into the search.</param>
        /// <param name="toDate">The upper margin of the time range; the margin is included into the search.</param>
        /// <param name="team">The <see cref="Team"/> to load all <see cref="Measurement"/> objects for.</param>
        /// <returns>All the <see cref="Measurement"/> instances within specified time range.</returns>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="team"/> is <c>null</c>.</exception>
        public List<Measurement> LoadAllWithin(DateTime fromDate, DateTime toDate, Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException("team");
            }

            List<Measurement> teamMeasurements = new List<Measurement>();
            TeamService.GetInstance(database).LoadReferences(team);

            DbTableMap map = database.GetMap(typeof(Measurement));
            string playerRefIdColumn = map.GetForeignKeyColumn(typeof(PlayerReference));
            string timestampColumn = map.GetColumnForMemberName("Timestamp");

            foreach (PlayerReference reference in team.References)
            {
                QueryConstraint constraint = new QueryConstraint(playerRefIdColumn, reference.Id, QueryOperator.Equal);
                constraint.AppendConstraint(QueryOperator.And, new QueryConstraint(timestampColumn, fromDate, QueryOperator.BiggerEqual));
                constraint.AppendConstraint(QueryOperator.And, new QueryConstraint(timestampColumn, toDate, QueryOperator.SmallerEqual));

                teamMeasurements.AddRange(database.LoadAll<Measurement>(constraint));
            }

            return teamMeasurements;
        }

        public List<Measurement> LoadAllForPlayerByCategory(Player player, FeatureCategory category)
        {
            // TODO: implementation

            return new List<Measurement>();
        }

        public List<Measurement> LoadAllForTeamByCategory(Team team, FeatureCategory category)
        {
            // TODO: implementation

            return new List<Measurement>();
        }

        public List<Measurement> LoadAllForTeamBySubCategory(Team team, FeatureSubCategory subCategory)
        {
            // TODO: implementation

            return new List<Measurement>();
        }

        public List<Measurement> LoadAllForPlayerBySubCategory(Player player, FeatureSubCategory subCategory)
        {
            // TODO: implementation

            return new List<Measurement>();
        }

        /// <summary>
        /// Loads all the object references for specified <paramref name="measurement"/>.
        /// </summary>
        /// <param name="measurement">The <see cref="Measurement"/> to load object references for.</param>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="measurement"/> is null.</exception>
        public void LoadReferences(Measurement measurement)
        {
            if (measurement == null)
            {
                throw new ArgumentNullException("measurement");
            }

            PlayerReference reference = database.LoadById<PlayerReference>(measurement.Reference.Id);
            measurement.Reference = reference;

            LoadTeamInfo(measurement, reference);

            LoadPlayerInfo(measurement, reference);

            LoadCategoryInfo(measurement);
        }

        /// <summary>
        /// Loads the <see cref="Team"/> information for specified <see cref="Measurement"/> and <see cref="PlayerReference"/>.
        /// </summary>
        /// <param name="measurement">The affected <see cref="Measurement"/>.</param>
        /// <param name="reference">The affected <see cref="PlayerReference"/>.</param>
        public void LoadTeamInfo(Measurement measurement, PlayerReference reference)
        {
            Team team = TeamService.GetInstance(database).LoadById(reference.Team.Id);
            measurement.Reference.Team = team;
        }

        /// <summary>
        /// Loads the <see cref="Player"/> information for specified <see cref="Measurement"/> and <see cref="PlayerReference"/>.
        /// </summary>
        /// <param name="measurement">The affected <see cref="Measurement"/>.</param>
        /// <param name="reference">The affected <see cref="PlayerReference"/>.</param>
        public void LoadPlayerInfo(Measurement measurement, PlayerReference reference)
        {
            Player player = PlayerService.GetInstance(database).LoadById(reference.Player.Id);
            measurement.Reference.Player = player;
        }

        /// <summary>
        /// Loads the category information for specified <see cref="Measurement"/> and <see cref="PlayerReference"/>.
        /// </summary>
        /// <param name="measurement">The affected <see cref="Measurement"/>.</param>
        /// <param name="reference">The affected <see cref="PlayerReference"/>.</param>
        public void LoadCategoryInfo(Measurement measurement)
        {
            FeatureCategoryService featureService = FeatureCategoryService.GetInstance(database);
            FeatureSubCategory subCategory = featureService.LoadSubCategoryById(measurement.SubCategory.Id);
            measurement.SubCategory = subCategory;
        }

        /// <summary>
        /// Loads all <see cref="Measurement"/> for specified database ID of a <see cref="PlayerReference"/>.
        /// </summary>
        /// <param name="playerReferenceId">The database ID of a <see cref="PlayerReference"/>.</param>
        /// <returns>All <see cref="Measurement"/> that belong to specified database ID.</returns>
        private List<Measurement> LoadAllMeasurements(long playerReferenceId)
        {
            Type refType = typeof(PlayerReference);
            DbTableMap map = database.GetMap(typeof(Measurement));
            string playerRefIdColumn = map.GetForeignKeyColumn(refType);

            QueryConstraint constraint = new QueryConstraint(playerRefIdColumn, playerReferenceId, QueryOperator.Equal);

            List<Measurement> measurements = database.LoadAll<Measurement>(constraint);
            measurements.ForEach(m => LoadCategoryInfo(m));

            return measurements;
        }

        #endregion

        #region Service function - Save

        /// <summary>
        /// Saves the specified <paramref name="measurement"/> to the database.
        /// </summary>
        /// <param name="measurement">The <see cref="Measurement"/> to be saved to the database.</param>
        public override void Save(Measurement measurement)
        {
            base.Save(measurement);
        }

        /// <summary>
        /// Saves all the specified <see cref="Measurement"/> in <paramref name="measurementList"/> to the database.
        /// </summary>
        /// <param name="measurementList">The list of <see cref="Measurement"/> to be saved.</param>
        public override void SaveAll(IEnumerable<Measurement> measurementList)
        {
            base.SaveAll(measurementList);
        }

        #endregion
    }
}
