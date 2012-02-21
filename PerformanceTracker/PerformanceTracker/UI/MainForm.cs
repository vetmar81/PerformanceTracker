using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vema.PerformanceTracker.UI
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        internal MainForm(ApplicationConfig appConfig, Database database, string teamDescriptor) : base()
        {
            this.appConfig = appConfig;
            this.database = database;
            this.teamDescriptor = teamDescriptor;

            InitializeComponent();

            SetText("Erfassung, Verwaltung & Analyse von Leistungsdaten");
            lblTitle.Text = AppInfo.AssemblyTitle;
        }

        /// <summary>
        /// Handles the Click event of the aboutMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }
    }
}
