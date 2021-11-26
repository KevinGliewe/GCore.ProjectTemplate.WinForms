using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GCore.ProjectTemplate.WinForms.ToolWindows.Administration
{
    public partial class ToolWindowBase : DockContent, IToolWindowBase
    {
        public ToolWindowBase()
        {
            AutoScaleMode = AutoScaleMode.Dpi;

            FormClosing += (sender, args) =>
            {
                args.Cancel = true;
                base.Hide();
            };
        }

        public virtual void ToolWindowShow(DockPanel dockPanel)
        {
            base.Show(dockPanel);
        }

    }
}
