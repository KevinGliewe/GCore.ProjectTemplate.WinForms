using System;
using GCore.ProjectTemplate.WinForms.Config.Attributes;


namespace GCore.ProjectTemplate.WinForms.Config {

    [ConfigOption("Application")]
    public sealed record ApplicationOptions {
        public string Option { get; set; } = "default";
    }
}