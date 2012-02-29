using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 21.02.2012 17:04
    /// Represents about form.
    /// </summary>
    internal partial class AboutForm : BaseForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        internal AboutForm() : base()
        {
            InitializeComponent();

            this.Text = String.Format("About {0}", AppInfo.AssemblyTitle);
            lblTitle.Text = AppInfo.AssemblyTitle;
            lblProducer.Text = AppInfo.AssemblyCompany;
            lblVersionValue.Text = AppInfo.AssemblyVersion;
            lblCopyrightValue.Text = AppInfo.AssemblyCopyright;
            txtDescription.Text = AppInfo.AssemblyDescription;
        }
    }
}
