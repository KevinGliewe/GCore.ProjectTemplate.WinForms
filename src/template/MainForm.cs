using System.Diagnostics;
using System.Text;
using Autofac;
using GCore.Logging;
using GCore.ProjectTemplate.WinForms.Config;
using GCore.ProjectTemplate.WinForms.MainFormPlugins;
using GCore.ProjectTemplate.WinForms.ToolWindows.Administration;
using WeifenLuo.WinFormsUI.Docking;

namespace GCore.ProjectTemplate.WinForms
{
    public partial class MainForm : Form, IMainForm
    {
        private IToolWindowsAdmin _toolWindowsAdmin;
        private IContainer _services;
        private MainFormOptions _options;

        public MainForm(IToolWindowsAdmin toolWindowsAdmin, IContainer services, MainFormOptions options)
        {
            InitializeComponent();
            _toolWindowsAdmin = toolWindowsAdmin;
            _services = services;
            _options = options;

            foreach (var entry in _toolWindowsAdmin)
                NotifyToolWindowAdded(entry.Key);

            dockPanel.Theme = new VS2015BlueTheme();

            OpenInitialWindows();

        }

        protected virtual void OpenInitialWindows()
        {
            foreach (var openWindow in _options.OpenWindows)
            {
                OpenToolWindow(openWindow);
            }
        }

        #region GUI Events
        private void menuItemViewToolWindow_Click(object? sender, EventArgs e)
        {
            var windowName = (sender as ToolStripLabel)?.Text ?? throw new Exception();
            OpenToolWindow(windowName);
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("FileVersion : " + AssemblyVersionConstants.FileVersion);
            sb.AppendLine("InformationalVersion : " + AssemblyVersionConstants.InformationalVersion);
            sb.AppendLine("Build Timestamp : " + AppSystemManager.GetBuildDate(typeof(Program).Assembly));
            MessageBox.Show(sb.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var mainFormPlugin in IMainFormPlugin.GetPlugins(_services))
            {
                mainFormPlugin.Loading(this);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var mainFormPlugin in IMainFormPlugin.GetPlugins(_services))
            {
                mainFormPlugin.Closing(this);
            }
        }
        #endregion

        #region IMainForm
        public virtual DockPanel DockPanel => dockPanel;

        public virtual MenuStrip MainMenu => mainMenu;

        public virtual ImageList ImageList => imageList;

        public virtual ToolStripContainer ToolStripContainer => toolStripContainer;

        public virtual StatusStrip StatusStrip => statusStrip;

        public virtual void NotifyToolWindowAdded(string name)
        {
            Debug.Assert(_toolWindowsAdmin.Keys.Contains(name));
            
            var tsl = new ToolStripLabel()
            {
                Text = name,
                Tag = name,
                AutoSize = true
            };
            tsl.Width = TextRenderer.MeasureText(name, tsl.Font).Width;
            tsl.Click += menuItemViewToolWindow_Click;

            menuItemView.DropDownItems.Add(tsl);

            Log.Debug($"NotifyToolWindowAdded '{name}'");
        }

        public virtual ToolWindowBase OpenToolWindow(string name)
        {
            

            var window = _toolWindowsAdmin.GetToolWindow(name);
            window.ToolWindowShow(this.DockPanel);
            return window;
        }
        #endregion
    }
}