using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GCore.ProjectTemplate.WinForms.Handler.Attributes;
using WeifenLuo.WinFormsUI.Docking;

namespace GCore.ProjectTemplate.WinForms.ToolWindows.Administration
{
    public partial class ToolWindowBase : DockContent, IToolWindowBase
    {
        public LifetimeAttribute.Lifetime Lifetime { get; private set; }

        public ToolWindowBase()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            Lifetime = LifetimeAttribute.GetLifetime(GetType());

            FormClosing += (sender, args) =>
            {
                if (Lifetime == LifetimeAttribute.Lifetime.Singleton)
                {
                    args.Cancel = true;
                    base.Hide();
                }
            };
        }

        public virtual void ToolWindowShow(DockPanel dockPanel)
        {
            base.Show(dockPanel);
        }

    }
}
