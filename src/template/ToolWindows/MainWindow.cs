using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCore.Logging;
using GCore.ProjectTemplate.WinForms.ToolWindows.Administration;

namespace GCore.ProjectTemplate.WinForms.ToolWindows
{
    public partial class MainWindow : ToolWindowBase, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Log.Info("Hello " + DateTime.Now);
        }
    }
}
