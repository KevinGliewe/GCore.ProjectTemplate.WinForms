﻿namespace GCore.ProjectTemplate.WinForms.ToolWindows.Administration;

[AttributeUsageAttribute(AttributeTargets.Interface | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ToolWindowAttribute : Attribute
{
    public String Name { get; private set; }

    public ToolWindowAttribute(string name)
    {
        Name = name;
    }
}