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
    /// <summary>
    /// Markus Vetsch, 20.02.2012 14:26
    /// Service class providing database manipulations methods for 
    /// <see cref="Player"/> instances such as inserting, updating and deleting
    /// </summary>
    public class PlayerService : BaseService<Player>
    {
        private static PlayerService instance;      // Singleton

        /// <summary>
        /// Prevents a default instance of the <see cref="PlayerService"/> class from being created.
        /// Use <see cref="PlayerService.GetInstance(Db)"/> to access the singleton instance instead.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        private PlayerService(Db database)
            : base(database)
        { }

        /// <summary>
        /// Gets the singleton <see cref="PlayerService"/> instance.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        /// <returns>The singleton <see cref="PlayerService"/>.</returns>
        public static PlayerService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new PlayerService(database);
            }

            return instance;
        }

        #region Service Functions - Load

        /// <summary>
        /// Loads the <see cref="Player"/> object from database with specified database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Player"/> to be loaded.</param>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object refernces have to be lazy-loaded in a separate step.</param>
        /// <returns>
        /// The <see cref="Player"/> object from the database or <c>null</c>, if no matching item found.
        /// </returns>
        public Player LoadById(long id, bool loadReferences)
        {
            Player player = base.LoadById(id);

            if (loadReferences)
            {
                LoadPlayerHistory(player);
                LoadPlayerReference(player);
            }

            return player;
        }

        /// <summary>
        /// Loads the <see cref="Player"/> object specified by the database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="Player"/> to be loaded.</param>
        /// <returns>The loaded <see cref="Player"/> object or <c>null</c>, if no matching item found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using <see cref="PlayerService.LoadById(long, bool)"/> instead.</returns>
        public override Player LoadById(long id)
        {
            return LoadById(id, false);
        }

        /// <summary>
        /// Loads all <see cref="Player"/> objects from database.
        /// </summary>
        /// <returns>All the <see cref="Player"/> objects from the database or an
        /// empty <see cref="List&lt;Player&gt;"/>, if no items found.
        /// Referencing objects are not loaded - only IDs are available. For eager loading of object references
        /// consider using PlayerService.LoadAll(bool).</returns>
        public override List<Player> LoadAll()
        {
            return LoadAll(false);
        }

        /// <summary>
        /// Loads all <see cref="Player"/> objects from database.
        /// </summary>
        /// <param name="loadReferences">if set to <c>true</c> object references are loaded by default; otherwise
        /// object refernces have to be lazy-loaded in a separate step.</param>
        /// <returns>
        /// All the <see cref="Player"/> objects from the database.
        /// </returns>
        public List<Player> LoadAll(bool loadReferences)
        {
            List<Player> players = base.LoadAll();
            if (loadReferences)
            {
                foreach (Player player in players)
                {
                    LoadPlayerHistory(player);
                    LoadPlayerReference(player);
                }
            }

            return players;
        }

        /// <summary>
        /// Loads all <see cref="Player"/> objects matching the specified birthday filter.
        /// </summary>
        /// <param name="birthday">The birthday filter.</param>
        /// <param name="olderThan">if set to <c>true</c>, all <see cref="Player"/> instances
        /// with a birthday older than but exclusive <paramref name="birthday"/> will be loaded.
        /// Otherwise all <see cref="Player"/> younger than inclusive <paramref name="birthday"/> will be loaded.</param>
        /// <returns>The list of <see cref="Player"/> instance matching the specified birthday filter.</returns>
        public List<Player> LoadByBirthday(DateTime birthday, bool olderThan)
        {
            return LoadByBirthdayFilter(birthday, olderThan);
        }

        /// <summary>
        /// Loads all the <see cref="Player"/> objects matching the specified filter rule applying to first name.
        /// </summary>
        /// <param name="filter">The filter rule - wildcards will be respected.</param>
        /// <example>LoadByFirstName("%oso%) searches for the specified character sequence inside the first name.
        /// LoadByFirstName("S*")  searches for all first names starting with 'S' and followed by an arbitrary sequence of characters"</example>
        /// <returns>The list of <see cref="Player"/> instances matching the filter rule.</returns>
        public List<Player> LoadByFirstName(string filter)
        {
            return LoadByFirstNameFilter(filter);
        }

        /// <summary>
        /// Loads all the <see cref="Player"/> objects matching the specified filter rule applying to last name.
        /// </summary>
        /// <param name="filter">The filter rule - wildcards will be respected.</param>
        /// <example>LoadByFirstName("%oso%) searches for the specified character sequence inside the last name.
        /// LoadByFirstName("S*")  searches for all last names starting with 'S' and followed by an arbitrary sequence of characters"</example>
        /// <returns>The list of <see cref="Player"/> instances matching the filter rule.</returns>
        public List<Player> LoadByLastName(string filter)
        {
            return LoadByLastNameFilter(filter);
        }

        /// <summary>
        /// Loads all <see cref="PlayerReference"/> items that belong to specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to load all references for.</param>
        /// <returns>The list of <see cref="PlayerReference"/> items belonging to the <paramref name="player"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="player"/> is <c>null</c>.</exception>
        public List<PlayerReference> LoadAllReferences(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return LoadAllReferences(player);
        }

        /// <summary>
        /// Loads all <see cref="PlayerDataHistory"/> items that belong to specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to load all references for.</param>
        /// <returns>The list of <see cref="PlayerDataHistory"/> items belonging to the <paramref name="player"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="player"/> is <c>null</c>.</exception>
        public List<PlayerDataHistory> LoadCompleteHistory(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return LoadCompleteDataHistory(player);
        }

        /// <summary>
        /// Loads the history data for specified <see cref="Player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to load the history for.</param>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="player"/> is <c>null</c>.</exception>
        public void LoadPlayerHistory(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            if (player.DataHistory == null)
            {
                player.DataHistory = null;
                return;
            }

            PlayerDataHistory dataHistory = database.LoadById<PlayerDataHistory>(player.DataHistory.Id);
            player.DataHistory = dataHistory;
        }

        /// <summary>
        /// Gets the current history for the specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to get the history for..</param>
        /// <returns>The currently valid <see cref="PlayerDataHistory"/>.</returns>
        public PlayerDataHistory GetCurrentHistory(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            if (player.DataHistory == null)
            {
                return null;
            }

            return database.LoadById<PlayerDataHistory>(player.DataHistory.Id);
        }

        /// <summary>
        /// Loads the assigned reference for specified <see cref="Player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to load the reference for.</param>
        /// <exception cref="ArgumentNullException">Thrown, if <paramref name="player"/> is <c>null</c>.</exception>
        public void LoadPlayerReference(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            PlayerReference reference = database.LoadById<PlayerReference>(player.Reference.Id);
            player.Reference = reference;
        }

            #region Private Helpers - Load Player

            /// <summary>
            /// Loads all <see cref="PlayerReference"/> items that belong to specified <paramref name="player"/>.
            /// </summary>
            /// <param name="player">The <see cref="Player"/> to load all references for.</param>
            /// <returns>The list of <see cref="PlayerReference"/> items belonging to the <paramref name="player"/>.</returns>
            private List<PlayerReference> LoadCompleteReferences(Player player)
            {
                // Get constraint column names

                DbTableMap map = database.Config.GetMap(typeof(PlayerReference));
                string foreignKeyColumn = map.GetForeignKeyColumn(typeof(Player));

                // Set constraint - search for player ID

                QueryConstraint constraint = new QueryConstraint(foreignKeyColumn, player.Id, QueryOperator.Equal);

                return database.LoadAll<PlayerReference>(constraint);
            }

            /// <summary>
            /// Loads all <see cref="PlayerDataHistory"/> items that belong to specified <paramref name="player"/>.
            /// </summary>
            /// <param name="player">The <see cref="Player"/> to load all references for.</param>
            /// <returns>The list of <see cref="PlayerDataHistory"/> items belonging to the <paramref name="player"/>.</returns>
            private List<PlayerDataHistory> LoadCompleteDataHistory(Player player)
            {
                // Get constraint column names

                DbTableMap map = database.Config.GetMap(typeof(PlayerDataHistory));
                string foreignKeyColumn = map.GetForeignKeyColumn(typeof(Player));

                // Set constraint - search for player ID

                QueryConstraint constraint = new QueryConstraint(foreignKeyColumn, player.Id, QueryOperator.Equal);

                return database.LoadAll<PlayerDataHistory>(constraint);
            }

            /// <summary>
            /// Loads all the <see cref="Player"/> objects matching the specified filter rule applying to first name.
            /// </summary>
            /// <param name="filter">The filter rule - wildcards will be respected.</param>
            /// <example>LoadByFirstName("%oso%) searches for the specified character sequence inside the first name.
            /// LoadByFirstName("S*")  searches for all first names starting with 'S' and followed by an arbitrary sequence of characters"</example>
            /// <returns>The list of <see cref="Player"/> instances matching the filter rule.</returns>
            private List<Player> LoadByFirstNameFilter(string filter)
            {
                DbTableMap map = database.Config.GetMap(typeof(Player));
                string firstNameColumn = map.GetColumnForMemberName("FirstName");

                QueryConstraint constraint = new QueryConstraint(firstNameColumn, filter, QueryOperator.Like);

                return database.LoadAll<Player>(constraint);
            }

            /// <summary>
            /// Loads all the <see cref="Player"/> objects matching the specified filter rule applying to last name.
            /// </summary>
            /// <param name="filter">The filter rule - wildcards will be respected.</param>
            /// <example>LoadByFirstName("%oso%) searches for the specified character sequence inside the last name.
            /// LoadByFirstName("S*")  searches for all last names starting with 'S' and followed by an arbitrary sequence of characters"</example>
            /// <returns>The list of <see cref="Player"/> instances matching the filter rule.</returns>
            private List<Player> LoadByLastNameFilter(string filter)
            {
                DbTableMap map = database.Config.GetMap(typeof(Player));
                string lastNameColumn = map.GetColumnForMemberName("LastName");

                QueryConstraint constraint = new QueryConstraint(lastNameColumn, filter, QueryOperator.Like);

                return database.LoadAll<Player>(constraint);
            }

            /// <summary>
            /// Loads all <see cref="Player"/> objects matching the specified birthday filter.
            /// </summary>
            /// <param name="birthday">The birthday filter.</param>
            /// <param name="olderThan">if set to <c>true</c>, all <see cref="Player"/> instances
            /// with a birthday older than but exclusive <paramref name="birthday"/> will be loaded.
            /// Otherwise all <see cref="Player"/> younger than inclusive <paramref name="birthday"/> will be loaded.</param>
            /// <returns>The list of <see cref="Player"/> instance matching the specified birthday filter.</returns>
            private List<Player> LoadByBirthdayFilter(DateTime birthday, bool olderThan)
            {
                DbTableMap map = database.Config.GetMap(typeof(Player));
                string birthdayColumn = map.GetColumnForMemberName("Birthday");

                QueryOperator op = olderThan ? QueryOperator.Smaller : QueryOperator.BiggerEqual;
                QueryConstraint constraint = new QueryConstraint(birthdayColumn, birthday, op);

                return database.LoadAll<Player>(constraint);
            }

            #endregion

        #endregion

        #region Service Functions - Save / Update

        /// <summary>
        /// Saves the specified <paramref name="player"/> and reference meta information to the database.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to be saved.</param>
        public override void Save(Player player)
        {
            // Save the player and create also new reference and history entry

            base.Save(player);

            if (player.Reference != null)
            {
                database.SaveObject<PlayerReference>(player.Reference);
            }
            if (player.DataHistory != null)
            {
                database.SaveObject<PlayerDataHistory>(player.DataHistory);
            }
        }

        /// <summary>
        /// Saves all specified <see cref="Player"/> objects and reference meta information to the database.
        /// </summary>
        /// <param name="players">The set of <see cref="Player"/> objects to be saved.</param>
        public override void SaveAll(IEnumerable<Player> players)
        {
            // Save all players and create new reference and history entries

            base.SaveAll(players);
            database.BulkSaveObject<PlayerReference>(players.Select(p => p.Reference));
            database.BulkSaveObject<PlayerDataHistory>(players.Select(p => p.DataHistory));
        }

        /// <summary>
        /// Updates the specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to be updated.</param>
        public override void Update(Player player)
        {
            base.Update(player);

            // Compare property values for equality

            UpdateHistory(player);
            UpdatePlayerReference(player);

            // Update< old / insert new temporal data entry in database
        }

        /// <summary>
        /// Updates all the specified <paramref name="players"/>.
        /// </summary>
        /// <param name="players">The <see cref="Player"/> objects to be updated.</param>
        public override void UpdateAll(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                Update(player);
            }
        }

        /// <summary>
        /// Updates the player reference for specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to update the refernce for.</param>
        public void UpdatePlayerReference(Player player)
        {
            // Compare property values for equality
            // Update< old / insert new temporal data entry in database

            PlayerReferenceComparison referenceComparison = new PlayerReferenceComparison();
            PlayerReference previousRef = database.LoadById<PlayerReference>(player.Reference.Id);
            PlayerReference currentRef = player.Reference;

            if (!referenceComparison.IsEqual(previousRef, currentRef))
            {
                database.UpdateObject<PlayerReference>(previousRef);
                database.SaveObject<PlayerReference>(currentRef);
            }
        }

        /// <summary>
        /// Updates the data history for specified <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to update the data history for.</param>
        public void UpdateHistory(Player player)
        {
            // Compare property values for equality
            // Update< old / insert new temporal data entry in database

            PlayerDataHistoryComparison historyComparison = new PlayerDataHistoryComparison();
            PlayerDataHistory previousHistory = database.LoadById<PlayerDataHistory>(player.DataHistory.Id);
            PlayerDataHistory currentHistory = player.DataHistory;

            if (!historyComparison.IsEqual(previousHistory, currentHistory))
            {
                database.UpdateObject<PlayerDataHistory>(previousHistory);
                database.SaveObject<PlayerDataHistory>(currentHistory);
            }
        }

        #endregion

        #region Service Functions - Delete

        /// <summary>
        /// Deletes the specified <paramref name="player"/> and all of its references in cascading deletions.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> object to be deleted.</param>
        public override void Delete(Player player)
        {
            base.Delete(player);
        }

        /// <summary>
        /// Deletes all specified <paramref name="players"/> and all of their references in cascading deletions.
        /// </summary>
        /// <param name="players">The <see cref="Player"/> objects to be deleted.</param>
        public override void DeleteAll(IEnumerable<Player> players)
        {
            base.DeleteAll(players);
        }

        #endregion
    }
}
