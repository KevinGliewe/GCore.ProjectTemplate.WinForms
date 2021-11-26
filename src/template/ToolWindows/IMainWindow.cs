using GCore.ProjectTemplate.WinForms.Handler.Attributes;
using GCore.ProjectTemplate.WinForms.ToolWindows.Administration;

namespace GCore.ProjectTemplate.WinForms.ToolWindows;

[ToolWindow("Main Window")]
[Handler("ToolWindows:MainWindow", nameof(MainWindow))]
[Lifetime(LifetimeAttribute.Lifetime.Singleton)]
public interface IMainWindow : IToolWindowBase
{
    
}