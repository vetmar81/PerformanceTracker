using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;

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
            return database.LoadAll<Player>();
        }

        internal Player SelectPlayerById(long id)
        {
            return database.LoadById<Player>(id);
        }

        internal List<Player> SelectPlayerByLastNamePart(string lastNamePart)
        {
            QueryConstraint constraint = new QueryConstraint("lastname", lastNamePart, QueryOperator.Like);
            return database.LoadAll<Player>(constraint);
        }

        internal Team SelectTeamById(long id)
        {
            return database.LoadById<Team>(id);
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
            return database.LoadAll<Team>();
        }

        internal List<Team> SelectAllCurrentTeams()
        {
            QueryConstraint constraint = new QueryConstraint("deleted", false, QueryOperator.Equal);
            return database.LoadAll<Team>(constraint);
        }

        internal List<Player> SelectByBirthdateOlder(DateTime date)
        {
            QueryConstraint constraint = new QueryConstraint("birthdate", date, QueryOperator.SmallerEqual);
            return database.LoadAll<Player>(constraint);
        }
    }
}
