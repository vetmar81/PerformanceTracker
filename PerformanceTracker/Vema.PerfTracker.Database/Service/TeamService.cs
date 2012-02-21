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
    /// <summary>
    /// Markus Vetsch, 20.02.2012 14:52
    /// Service class providing database manipulations methods for 
    /// <see cref="Team"/> instances such as inserting, updating and deleting
    /// </summary>
    public class TeamService : BaseService<Team>
    {
        private static TeamService instance;        // Singleton

        /// <summary>
        /// Prevents a default instance of the <see cref="TeamService"/> class from being created.
        /// Use <see cref="TeamService.GetInstance(Db)"/> to access the singleton instance instead.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        private TeamService(Db database)
            : base(database)
        { }

        /// <summary>
        /// Gets the singleton <see cref="TeamService"/> instance.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        /// <returns>The singleton <see cref="TeamService"/>.</returns>
        public static TeamService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new TeamService(database);
            }

            return instance;
        }

        #region Service functions - Load

        /// <summary>
        /// Loads the currently valid <see cref="Team"/> object with specified database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Team"/>.</param>
        /// <returns>The <see cref="Team"/> object from the database or <c>null</c>, if no matching items found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using <see cref="TeamService.LoadCurrent(long, bool)"/>.</returns>
        public Team LoadCurrent(long id)
        {
            return LoadCurrent(id, false);
        }

        /// <summary>
        /// Loads the currently valid <see cref="Team"/> object with specified database ID from the database.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Team"/>.</param>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object references have to be lazy-loaded in a separate step.</param>
        /// <returns>The <see cref="Team"/> object from the database or <c>null</c>, if no matching items found.</returns>
        public Team LoadCurrent(long id, bool loadReferences)
        {
            Team team = base.LoadCurrent<Team>(id);
            if (loadReferences)
            {
                LoadReferences(team);
            }

            return team;
        }

        /// <summary>
        /// Loads the currently valid <see cref="Team"/> object with specified <paramref name="descriptor"/> from the database.
        /// </summary>
        /// <param name="descriptor">The descriptor of the <see cref="Team"/>.</param>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object references have to be lazy-loaded in a separate step.</param>
        /// <returns>The <see cref="Team"/> object from the database or <c>null</c>, if no matching items found.</returns>
        public Team LoadCurrent(string descriptor, bool loadReferences)
        {
            DbTableMap map = database.GetMap(typeof(Team));
            string descriptorColumn = map.GetColumnForMemberName("Descriptor");

            QueryConstraint constraint = new QueryConstraint(descriptorColumn, descriptor, QueryOperator.Equal);

            Team team = base.LoadCurrent<Team>(constraint);
            if (loadReferences)
            {
                LoadReferences(team);
            }

            return team;
        }

        /// <summary>
        /// Loads the currently valid <see cref="Team"/> object with specified <paramref name="descriptor"/> from the database.
        /// </summary>
        /// <param name="descriptor">The descriptor of the <see cref="Team"/>.</param>
        /// <returns>The <see cref="Team"/> object from the database or <c>null</c>, if no matching items found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using <see cref="TeamService.LoadCurrent(string, bool)"/>.</returns>
        public Team LoadCurrent(string descriptor)
        {
            return LoadCurrent(descriptor, false);
        }

        /// <summary>
        /// Loads all currently valid <see cref="Team"/> objects from the database.
        /// </summary>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object references have to be lazy-loaded in a separate step.</param>
        /// <returns>The list of currently valid <see cref="Team"/> objects.</returns>
        public List<Team> LoadAllCurrent(bool loadReferences)
        {
            List<Team> teams = base.LoadAllCurrent<Team>();
            if (loadReferences)
            {
                teams.ForEach(team => LoadReferences(team));
            }

            return teams;
        }

        /// <summary>
        /// Loads all currently valid <see cref="Team"/> objects from the database.
        /// </summary>
        /// <returns>All the <see cref="Team"/> objects from the database or an
        /// empty <see cref="List&lt;Team&gt;"/>, if no items found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using <see cref="TeamService.LoadAllCurrent(bool)"/>.</returns>
        public List<Team> LoadAllCurrent()
        {
            return LoadAllCurrent(false);
        }

        /// <summary>
        /// Loads all <see cref="Team"/> objects from database. - independent whether they're currently valid or not.
        /// </summary>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object references have to be lazy-loaded in a separate step.</param>
        /// <returns>
        /// All the <see cref="Team"/> objects from the database.
        /// </returns>
        public List<Team> LoadAll(bool loadReferences)
        {
            List<Team> teams = base.LoadAll();
            if (loadReferences)
            {
                teams.ForEach(team => LoadReferences(team));
            }

            return teams;
        }

        /// <summary>
        /// Loads all <see cref="Team"/> objects from database - independent whether they're currently valid or not.
        /// </summary>
        /// <returns>All the <see cref="Team"/> objects from the database or an
        /// empty <see cref="List&lt;Team&gt;"/>, if no items found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using <see cref="TeamService.LoadAll(bool)"/>.</returns>
        public override List<Team> LoadAll()
        {
            return LoadAll(false);
        }

        /// <summary>
        /// Loads the <see cref="Team"/> object from database with specified database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Player"/> to be loaded.</param>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object references have to be lazy-loaded in a separate step.</param>
        /// <returns>
        /// The <see cref="Team"/> object from the database or <c>null</c>, if no matching item found.
        /// </returns>
        public Team LoadById(long id, bool loadReferences)
        {
            Team team = base.LoadById(id);

            if (loadReferences)
            {
                LoadReferences(team);
            }

            return team;
        }

        /// <summary>
        /// Loads the <see cref="Player"/> object specified by the database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Player"/> to be loaded.</param>
        /// <returns>The loaded <see cref="Player"/> object or <c>null</c>, if no matching item found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using <see cref="TeamService.LoadById(long, bool)"/> instead.</returns>
        public override Team LoadById(long id)
        {
            return LoadById(id, false);
        }

        /// <summary>
        /// Loads all the <see cref="PlayerReference"/> items assigned to the <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> instance to load references for.</param>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="team"/> is <c>null</c>.</exception>
        public void LoadReferences(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException("team");
            }

            for (int i = 0; i < team.References.Count; i++)
            {
                PlayerReference current = team.References[i];
                PlayerReference loaded = database.LoadById<PlayerReference>(current.Id);
                team.References[i] = loaded;
            }    
        }

        /// <summary>
        /// Loads the list of <see cref="Player"/> objects currently assigned to the specified <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/>, which to load players for.</param>
        /// <returns>The list of <see cref="Player"/> objects currently assigned to the specified <paramref name="team"/>.</returns>
        public List<Player> LoadCurrentPlayers(Team team)
        {
            List<Player> players = new List<Player>();
            LoadReferences(team);

            PlayerService service = PlayerService.GetInstance(database);

            foreach (PlayerReference reference in team.References)
            {
                Player player = service.LoadById(reference.Player.Id);
                if (player != null)
                {
                    players.Add(player);
                }
            }

            return players;
        }

        #endregion

        #region Service functions - Save / Update

        /// <summary>
        /// Saves the specified <paramref name="team"/> to the database.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> to be saved.</param>
        public override void Save(Team team)
        {
            //TODO: Determine, when to update references

            UpdateReferences(team);
            base.Update(team);

            base.Save(team);
            SaveReferences(team);
        }

        private void SaveReferences(Team team)
        {
            for (int i = 0; i < team.References.Count; i++)
            {
                database.SaveObject<PlayerReference>(team.References[i]);
            }
        }

        private void UpdateReferences(Team team)
        {
            for (int i = 0; i < team.References.Count; i++)
            {
                database.UpdateObject<PlayerReference>(team.References[i]);
            }
        }

        #endregion

        #region Service functions - Delete


        #endregion
    }
}
