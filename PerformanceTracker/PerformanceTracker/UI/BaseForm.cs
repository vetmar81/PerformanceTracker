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
    internal partial class BaseForm : Form
    {
        protected BaseForm()
        {
            InitializeComponent();
        }

        protected void SetText(string text)
        {
            Text = string.Format("{0} - {1}", AppInfo.AssemblyTitle, text);
        }
    }
}
