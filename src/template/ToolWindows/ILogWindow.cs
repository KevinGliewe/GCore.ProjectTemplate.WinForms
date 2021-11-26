using GCore.ProjectTemplate.WinForms.Handler.Attributes;
using GCore.ProjectTemplate.WinForms.ToolWindows.Administration;

namespace GCore.ProjectTemplate.WinForms.ToolWindows;

[ToolWindow("Log Window")]
[Handler("ToolWindows:LogWindow", nameof(LogWindow))]
[Lifetime(LifetimeAttribute.Lifetime.Singleton)]
public interface ILogWindow: IToolWindowBase
{
    
}