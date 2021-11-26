using Timer = System.Windows.Forms.Timer;

namespace GCore.ProjectTemplate.WinForms.MainFormPlugins;

public class MainStatusStrip : StatusStripBase
{

    private Timer _timer = new Timer()
    {
        Interval = 1000,
        Enabled = true
    };

    private ToolStripStatusLabel _label = new ToolStripStatusLabel();

    public MainStatusStrip(IMainForm mainForm) : base(mainForm)
    {
        //AddLabel("Hello World");
        StripItems.Add(_label);

        _timer.Tick += (sender, args) => _label.Text = DateTime.Now.ToString();
    }

    public void ToggleVisibility()
    {
        _label.Visible = !_label.Visible;
    }
}