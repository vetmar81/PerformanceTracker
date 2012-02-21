using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Vema.PerformanceTracker.UI;

namespace Vema.PerformanceTracker
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 14:08
    /// Startup class of the application providing the <see cref="Main()"/> method.
    /// </summary>
    internal static class PerformanceTracker
    {
        /// <summary>
        /// Gets the startup path of the application.
        /// </summary>
        internal static string StartupPath
        {
            get { return Application.StartupPath; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            StartupForm startup = new StartupForm();
                 
            if (startup.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            ApplicationConfig appConfig = startup.AppConfig;
            Database database = startup.Database;
            string selectedTeam = startup.SelectedTeam;

            Application.Run(new MainForm(appConfig, database, selectedTeam));
        }

        /// <summary>
        /// Event handler for any uncaught exception, that might otherwise force application shut down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null)
            {
                Exception ex = (Exception) e.ExceptionObject;
                Gui.ShowError(ex);
            }
        }
    }
}
