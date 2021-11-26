using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCore.ProjectTemplate.WinForms.ToolWindows.Administration;
using WeifenLuo.WinFormsUI.Docking;

namespace GCore.ProjectTemplate.WinForms.ToolWindows
{
    public partial class LogWindow : ToolWindowBase, ILogWindow
    {
        public LogWindow()
        {
            InitializeComponent();
            Program.SystemManager.DebugLogging = false;
        }

        public override void ToolWindowShow(DockPanel dockPanel)
        {
            base.Show(dockPanel, DockState.DockBottom);
        }
    }
}
