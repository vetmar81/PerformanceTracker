using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Service;

namespace Vema.PerfTracker
{
    internal class ClientDatabase
    {
        private PgDb database;

        internal ClientDatabase(string configPath)
        {
            database = PgDb.Create(configPath);
        }

        internal List<Player> SelectAllPlayers()
        {
            return PlayerService.GetInstance(database).LoadAll(true);
        }

        internal Player SelectPlayerById(long id)
        {
            return PlayerService.GetInstance(database).LoadById(id);
        }

        internal List<Player> SelectPlayerByLastNamePart(string lastNamePart)
        {
            return PlayerService.GetInstance(database).LoadByLastName(lastNamePart);
        }

        internal List<Player> SelectPlayerByFirstNamePart(string firstNamePart)
        {
            return PlayerService.GetInstance(database).LoadByFirstName(firstNamePart);
        }

        internal Team SelectTeamById(long id)
        {
            return TeamService.GetInstance(database).LoadById(id, true);
        }

        internal List<Player> SelectAllPlayerForTeam(long id)
        {
            return TeamService.GetInstance(database).LoadCurrentPlayers(id);
        }

        internal Team SelectCurrentTeamByDescriptor(string descriptor)
        {
            QueryConstraint constraint = new QueryConstraint("descriptor", descriptor, QueryOperator.Equal);
            constraint.AppendConstraint(QueryOperator.And, "deleted", false, QueryOperator.Equal);

            List<Team> teams = database.LoadAll<Team>(constraint);

            if (teams.Count > 1)
            { 
            }

            return teams[0];
        }

        internal List<Team> SelectAllTeams()
        {
            return TeamService.GetInstance(database).LoadAll();
        }

        internal List<Team> SelectAllCurrentTeams()
        {
            QueryConstraint constraint = new QueryConstraint("deleted", false, QueryOperator.Equal);
            return database.LoadAll<Team>(constraint);
        }

        internal List<Player> SelectByBirthdateOlder(DateTime date)
        {
            return PlayerService.GetInstance(database).LoadByBirthday(date, true);
        }

        internal List<Player> SelectByBirthdateYounger(DateTime date)
        {
            return PlayerService.GetInstance(database).LoadByBirthday(date, false);
        }
    }
}
