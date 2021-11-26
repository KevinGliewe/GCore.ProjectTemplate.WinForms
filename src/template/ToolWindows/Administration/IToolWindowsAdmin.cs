using Autofac.Core;
using GCore.Data;
using GCore.ProjectTemplate.WinForms.Handler.Attributes;

namespace GCore.ProjectTemplate.WinForms.ToolWindows.Administration;

[Handler("Handlers:ToolWindowsAdmin", nameof(ToolWindowsAdmin))]
[Lifetime(LifetimeAttribute.Lifetime.Singleton)]
public interface IToolWindowsAdmin : IReadOnlyDictionary<String, Type>
{
    ToolWindowBase GetToolWindow(string name);
    ToolWindowBase GetToolWindow(string name, IEnumerable<Parameter> parameters);
    ToolWindowBase GetToolWindow(string name, params Parameter[] parameters);
}