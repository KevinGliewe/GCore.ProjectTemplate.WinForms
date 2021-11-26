using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCore.ProjectTemplate.WinForms.MainFormPlugins
{
    public class MainToolStrip : ToolStripBase
    {
        public MainToolStrip(IMainForm form, MainStatusStrip statusStrip) : base(form)
        {
            AddButton(ImageListId.Add, b => MessageBox.Show("Hello from ToolStrip!") );
            AddSeparator();
            AddButton(ImageListId.RunUpdate, "Toggle status visibility", b => statusStrip.ToggleVisibility());
        }
    }
}
