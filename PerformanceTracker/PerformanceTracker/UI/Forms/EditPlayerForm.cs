using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Access;
using Vema.PerformanceTracker.UI;
using Vema.PerfTracker.Database;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 28.02.2012 23:00
    /// Dialog to edit single player for a certain team.
    /// </summary>
    internal partial class EditPlayerForm : BaseForm
    {
        private readonly EditMode editMode;
        private readonly string teamDescriptor;
        private readonly long playerId;
        private readonly Database database;
        private readonly ApplicationConfig appConfig;

        private Player player;
        private PlayerDataHistory dataHistory;
        private Team team;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPlayerForm"/> class.
        /// </summary>
        /// <param name="editMode">The <see cref="EditMode"/> to be used.</param>
        /// <param name="teamDescriptor">The team descriptor.</param>
        internal EditPlayerForm(EditMode editMode, string teamDescriptor)
            : this(editMode, teamDescriptor, -1)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPlayerForm"/> class.
        /// </summary>
        /// <param name="editMode">The <see cref="EditMode"/> to be used.</param>
        /// <param name="teamDescriptor">The team descriptor.</param>
        /// <param name="playerId">The player database ID.</param>
        internal EditPlayerForm(EditMode editMode, string teamDescriptor, long playerId) 
            : base()
        {
            this.editMode = editMode;
            this.teamDescriptor = teamDescriptor;
            this.playerId = playerId;

            database = Database.Instance;
            appConfig = ApplicationConfig.Instance;

            InitializeComponent();

            cbxCountries.DataSource = appConfig.Countries;

            PreloadDatabaseInfo();

            UpdateControls();
            DefineHeaderText();
        }

        /// <summary>
        /// Defines the header text according to the edit mode.
        /// </summary>
        private void DefineHeaderText()
        {
            StringBuilder builder = new StringBuilder();

            if (editMode == EditMode.Create)
            {
                builder.Append("Neuen Spieler hinzufügen:");
            }
            if (editMode == EditMode.Update)
            {
                string playerName = (player != null) ? player.FullName : string.Empty;
                builder.AppendFormat("Spieler '{0}' bearbeiten:", playerName);
            }
            if (!string.IsNullOrEmpty(teamDescriptor))
            {
                builder.AppendFormat(" Mannschaft '{0}'", teamDescriptor);
            }

            SetText(builder.ToString());
        }

        /// <summary>
        /// Updates the controls according to the edit mode.
        /// </summary>
        private void UpdateControls()
        {
            if (editMode == EditMode.Create)
            {
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                datePicker.Enabled = true;
                cbxCountries.Enabled = true;
                txtHeight.Enabled = true;
                txtWeight.Enabled = true;
                rtxtRemark.Enabled = true;

                btnSave.Text = "Speichern";
            }
            else if (editMode == EditMode.Update)
            {
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                datePicker.Enabled = false;
                cbxCountries.Enabled = false;
                txtHeight.Enabled = true;
                txtWeight.Enabled = true;
                rtxtRemark.Enabled = true;

                btnSave.Text = "Aktualisieren";
            }
        }

        /// <summary>
        /// Preloads the player info, if in update mode.
        /// </summary>
        private void PreloadDatabaseInfo()
        {
            if (editMode == EditMode.Create)
            {
                team = database.LoadCurrentTeam(teamDescriptor);
            }

            if (playerId != -1)
            {
                player = database.LoadPlayer(playerId);
                dataHistory = database.GetCurrentHistory(player);

                txtFirstName.Text = player.FirstName;
                txtLastName.Text = player.LastName;
                datePicker.Value = player.Birthday;
                cbxCountries.SelectedItem = appConfig.GetByCode(player.Country);
            }
        }

        /// <summary>
        /// Validates the mandatory fields for correct input values.
        /// </summary>
        /// <returns><c>true</c>, if mandatory fields contain correct input values.</returns>
        private bool ValidateMandatoryFields()
        {
            bool test = InputValueValidator.IsValidString(txtFirstName.Text);
            test &= InputValueValidator.IsValidString(txtLastName.Text);
            test &= InputValueValidator.IsValidString(cbxCountries.Text);

            return test;
        }

        /// <summary>
        /// Creates a new <see cref="Player"/>.
        /// </summary>
        /// <returns>The new <see cref="Player"/>.</returns>
        private Player CreateNew()
        {
            PlayerDao playerDao = (PlayerDao) DaoFactory.CreateDao<Player>();
            PlayerDataHistoryDao historyDao = (PlayerDataHistoryDao) DaoFactory.CreateDao<PlayerDataHistory>();
            PlayerReferenceDao referenceDao = (PlayerReferenceDao) DaoFactory.CreateDao<PlayerReference>();

            // Set the player reference infos

            referenceDao.PlayerDao = playerDao;
            referenceDao.TeamDao = team.Dao;

            // Set the history infos

            historyDao.Height = (string.IsNullOrEmpty(txtHeight.Text)) ? null : (int?) int.Parse(txtHeight.Text);
            historyDao.Weight = (string.IsNullOrEmpty(txtWeight.Text)) ? null : (double?) double.Parse(txtWeight.Text);
            historyDao.Remark = rtxtRemark.Text;

            // Set the player infos

            playerDao.FirstName = txtFirstName.Text;
            playerDao.LastName = txtLastName.Text;
            playerDao.Birthday = datePicker.Value;
            playerDao.Country = ((ApplicationConfig.CountryCodeItem) cbxCountries.SelectedItem).Code;
            playerDao.DataHistoryDao = historyDao;
            playerDao.ReferenceDao = referenceDao;

            // Create the corresponding domain object

            return (Player) playerDao.CreateDomainObject();
        }

        /// <summary>
        /// Creates an updated <see cref="Player"/>.
        /// </summary>
        /// <returns>The updated <see cref="Player"/>.</returns>
        private Player CreateUpdate()
        {
            PlayerDao playerDao = player.Dao;
            PlayerDataHistoryDao historyDao = dataHistory.Dao;

            historyDao.Height = (string.IsNullOrEmpty(txtHeight.Text)) ? null : (int?) int.Parse(txtHeight.Text);
            historyDao.Weight = (string.IsNullOrEmpty(txtWeight.Text)) ? null : (double?) double.Parse(txtWeight.Text);
            historyDao.Remark = rtxtRemark.Text;

            playerDao.DataHistoryDao = historyDao;

            return (Player) playerDao.CreateDomainObject();
        }

        #region Event Handling

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
                if (editMode == EditMode.Create)
                {
                    if (ValidateMandatoryFields())
                    {
                        // Create a new object and save entirely

                        Player player = CreateNew();
                        database.SavePlayer(player);
                    }
                    else
                    {
                        Gui.ShowError("Ungültige Eingabe", "Ein oder mehrere Pflichtfelder enthalten ungültige Werte.");
                        return;
                    }
                }
                else if (editMode == EditMode.Update)
                {
                    // Create an updated object and update only the history

                    Player player = CreateUpdate();
                    database.InvalidatePlayerHistory(player);
                }

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

            if (InputValueValidator.IsValidString(control.Text))
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

                if (InputValueValidator.IsValidPositiveInteger(control.Text))
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

                if (InputValueValidator.IsValidPositiveDouble(control.Text))
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
