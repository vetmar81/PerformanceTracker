using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Config;
using System.Data.Common;

namespace Vema.PerfTracker.Database.Service
{
    public class TeamService : BaseService<Team>
    {
        private static TeamService instance;

        private TeamService(Db database)
            : base(database)
        { }

        public static TeamService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new TeamService(database);
            }

            return instance;
        }

        #region Object loading

        public List<Team> LoadAll(bool loadAllReferences)
        {
            List<Team> teams = base.LoadAll();
            if (loadAllReferences)
            {
                foreach (Team team in teams)
                {
                    AssignCurrentPlayerReferences(team);
                }
            }

            return teams;
        }

        public override List<Team> LoadAll()
        {
            return LoadAll(false);
        }

        public override Team LoadById(long id)
        {
            Team team = base.LoadById(id);
            for (int i = 0; i < team.PlayerReferences.Count; i++)
            {
                PlayerReference current = team.PlayerReferences[i];
                LoadReference(current.Id, out current);
            }

            return team;
        }

        private void LoadReference(long referenceId, out PlayerReference reference)
        {
            reference = database.LoadById<PlayerReference>(referenceId);
        }

        //public Team LoadById(long id, bool loadReferences)
        //{
        //    Team team = database.LoadById<Team>(id);

        //    if (loadReferences)
        //    {
        //        AssignCurrentPlayerReferences(team);
        //    }

        //    return team;
        //}

        public List<Player> LoadCurrentPlayers(long teamId)
        {
            //Team team = LoadById(teamId, true);
            return new List<Player>();
        }

        public List<Player> LoadCurrentPlayers(Team team)
        {
            AssignCurrentPlayerReferences(team);
            return team.PlayerReferences.Select(r => r.Player).ToList();
        }

        public Team LoadTeamByDescriptor(string descriptor)
        {
            DbTableMap map = database.Config.GetMap(typeof(Team));
            string descriptorColumn = map.GetColumnForProperty("Descriptor");

            QueryConstraint descriptorConstraint = new QueryConstraint(descriptorColumn, descriptor.ToUpper(), QueryOperator.Equal);

            return database.LoadCurrent<Team>(descriptorConstraint);
        }

        public List<PlayerReference> LoadCurrentPlayerReferences(Team team)
        {
            DbTableMap map = database.Config.GetMap(typeof(PlayerReference));
            string teamIdColumn = map.GetColumnForProperty("team");

            QueryConstraint constraint = new QueryConstraint(teamIdColumn, team.Id, QueryOperator.Equal);

            return database.LoadAllCurrent<PlayerReference>(constraint);
        }

        #endregion

        public override void Save(Team obj)
        {
            base.Save(obj);
        }

        public override void SaveAll(IEnumerable<Team> objList)
        {
            base.SaveAll(objList);
        }

        private void AssignCurrentPlayerReferences(Team team)
        {
            team.PlayerReferences.Clear();

            // Assign team to references

            List<PlayerReference> references = LoadCurrentPlayerReferences(team);
            references.ForEach(r => r.Team = team);

            team.PlayerReferences.AddRange(references);

            // Assign players to references

            AssignPlayersByReferences(team.Id, references);
        }       

        private void AssignPlayersByReferences(long teamId, IEnumerable<PlayerReference> references)
        {
            foreach (PlayerReference reference in references)
            {
                long playerId = LoadPlayerId(teamId, reference.Id);

                if (playerId != -1)
                {
                    Player player = database.LoadById<Player>(playerId);
                    reference.Player = player;
                    player.Reference = reference;
                }
            }
        }

        private long LoadPlayerId(long teamId, long referenceId)
        {
            DbTableMap map = database.Config.GetMap(typeof(PlayerReference));
            string idColumn = map.GetIdColumn();
            string teamIdColumn = map.GetColumnForProperty("team");
            string playerIdColumn = map.GetColumnForProperty("player");

            QueryBuilder builder = new QueryBuilder(QueryType.Select);
            QueryConstraint constraint = new QueryConstraint(idColumn, referenceId, QueryOperator.Equal);
            constraint.AppendConstraint(QueryOperator.And, teamIdColumn, teamId, QueryOperator.Equal);

            string sql = builder.CreateSelectSql(map.Table, constraint, playerIdColumn);

            long playerId = -1;

            try
            {
                database.OpenConnection();

                DbDataReader reader = database.ExecuteReader(sql);

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    playerId = reader.GetInt64(0);
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

            return playerId;
        }
    }
}
