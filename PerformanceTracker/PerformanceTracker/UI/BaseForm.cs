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
    /// Markus Vetsch, 27.02.2012 13:00
    /// Base class for all forms. Provides style information.
    /// </summary>
    internal partial class BaseForm : Form
    {
        protected BaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the text to be displayed in the title bar of the form.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        protected void SetText(string text)
        {
            Text = string.Format("{0} - {1}", AppInfo.AssemblyTitle, text);
        }
    }
}
