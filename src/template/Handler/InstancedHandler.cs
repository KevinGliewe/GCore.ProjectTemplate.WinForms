using System;
using GCore.Logging;

namespace GCore.ProjectTemplate.WinForms.Handler {
    public class InstancedHandler : IInstancedHandler
    {
        public void OnAction()
        {
            Log.Info("InstancedHandler::OnAction()");
        }
    }
}