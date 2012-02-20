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
    public class MeasurementService : BaseService<Measurement>
    {
        private static MeasurementService instance;

        private MeasurementService(Db database)
            : base(database)
        { 
        }

        public static MeasurementService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new MeasurementService(database);
            }

            return instance;
        }

        #region Object Loading

        public Measurement LoadById(long id, bool loadAllReferences)
        {
            Measurement measurement = base.LoadById(id);
            if (loadAllReferences)
            {
                AssignReferences(measurement);
            }

            return measurement;
        }

        public override Measurement LoadById(long id)
        {
            return LoadById(id, false);
        }

        public List<Measurement> LoadAllMeasurementsForPlayer(Player player)
        {
            return LoadMeasurementsForPlayer(player);
        }

        public List<Measurement> LoadAllMeasurementsForTeam(Team team)
        {
            return LoadMeasurementsForTeam(team);
        }

        #endregion

        public override void Save(Measurement obj)
        {
            base.Save(obj);
        }

        public override void SaveAll(IEnumerable<Measurement> objList)
        {
            base.SaveAll(objList);
        }

        private void AssignReferences(Measurement measurement)
        {

        }

        private List<Measurement> LoadByReferences(IEnumerable<PlayerReference> references)
        {
            List<Measurement> resultList = new List<Measurement>();

            foreach (PlayerReference reference in references)
            {
                //reference.Player = player;

                DbTableMap map = database.Config.GetMap(typeof(Measurement));
                string playerReferenceIdColumn = map.GetColumnForMemberName("playerReference");

                QueryConstraint constraint = new QueryConstraint(playerReferenceIdColumn, reference.Id, QueryOperator.Equal);
                QueryBuilder builder = new QueryBuilder(QueryType.Select);
                string sql = builder.CreateSelectSql(map.Table, constraint, playerReferenceIdColumn);

                List<Measurement> currentList = database.LoadAll<Measurement>(constraint);
                currentList.ForEach(m => m.Reference = reference);

                resultList.AddRange(currentList);
            }

            return resultList;
        }

        private List<Measurement> LoadMeasurementsForPlayer(Player player)
        {            
            List<PlayerReference> references = PlayerService.GetInstance(database).LoadAllReferences(player);
            List<Measurement> resultList = LoadByReferences(references);
            resultList.ForEach(m => m.Reference.Player = player);

            return resultList;
        }

        private List<Measurement> LoadMeasurementsForTeam(Team team)
        {
            List<PlayerReference> references = TeamService.GetInstance(database).LoadCurrentPlayerReferences(team);
            List<Measurement> resultList = LoadByReferences(references);
            resultList.ForEach(m => m.Reference.Team = team);

            return resultList;
        }

        private long LoadSubCategoryId(Measurement measurement)
        {
            DbTableMap map = database.Config.GetMap(typeof(Measurement));
            string idColumn = map.GetIdColumnName();
            string playerReferenceIdColumn = map.GetColumnForMemberName("playerReference");

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            string sql = builder.CreateSelectSql(map.Table, null, playerReferenceIdColumn);

            long playerReferenceId = -1;

            try
            {
                database.OpenConnection();

                DbDataReader reader = database.ExecuteReader(sql);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    playerReferenceId = reader.GetInt64(0);
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                database.CloseConnection();
            }

            return playerReferenceId;
        }
    }
}
