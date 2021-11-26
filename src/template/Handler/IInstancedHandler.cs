using System;
using GCore.ProjectTemplate.WinForms.Handler.Attributes;

namespace GCore.ProjectTemplate.WinForms.Handler {
    [Handler("Handlers:InstancedHandler", nameof(InstancedHandler))]
    [Lifetime(LifetimeAttribute.Lifetime.Singleton)]
    public interface IInstancedHandler {
        void OnAction();
    }
}