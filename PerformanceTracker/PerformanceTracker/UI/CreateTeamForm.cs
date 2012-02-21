using System;
using System.Drawing;
using Vema.PerfTracker.Database.Domain;

namespace Vema.PerformanceTracker.UI
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 20:12
    /// Form to insert new teams right before the main application is started
    /// </summary>
    internal partial class CreateTeamForm : BaseForm
    {
        private readonly Database database;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamForm"/> class.
        /// </summary>
        /// <param name="database">The associated <paramref name="database"/> interface.</param>
        internal CreateTeamForm(Database database) : base()
        {
            this.database = database;

            InitializeComponent();

            SetText("Neues Team");
            lblStatus.Text = "Bitte Teamdetails einfügen ...";
        }

        /// <summary>
        /// Determines whether the inserted descriptor is valid,
        /// i.e. not equal to <see cref="string.Empty"/> or <c>null</c>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the descriptor input is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidInput()
        {
            return !string.IsNullOrEmpty(txtDescriptor.Text);
        }

        /// <summary>
        /// Updates the status message by setting <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        private void UpdateStatus(string text)
        {
            lblStatus.Text = text;
        }

        /// <summary>
        /// Handles the Click event of the btnCreate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!IsValidInput())
            {
                // Notify wrong insertion

                txtDescriptor.BackColor = Color.Tomato;
                UpdateStatus("Pflichtfeld darf nicht leer sein.");

                return;
            }

            string descriptor = txtDescriptor.Text;
            string ageGroup = txtAgeGroup.Text;

            if (!database.ExistsTeam(descriptor))
            {
                database.SaveTeam(txtDescriptor.Text, txtAgeGroup.Text);
                UpdateStatus("Neues Team eingefügt ...");
            }
            else
            {
                if (Gui.AskQuestion("Team-Bezeichner existiert bereits",
                    "Der angegebene Bezeichner für das Team existiert bereits auf der Datenbank. Möchten Sie die alte Teamdefinition sowie alle Referenzen dadurch ungültig machen?"))
                {
                    database.InvalidateTeam(descriptor, ageGroup);
                    UpdateStatus("Team-Bezeicher aktualisiert ...");
                }
            }

            txtDescriptor.BackColor = Color.White;
        }
    }
}
