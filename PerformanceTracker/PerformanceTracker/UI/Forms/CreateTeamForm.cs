using System;
using System.Drawing;
using Vema.PerfTracker.Database.Domain;
using System.Windows.Forms;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 20:12
    /// Form to insert new teams right before the main application is started
    /// </summary>
    internal partial class CreateTeamForm : BaseForm
    {
        private readonly bool useDialogResult;
        private readonly Database database;

        internal string UpdatedTeam { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamForm"/> class.
        /// </summary>
        /// <param name="database">The associated <paramref name="database"/> interface.</param>
        internal CreateTeamForm(Database database) : this(database, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamForm"/> class.
        /// </summary>
        /// <param name="database">The associated <paramref name="database"/> interface.</param>
        /// <param name="useDialogResult">if set to <c>true</c> the dialog result 
        /// of this instance will be set and evaluated.</param>
        internal CreateTeamForm(Database database, bool useDialogResult)
            : base()
        {
            this.database = database;
            this.useDialogResult = useDialogResult;

            InitializeComponent();

            SetText("Neue Mannschaft");
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

                Gui.SetTextboxError(txtDescriptor);
                UpdateStatus("Pflichtfeld darf nicht leer sein.");

                return;
            }

            string descriptor = txtDescriptor.Text;
            string ageGroup = txtAgeGroup.Text;

            // Check, if team definiton for given descriptor exists on database

            if (database.ExistsCurrentTeam(descriptor))
            {
                if (Gui.AskQuestion("Team-Bezeichner existiert bereits",
                    "Der angegebene Bezeichner für das Team existiert bereits auf der Datenbank. Möchten Sie die alte Teamdefinition sowie alle Referenzen dadurch ungültig machen?"))
                {
                    database.UpdateTeam(descriptor, ageGroup);
                    UpdateStatus("Team-Bezeicher aktualisiert ...");

                    UpdatedTeam = descriptor;
                }

                // Set the dialog result, in case it's supposed to be used

                if (useDialogResult) { DialogResult = DialogResult.OK; }
            }
            else
            {
                database.SaveTeam(descriptor, ageGroup);
                UpdateStatus("Neues Team eingefügt ...");

                UpdatedTeam = descriptor;

                // Set the dialog result, in case it's supposed to be used

                if (useDialogResult) { DialogResult = DialogResult.OK; }
            }

            Gui.ResetTextboxFromError(txtDescriptor);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();

            if (useDialogResult) { DialogResult = DialogResult.Cancel; }
        }

        /// <summary>
        /// Handles the FormClosing event of the CreateTeamForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void CreateTeamForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && useDialogResult)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
