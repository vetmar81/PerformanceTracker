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

        public Measurement LoadById(long id, bool loadReferences)
        {
            Measurement measurement = base.LoadById(id);
            if (loadReferences)
            {
                LoadReferences(measurement);
            }

            return measurement;
        }

        public override Measurement LoadById(long id)
        {
            return LoadById(id, false);
        }

        public List<Measurement> LoadAll(bool loadReferences)
        {
            List<Measurement> measurements = base.LoadAll();
            if (loadReferences)
            {
                measurements.ForEach(measurement => LoadReferences(measurement));
            }

            return measurements;
        }

        public override List<Measurement> LoadAll()
        {
            return LoadAll(false);
        }

        public void LoadReferences(Measurement measurement)
        {
            if (measurement == null)
            {
                throw new ArgumentNullException("measurement");
            }

            PlayerReference reference = database.LoadById<PlayerReference>(measurement.Reference.Id);
            measurement.Reference = reference;

            Team team = TeamService.GetInstance(database).LoadById(reference.Team.Id);
            measurement.Reference.Team = team;

            Player player = PlayerService.GetInstance(database).LoadById(reference.Player.Id);
            measurement.Reference.Player = player;

            FeatureCategoryService featureService = FeatureCategoryService.GetInstance(database);
            FeatureSubCategory subCategory = featureService.LoadSubCategoryById(measurement.SubCategory.ParentCategory.Id,
                                                                                    measurement.SubCategory.Id);
            measurement.SubCategory = subCategory;
        }

        #endregion

        #region Service function - Save

        public override void Save(Measurement measurement)
        {
            base.Save(measurement);
        }

        public override void SaveAll(IEnumerable<Measurement> measurementList)
        {
            base.SaveAll(measurementList);
        }

        #endregion
    }
}
