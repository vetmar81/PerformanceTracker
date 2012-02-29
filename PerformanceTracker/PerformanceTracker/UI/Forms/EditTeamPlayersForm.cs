using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Vema.PerformanceTracker.UI.Binding;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Access;
using Vema.PerfTracker.Database;
using Vema.PerformanceTracker.UI;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 28.02.2012 18:19
    /// Represents the form to edit players for the selected team
    /// </summary>
    internal partial class EditTeamPlayersForm : BaseForm
    {
        private static DateTime defaultDate = DateTime.Today;

        private readonly string teamDescriptor;
        private readonly Database database;
        private Team team;

        private List<PlayerListItem> addList;
        private List<PlayerListItem> deleteList;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTeamPlayersForm"/> class.
        /// </summary>
        /// <param name="teamDescriptor">The team descriptor to use in this context.</param>
        internal EditTeamPlayersForm(string teamDescriptor)
        {
            this.teamDescriptor = teamDescriptor;
            database = Database.Instance;

            addList = new List<PlayerListItem>();
            deleteList = new List<PlayerListItem>();

            InitializeComponent();

            SetText(string.Format("Spieler zu Mannschaft '{0}' hinzufügen", teamDescriptor));
        }

        /// <summary>
        /// Adapts the size columns of the columns in the list view automatically to best fit mode.
        /// </summary>
        private void AutoSizeColumns()
        {
            lvwPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /// <summary>
        /// Creates a new <see cref="PlayerListItem"/> to be added to the list view.
        /// </summary>
        /// <returns>The created <see cref="PlayerListItem"/>.</returns>
        private PlayerListItem CreatePlayerItem()
        {
            PlayerListItem newPlayerItem = new PlayerListItem();
            newPlayerItem.FirstName = txtFirstName.Text;
            newPlayerItem.LastName = txtLastName.Text;
            newPlayerItem.Birthday = datePicker.Value;
            newPlayerItem.Country = ((ApplicationConfig.CountryCodeItem)cbxCountries.SelectedItem).Code;
            newPlayerItem.Remark = rtxtRemark.Text;
            newPlayerItem.Height = (!string.IsNullOrEmpty(txtHeight.Text)) ? (int?)int.Parse(txtHeight.Text) : null;
            newPlayerItem.Weight = (!string.IsNullOrEmpty(txtWeight.Text)) ? (double?)double.Parse(txtWeight.Text) : null;

            return newPlayerItem;
        }

        /// <summary>
        /// Updates the status of various controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c>, the affected controls are enabled;
        /// otherwise disabled and reset to default.</param>
        private void UpdateControls(bool enable)
        {
            txtFirstName.Enabled = enable;
            txtLastName.Enabled = enable;
            txtHeight.Enabled = enable;
            txtWeight.Enabled = enable;
            datePicker.Enabled = enable;
            cbxCountries.Enabled = enable;
            rtxtRemark.Enabled = enable;

            // Reset to default on disable

            if (!enable)
            {
                txtFirstName.Clear();
                txtLastName.Clear();
                txtHeight.Clear();
                txtWeight.Clear();
                datePicker.Value = defaultDate;
                cbxCountries.ResetText();
                rtxtRemark.Clear();
            }
        }

        /// <summary>
        /// Creates the save list of <see cref="Player"/> to be saved.
        /// </summary>
        /// <returns>The list of <see cref="Player"/> to be saved.</returns>
        private List<Player> CreateSaveList()
        {
            List<Player> saveList = new List<Player>();

            foreach (PlayerListItem addItem in addList)
            {
                saveList.Add(CreatePlayer(addItem));
            }

            return saveList;
        }

        /// <summary>
        /// Creates the <see cref="PlayerReference"/> list to be invalidated.
        /// </summary>
        /// <returns>The list of <see cref="PlayerReference"/> to be invalidated.</returns>
        private List<PlayerReference> CreateUpdateList()
        {
            List<PlayerReference> updateList = new List<PlayerReference>();

            foreach (PlayerListItem deleteItem in deleteList)
            {
                // Load player reference for each player

                database.LoadPlayerReference(deleteItem.Player);
                updateList.Add(deleteItem.Player.Reference);
            }

            return updateList;
        }

        /// <summary>
        /// Creates the <see cref="Player"/> instance of associated <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="PlayerListItem"/> to convert to a <see cref="Player"/>.</param>
        /// <returns>The corresponding <see cref="Player"/></returns>
        private Player CreatePlayer(PlayerListItem item)
        {
            PlayerDao playerDao = (PlayerDao) DaoFactory.CreateDao<Player>();
            PlayerDataHistoryDao historyDao = (PlayerDataHistoryDao) DaoFactory.CreateDao<PlayerDataHistory>();
            PlayerReferenceDao referenceDao = (PlayerReferenceDao) DaoFactory.CreateDao<PlayerReference>();

            // Set the history infos

            historyDao.Height = item.Height;
            historyDao.Weight = item.Weight;
            historyDao.Remark = item.Remark;

            // Set the player reference infos

            referenceDao.PlayerDao = playerDao;
            referenceDao.TeamDao = team.Dao;

            // Set the player infos

            playerDao.FirstName = item.FirstName;
            playerDao.LastName = item.LastName;
            playerDao.Birthday = item.Birthday;
            playerDao.Country = item.Country;
            playerDao.DataHistoryDao = historyDao;
            playerDao.ReferenceDao = referenceDao;

            // Create the corresponding domain object

            return (Player) playerDao.CreateDomainObject();
        }

        /// <summary>
        /// Validates the mandatory fields for correct input values.
        /// </summary>
        /// <returns><c>true</c>, if mandatory fields contain correct input values.</returns>
        private bool ValidateMandatoryFields()
        {
            bool test = PlayerValueValidator.IsValidString(txtFirstName.Text);
            test &= PlayerValueValidator.IsValidString(txtLastName.Text);
            test &= PlayerValueValidator.IsValidString(cbxCountries.Text);

            return test;
        }

        #region Event Handling

        /// <summary>
        /// Handles the Load event of the EditPlayersForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EditPlayersForm_Load(object sender, EventArgs e)
        {
            cbxCountries.DataSource = ApplicationConfig.Instance.Countries;

            // Load available player definitons from database

            team = database.LoadCurrentTeam(teamDescriptor);

            // Load all values from the players

            List<Player> playerList = database.LoadAllPlayersOfTeam(team);
            database.LoadCurrentHistory(playerList);

            lvwPlayers.BeginUpdate();

            // Add items to list view

            foreach (Player player in playerList)
            {
                PlayerListItem playerItem = new PlayerListItem(player);
                ListViewItem listItem = lvwPlayers.Items.Add(new ListViewItem(playerItem.ToArray()));
                listItem.Name = playerItem.Id.ToString();
                listItem.Tag = playerItem;
            }

            // Auto size columns to best fit

            AutoSizeColumns();

            lvwPlayers.EndUpdate();
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateMandatoryFields())
            {
                PlayerListItem newItem = CreatePlayerItem();

                ListViewItem listItem = lvwPlayers.Items.Add(new ListViewItem(newItem.ToArray()));
                listItem.Tag = newItem;
                listItem.Name = newItem.Id.ToString();

                addList.Add(newItem);

                UpdateControls(false);

                btnNewPlayer.Enabled = true;
                btnExistingPlayer.Enabled = true;
            }
            else
            {
                Gui.ShowError("Ungültige Eingabe", "Ein oder mehrere Pflichtfelder enthalten ungültige Werte.");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = lvwPlayers.SelectedItems;

            foreach (ListViewItem item in items)
            {
                // Only add to delete list, if not a new item

                if (item.Name != "-1")
                {
                    deleteList.Add((PlayerListItem) item.Tag);
                }
                                
                lvwPlayers.Items.Remove(item);
            }            
        }

        /// <summary>
        /// Handles the Click event of the btnNewPlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNewPlayer_Click(object sender, EventArgs e)
        {
            UpdateControls(true);
            btnExistingPlayer.Enabled = false;
            txtFirstName.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnExistingPlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnExistingPlayer_Click(object sender, EventArgs e)
        {
            UpdateControls(false);            
            btnNewPlayer.Enabled = false;            
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Gui.AskQuestion("Änderungen verwerfen", "Wollen Sie wirklich alle Änderungen verwerfen und den Dialog verlassen?"))
            {
                Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Gui.AskQuestion("Änderungen speichern", "Wollen Sie die vorgenommenen Änderungen speichern und den Dialog verlassen?"))
            {
                List<Player> saveList = CreateSaveList();
                List<PlayerReference> updateList = CreateUpdateList();

                database.SavePlayerList(saveList);
                database.InvalidatePlayerReferenceList(updateList);

                Close();
            }
        }

        /// <summary>
        /// Handles the Validated event a <see cref="Control"/> expecting a <see cref="string"/> value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ValidatedString(object sender, EventArgs e)
        {
            Control control = (Control) sender;

            // Validate for a string value

            if (PlayerValueValidator.IsValidString(control.Text))
            {
                Gui.ResetTextboxFromError(control);
            }
            else
            {
                Gui.ShowMessage("Ungültige Eingabe", "Pflichtfeld darf nicht leer sein.");
                Gui.SetTextboxError(control);
            }
        }

        /// <summary>
        /// Handles the Validated event a <see cref="Control"/> expecting a <see cref="int"/> value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ValidatedInteger(object sender, EventArgs e)
        {
            Control control = (Control) sender;
            if (!string.IsNullOrEmpty(control.Text))
            {
                // Validate for an integer value

                if (PlayerValueValidator.IsValidInteger(control.Text))
                {
                    Gui.ResetTextboxFromError(control);
                }
                else
                {
                    Gui.ShowMessage("Ungültige Eingabe", "Eingabe muss eine positive ganze Zahl sein.");
                    Gui.SetTextboxError(control);
                }
            }
            else
            {
                Gui.ResetTextboxFromError(control);
            }
        }

        /// <summary>
        /// Handles the Validated event a <see cref="Control"/> expecting a <see cref="double"/> input value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ValidatedDouble(object sender, EventArgs e)
        {
            Control control = (Control) sender;
            if (!string.IsNullOrEmpty(control.Text))
            {
                // Validate for a double value

                if (PlayerValueValidator.IsValidDouble(control.Text))
                {
                    Gui.ResetTextboxFromError(control);
                }
                else
                {
                    Gui.ShowMessage("Ungültige Eingabe", "Eingabe muss eine positive Zahl (mit / ohne Kommastellen) sein.");
                    Gui.SetTextboxError(control);
                }
            }
            else
            {
                Gui.ResetTextboxFromError(control);
            }
        }

        #endregion
    }
}
