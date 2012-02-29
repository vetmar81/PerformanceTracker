using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:07
    /// Represents the startup form of the application that provides the option to initially select a team
    /// or to even define a new team.
    /// </summary>
    internal partial class StartupForm : BaseForm
    {
        /// <summary>
        /// Gets the application config.
        /// </summary>
        internal ApplicationConfig AppConfig { get; private set; }

        /// <summary>
        /// Gets the database access binding.
        /// </summary>
        internal Database Database { get; private set; }

        /// <summary>
        /// Gets the selected team.
        /// </summary>
        internal string SelectedTeam { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupForm"/> class.
        /// </summary>
        internal StartupForm() : base()
        {
            InitializeComponent();

            SetText("Mannschaft wählen");

            InitializeTeams();
        }

        /// <summary>
        /// Initializes the team descriptors for selection in the combo box.
        /// </summary>
        private void InitializeTeams()
        {
            string configFile = "Application.xml";
            string filePath = Path.Combine(Application.StartupPath, Path.Combine("config", configFile));

            if (File.Exists(filePath))
            {
                AppConfig = LoadConfig(filePath);
                Database = InitDatabase(AppConfig.DbConfigPath);

                LoadTeams();
            }
            else
            {
                Gui.ShowError("Unexpected error during application start",
                                string.Format("Configuration file '{0}' not found.{1}Application will be terminated!",
                                                configFile, Gui.DoubleNewLine));
                Application.Exit();
            }
        }

        /// <summary>
        /// Loads the teams dynmaically into the combo box.
        /// </summary>
        private void LoadTeams()
        {
            cbxTeams.Items.AddRange(Database.LoadAllTeamDescriptors().ToArray());

            if (cbxTeams.Items.Count > 0)
            {
                cbxTeams.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Loads the application configuration from specified <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The file path of the configuration file.</param>
        /// <returns>The <see cref="ApplicationConfig"/> built from the configuration file</returns>
        private ApplicationConfig LoadConfig(string filePath)
        {
            ApplicationConfig.Load(filePath);
            return ApplicationConfig.Instance;
        }

        /// <summary>
        /// Initializes the database access using the configuration in specified <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">The file path of the database configuration file.</param>
        /// <returns>The <see cref="Database"/> built from the configuration file.</returns>
        private Database InitDatabase(string filePath)
        {
            Database.Initialize(filePath);
            return Database.Instance;
        }

        /// <summary>
        /// Determines whether a team descriptor is selected in the combo box.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if a team is selected; otherwise, <c>false</c>.
        /// </returns>
        private bool IsTeamSelected()
        {
            return cbxTeams.SelectedItem != null;
        }

        /// <summary>
        /// Handles the FormClosing event of the StartupForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void StartupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Set dialog result => invalid due to use closing the form

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Handles the Click event of the control <see cref="btnSelect"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (IsTeamSelected())
            {
                // Initialize the main form with a team selection

                SelectedTeam = cbxTeams.SelectedItem.ToString(); 
            }

            Close();

            // Set dialog result => valid

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Click event of the btnNewTeam control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNewTeam_Click(object sender, EventArgs e)
        {
            CreateTeamForm teamForm = new CreateTeamForm(Database);
            teamForm.ShowDialog(this);

            // Reload teams for possible new team / updated team
            cbxTeams.Items.Clear();
            LoadTeams();
        }
    }
}
