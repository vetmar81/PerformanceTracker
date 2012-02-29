using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vema.PerfTracker.Database.Domain;
using Vema.PerformanceTracker.UI.Binding;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:48
    /// Represents the main form of the application.
    /// </summary>
    internal partial class MainForm : BaseForm
    {
        private readonly ApplicationConfig appConfig;
        private readonly Database database;

        private string teamDescriptor;

        private string recentlyUpdatedTeam;

        // Binding lists for data grid views

        private BindingList<PlayerRowEntry> playerEntries;
        private BindingList<PlayerDataRowEntry> playerDataRowEntries;
        private BindingList<PlayerMeasurementRowEntry> playerMeasurementEntries;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        internal MainForm(ApplicationConfig appConfig, Database database, string teamDescriptor) : base()
        {
            this.appConfig = appConfig;
            this.database = database;
            this.teamDescriptor = teamDescriptor;

            InitializeComponent();

            Width = appConfig.GuiWidth;
            Height = appConfig.GuiHeight;

            SetText("Erfassung, Verwaltung & Analyse von Leistungsdaten");
            lblTitle.Text = AppInfo.AssemblyTitle;

            playerEntries = new BindingList<PlayerRowEntry>();
            playerDataRowEntries = new BindingList<PlayerDataRowEntry>();
            playerMeasurementEntries = new BindingList<PlayerMeasurementRowEntry>();

            InitializeTeamCombobox();
        }

        /// <summary>
        /// Determines whether any <see cref="Team"/> is selected in the combo box.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if any <see cref="Team"/> is selected; otherwise, <c>false</c>.
        /// </returns>
        private bool IsTeamSelected()
        {
            return !string.IsNullOrEmpty(teamDescriptor);
        }

        /// <summary>
        /// Preselects the team on initial loading of main form.
        /// </summary>
        private void PreselectTeam()
        {
            if (!IsTeamSelected())
            {
                cbxTeams.SelectedIndex = -1;
                return;
            }

            SelectTeamInCombobox(teamDescriptor);
        }

        /// <summary>
        /// Selects the team with specified <paramref name="descriptor"/> in combobox.
        /// </summary>
        /// <param name="descriptor">The descriptor to be selected.</param>
        private void SelectTeamInCombobox(string descriptor)
        {
            int teamIndex = FindTeamIndex(descriptor);
            if (teamIndex != -1)
            {
                cbxTeams.SelectedIndex = teamIndex;
            }
        }

        /// <summary>
        /// Updates the team according to currently selected descriptor..
        /// </summary>
        /// <param name="selectedDescriptor">The selected team descriptor.</param>
        private void UpdateTeam(string selectedDescriptor)
        {
            teamDescriptor = selectedDescriptor;
            LoadTeam();
        }

        /// <summary>
        /// Loads the team according to current selection.
        /// </summary>
        private void LoadTeam()
        {
            // Clear all binding lists

            ClearBindings();

            if (!string.IsNullOrEmpty(teamDescriptor))
            {
                Team team = database.LoadCurrentTeam(teamDescriptor);
                UpdateTeamDetails(team);

                LoadPlayers(team);
            }
        }

        /// <summary>
        /// Loads the players for selected <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> to load players for.</param>
        private void LoadPlayers(Team team)
        {
            foreach (Player player in database.LoadAllPlayersOfTeam(team))
            {
                PlayerRowEntry entry = new PlayerRowEntry(player);
                playerEntries.Add(entry);
            }

            if (playerEntries.Count > 0)
            {
                dgvPlayers.DataSource = playerEntries;
            }
            else
            {
                UpdatePlayerMenuItems(false);
            }
        }

        /// <summary>
        /// Updates the team details for the selected <see cref="Team"/>.
        /// </summary>
        /// <param name="selectedTeam">The selected <see cref="Team"/>.</param>
        private void UpdateTeamDetails(Team selectedTeam)
        {
            lblNameValue.Text = selectedTeam.Descriptor;
            lblAgeGroupValue.Text = selectedTeam.AgeGroup;
            lblCreationDateValue.Text = selectedTeam.ValidFrom.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Updates the player history and performance data and loads it to the data grid.
        /// </summary>
        /// <param name="selectedPlayer">The <see cref="Player"/> to update data for.</param>
        private void UpdatePlayerData(Player selectedPlayer)
        {
            playerDataRowEntries.Clear();
            playerMeasurementEntries.Clear();

            if (selectedPlayer != null)
            {
                List<PlayerDataHistory> historyEntries = database.LoadCompleteHistory(selectedPlayer);
                List<Measurement> measurements = database.LoadMeasurements(selectedPlayer);

                foreach (PlayerDataHistory history in historyEntries)
                {
                    playerDataRowEntries.Add(new PlayerDataRowEntry(history, selectedPlayer.Id));
                }

                foreach (Measurement measurement in measurements)
                {
                    playerMeasurementEntries.Add(new PlayerMeasurementRowEntry(measurement, selectedPlayer.Id));
                }
            }

            dgvPlayerHistory.DataSource = playerDataRowEntries;
            dgvPlayerMeasurements.DataSource = playerMeasurementEntries;
        }

        /// <summary>
        /// Initializes the team combobox.
        /// </summary>
        private void InitializeTeamCombobox()
        {
            cbxTeams.Items.AddRange(database.LoadAllTeamDescriptors().ToArray());

            cbxTeams.SelectedIndexChanged += cbxTeams_SelectedIndexChanged;
        }

        /// <summary>
        /// Updates the team combobox.
        /// </summary>
        private void UpdateTeamCombobox()
        {
            cbxTeams.Items.Clear();
            cbxTeams.SelectedIndexChanged -= cbxTeams_SelectedIndexChanged;

            cbxTeams.Items.AddRange(database.LoadAllTeamDescriptors().ToArray());

            cbxTeams.SelectedIndexChanged += cbxTeams_SelectedIndexChanged;
        }

        /// <summary>
        /// Finds the index of the team in the combo box 
        /// for team selection of specified <paramref name="teamDescriptor"/>.
        /// </summary>
        /// <param name="teamDescriptor">The team descriptor.</param>
        /// <returns>The index of the <paramref name="teamDescriptor"/> in the combo box
        /// or -1, if not available.</returns>
        private int FindTeamIndex(string teamDescriptor)
        {
            if (string.IsNullOrEmpty(teamDescriptor)) { return -1; }

            for (int i = 0; i < cbxTeams.Items.Count; i++)
            {
                if (cbxTeams.Items[i].ToString() == teamDescriptor)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Updates the status label in status bar by setting the <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text to be set as status.</param>
        private void UpdateStatus(string text)
        {
            statusLabel.Text = text;
        }

        /// <summary>
        /// Updates the player menu items according to the state of current player selection.
        /// </summary>
        /// <param name="isPlayerSelected">if set to <c>true</c>, this indicates a player is selected.</param>
        private void UpdatePlayerMenuItems(bool isPlayerSelected)
        {
            editPlayerDataMenuItem.Enabled = isPlayerSelected;
            newMeasurementMenuItem.Enabled = isPlayerSelected;
            editMeasurementMenuItem.Enabled = isPlayerSelected;
        }

        /// <summary>
        /// Updates the team menu items according to the state of current team selection.
        /// </summary>
        /// <param name="isPlayerSelected">if set to <c>true</c>, this indicates a team is selected.</param>
        private void UpdateTeamMenuItems(bool isTeamSelected)
        {
            editTeamPlayersMenuItem.Enabled = isTeamSelected;
        }

        /// <summary>
        /// Shows the form to edit a single player.
        /// </summary>
        /// <returns></returns>
        private DialogResult ShowEditPlayerForm(EditMode mode)
        {
            Player selectedPlayer = null;

            if (dgvPlayers.SelectedRows != null && dgvPlayers.SelectedRows.Count == 1)
            {
                DataGridViewRow selection = dgvPlayers.SelectedRows[0];
                selectedPlayer = ((PlayerRowEntry) selection.DataBoundItem).Player;
            }
            
            EditPlayerForm editForm = null;

            if (mode == EditMode.Create)
            {
                editForm = new EditPlayerForm(mode, teamDescriptor);
            }
            else if (mode == EditMode.Update && selectedPlayer != null)
            {
                editForm = new EditPlayerForm(mode, teamDescriptor, selectedPlayer.Id);
            }
            else
            {
                Gui.ShowError("Problem", "Unerwarteter Fehler beim Öffnen des Dialogs.");
                return DialogResult.Cancel;
            }

            DialogResult result = editForm.ShowDialog(this);

            LoadTeam();

            return result;
        }

        /// <summary>
        /// Shows the form to edit players for currently selected team.
        /// </summary>
        /// <returns></returns>
        private DialogResult ShowEditTeamPlayersForm()
        {
            EditTeamPlayersForm editForm = new EditTeamPlayersForm(teamDescriptor);
            DialogResult result = editForm.ShowDialog(this);

            LoadTeam();

            return result;
        }

        /// <summary>
        /// Shows the form to create a new team.
        /// </summary>
        /// <returns></returns>
        private DialogResult ShowCreateTeamForm()
        {
            CreateTeamForm createForm = new CreateTeamForm(database, true);
            DialogResult result = createForm.ShowDialog(this);

            // Set the team, that was recently updated / created

            recentlyUpdatedTeam = createForm.UpdatedTeam;

            return result;
        }

        /// <summary>
        /// Clears the binding lists.
        /// </summary>
        private void ClearBindings()
        {
            // Clear all binding lists

            playerEntries.Clear();
            playerDataRowEntries.Clear();
            playerMeasurementEntries.Clear();
        }

        #region Event Handling

        /// <summary>
        /// Handles the Click event of the aboutMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        /// <summary>
        /// Handles the Click event of the exitMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbxTeams control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbxTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsTeamSelected())
            {
                UpdateTeamMenuItems(false);
                UpdateStatus("Bereit - keine Mannschaft ausgewählt ...");
            }

            UpdateTeamMenuItems(true);
            UpdateStatus("Aktualisiere Spieler ...");

            string selectedTeamDescriptor = cbxTeams.SelectedItem.ToString();
            UpdateTeam(selectedTeamDescriptor);

            UpdateStatus(string.Format("Spieler von Mannschaft '{0}' geladen ...", selectedTeamDescriptor));
        }

        /// <summary>
        /// Handles the SelectionChanged event of the dgvPlayers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void dgvPlayers_SelectionChanged(object sender, EventArgs e)
        {
            Player selectedPlayer = null;

            if (dgvPlayers.SelectedRows.Count == 1)
            {
                UpdatePlayerMenuItems(true);

                DataGridViewRow selection = dgvPlayers.SelectedRows[0];
                selectedPlayer = ((PlayerRowEntry) selection.DataBoundItem).Player;

                UpdatePlayerData(selectedPlayer);

                UpdateStatus(string.Format("Daten für Spieler '{0}' geladen ...",
                                            string.Concat(selectedPlayer.FirstName, " ", selectedPlayer.LastName)));
            }
            else
            {
                UpdatePlayerMenuItems(false);

                // Clear binding lists depending on the player selection

                playerDataRowEntries.Clear();
                playerMeasurementEntries.Clear();
            }
        }

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            PreselectTeam();

            dgvPlayers.SelectionChanged += dgvPlayers_SelectionChanged;
            dgvPlayers.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event of the newTeamMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void newTeamMenuItem_Click(object sender, EventArgs e)
        {
            if (Gui.IsDialogCancelled(ShowCreateTeamForm()))
            {
                return;
            }

            // Update combobox and select recently added / manipulated team

            UpdateTeamCombobox();
            SelectTeamInCombobox(recentlyUpdatedTeam);

            if (Gui.AskQuestion("Spieler hinzufügen", "Möchten Sie zur erstellen Mannschaft Spieler hinzufügen?"))
            {
                ShowEditTeamPlayersForm();
            }
        }

        /// <summary>
        /// Handles the Click event of the addPlayersMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void editTeamPlayersMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditTeamPlayersForm();
        }

        /// <summary>
        /// Handles the Click event of the newPlayerMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void newPlayerMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditPlayerForm(EditMode.Create);
        }

        /// <summary>
        /// Handles the Click event of the editPlayerDataMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void editPlayerDataMenuItem_Click(object sender, EventArgs e)
        {
            ShowEditPlayerForm(EditMode.Update);
        }

        #endregion 

        
    }
}
