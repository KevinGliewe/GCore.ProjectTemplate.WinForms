using WeifenLuo.WinFormsUI.Docking;

namespace GCore.ProjectTemplate.WinForms.ToolWindows.Administration;

public interface IToolWindowBase : IDockContent
{
    void ToolWindowShow(DockPanel dockPanel);
}