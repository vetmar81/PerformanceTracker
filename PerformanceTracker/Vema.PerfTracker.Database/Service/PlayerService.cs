using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Access;
using Vema.PerfTracker.Database.Config;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;

namespace Vema.PerfTracker.Database.Service
{
    public class PlayerService : BaseService<Player>
    {
        private static PlayerService instance;

        private PlayerService(Db database)
            : base(database)
        { }

        public static PlayerService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new PlayerService(database);
            }

            return instance;
        }

        #region Service Functions Loading

        public override Player LoadById(long id)
        {
            Player player = base.LoadById(id);
            LoadCurrentPlayerHistory(player);
            LoadCurrentPlayerReference(player);

            return player;
        }

        private void LoadCurrentPlayerHistory(Player player)
        {
            PlayerDataHistory dataHistory = database.LoadById<PlayerDataHistory>(player.DataHistory.Id);
            player.DataHistory = dataHistory;
        }

        private void LoadCurrentPlayerReference(Player player)
        {
            PlayerReference reference = database.LoadById<PlayerReference>(player.Reference.Id);
            player.Reference = reference;
        }

        //public override Player LoadById(long id)
        //{
        //    return LoadById(id, false);
        //}

        //public Player LoadById(long id, bool loadReferences)
        //{
        //    Player player = database.LoadById<Player>(id);

        //    if (loadReferences)
        //    {
        //        AssignReferences(player);
        //    }

        //    return player;
        //}

        public override List<Player> LoadAll()
        {
            return LoadAll(false);
        }

        public List<Player> LoadAll(bool loadReferences)
        {
            List<Player> players = database.LoadAll<Player>();

            if (loadReferences)
            {
                foreach (Player player in players)
                {
                    AssignReferences(player);
                }
            }

            return players;
        }

        public List<Player> LoadByBirthday(DateTime birthDate, bool olderThan)
        {
            return LoadByBirthdayFilter(birthDate, olderThan);
        }

        public List<Player> LoadByFirstName(string firstNameFilter)
        {
            return LoadByFirstNameFilter(firstNameFilter);
        }

        public List<Player> LoadByLastName(string lastNameFilter)
        {
            return LoadByLastNameFilter(lastNameFilter);
        }

        public List<PlayerReference> LoadAllReferences(Player player)
        {
            return LoadAllReferences(player.Id);
        }

        public List<PlayerDataHistory> LoadCompleteHistory(Player player)
        {
            return LoadCompleteDataHistory(player.Id);
        }

        #endregion

        #region Service Functions Saving / Updating

        public override void Save(Player obj)
        {
            database.SaveObject<Player>(obj);
            database.SaveObject<PlayerReference>(obj.Reference);
            database.SaveObject<PlayerDataHistory>(obj.DataHistory);
        }

        public override void SaveAll(IEnumerable<Player> objList)
        {
            foreach (Player player in objList)
            {
                Save(player);
            }
        }

        public override void Update(Player obj)
        {
            database.UpdateObject<Player>(obj);
            database.SaveObject<PlayerReference>(obj.Reference);
            database.SaveObject<PlayerDataHistory>(obj.DataHistory);
        }

        #endregion

        #region Private Helper / Loading

        private void AssignReferences(Player player)
        {
            PlayerReference reference = database.LoadCurrent<PlayerReference>(player);
            reference.Player = player;
            reference.Team = LoadTeamForPlayerReference(reference);

            player.Reference = reference;
            player.DataHistory = database.LoadCurrent<PlayerDataHistory>(player);
        }

        private Team LoadTeamForPlayerReference(PlayerReference reference)
        {
            long referenceId = reference.Id;
            long playerId = reference.Player.Id;

            DbTableMap map = database.Config.GetMap(typeof(PlayerReference));
            string teamIdColumn = map.GetColumnForMemberName("team");

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            string sql = builder.CreateSelectSql(map.Table, new QueryConstraint(map.GetIdColumnName(), reference.Id, QueryOperator.Equal), teamIdColumn);

            long teamId = LoadTeamId(reference);

            return (teamId == -1) ? null : database.LoadById<Team>(teamId);
        }

        private long LoadTeamId(PlayerReference reference)
        {
            DbTableMap map = database.Config.GetMap(typeof(PlayerReference));
            string teamIdColumn = map.GetColumnForMemberName("team");

            QueryConstraint constraint = new QueryConstraint(map.GetIdColumnName(), reference.Id, QueryOperator.Equal);
            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            string sql = builder.CreateSelectSql(map.Table, constraint, teamIdColumn);

            long teamId = -1;

            try
            {
                database.OpenConnection();

                DbDataReader reader = database.ExecuteReader(sql);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    teamId = reader.GetInt64(0);
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

            return teamId;
        }

        private List<PlayerReference> LoadAllReferences(long playerId)
        {
            // Get constraint column names

            DbTableMap map = database.Config.GetMap(typeof(PlayerReference));
            string foreignKeyColumn = map.GetForeignKeyColumn(typeof(Player));

            // Set constraint

            QueryConstraint constraint = new QueryConstraint(foreignKeyColumn, playerId, QueryOperator.Equal);

            return database.LoadAll<PlayerReference>(constraint);
        }

        private List<PlayerDataHistory> LoadCompleteDataHistory(long playerId)
        {
            DbTableMap map = database.Config.GetMap(typeof(PlayerDataHistory));
            string foreignKeyColumn = map.GetForeignKeyColumn(typeof(Player));

            QueryConstraint constraint = new QueryConstraint(foreignKeyColumn, playerId, QueryOperator.Equal);

            return database.LoadAll<PlayerDataHistory>(constraint);
        }

        private List<Player> LoadByFirstNameFilter(string filter)
        {
            DbTableMap map = database.Config.GetMap(typeof(Player));
            string firstNameColumn = map.GetColumnForMemberName("FirstName");

            QueryConstraint constraint = new QueryConstraint(firstNameColumn, filter, QueryOperator.Like);

            return database.LoadAll<Player>(constraint);
        }

        private List<Player> LoadByLastNameFilter(string filter)
        {
            DbTableMap map = database.Config.GetMap(typeof(Player));
            string lastNameColumn = map.GetColumnForMemberName("LastName");

            QueryConstraint constraint = new QueryConstraint(lastNameColumn, filter, QueryOperator.Like);

            return database.LoadAll<Player>(constraint);
        }

        private List<Player> LoadByBirthdayFilter(DateTime date, bool older)
        {
            DbTableMap map = database.Config.GetMap(typeof(Player));
            string birthdayColumn = map.GetColumnForMemberName("Birthday");

            QueryOperator op = older ? QueryOperator.Smaller : QueryOperator.BiggerEqual;
            QueryConstraint constraint = new QueryConstraint(birthdayColumn, date, op);

            return database.LoadAll<Player>(constraint);
        }

        #endregion

    }
}
