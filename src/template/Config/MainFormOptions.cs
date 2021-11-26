using GCore.ProjectTemplate.WinForms.Config.Attributes;

namespace GCore.ProjectTemplate.WinForms.Config;

[ConfigOption("MainForm")]

public sealed record MainFormOptions
{
    public string[] OpenWindows { get; set; } = new string[] { };
}