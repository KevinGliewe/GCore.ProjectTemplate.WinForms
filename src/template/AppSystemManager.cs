using Microsoft.Extensions.Configuration;

using System.Reflection;
using Autofac;
using GCore.Logging;
using GCore.ProjectTemplate.WinForms.Extensions;
using System.Diagnostics;

namespace GCore.ProjectTemplate.WinForms {
    public class AppSystemManager {
        public IConfiguration Config { get; private set; }
        public IContainer Services { get; private set; }
        public Config.ApplicationOptions Options { get; private set; } = new Config.ApplicationOptions();

        private DebugLogHandler _debugLogHandler = new DebugLogHandler();

        public bool DebugLogging { get => _debugLogHandler.Active; set => _debugLogHandler.Active = value; }

        public AppSystemManager(Action<IConfigurationBuilder> configBuild, Action<ContainerBuilder, IConfiguration> serviceBuild)
        {
            Log.LoggingHandler.Add(_debugLogHandler);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => Log.Exception("Unhandled Exception", (Exception)args.ExceptionObject);


            // Initialize Configuration
            var configurationBuilder = new ConfigurationBuilder();
            ConfigBuild(configurationBuilder);
            configBuild?.Invoke(configurationBuilder);
            Config = configurationBuilder.Build();

            var prevConColor = Console.ForegroundColor;

            Log.Debug($"Configuration:");
            foreach (var entry in Config.AsEnumerable())
                Log.Debug($"  {entry.Key} = {entry.Value}");


            // Initialize Services
            var serviceCollection = new ContainerBuilder();
            ServiceBuild(serviceCollection);
            serviceBuild?.Invoke(serviceCollection, Config);
            Services = serviceCollection.Build();

            Log.Debug($"Services:");
            foreach (var registeredService in Services.GetRegisteredServiceImplementations())
                Log.Debug($"  {registeredService.Service.FullName} => {registeredService.Implementation.FullName}");

            Options = Services.Resolve<Config.ApplicationOptions>();
        }

        private void ConfigBuild(IConfigurationBuilder builder) {

        }

        private void ServiceBuild(ContainerBuilder builder) {
            builder
                .AddSingleton(Config)
                .AddSingleton(this)
                .Register(c => this.Services).As<IContainer>();
        }

        public static DateTime GetBuildDate(Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<BuildDateAttribute>();
            return attribute != null ? attribute.DateTime : default(DateTime);
        }

        private class DebugLogHandler : GCore.Logging.ILoggingHandler
        {
            public bool Active { get; set; } = true;

            public void Debug(DateTime timestamp, string Message, StackTrace Stacktrace, params object[] list)
            {
            }

            public void Error(DateTime timestamp, string Message, StackTrace Stacktrace, params object[] list)
            {
            }

            public void Exaption(DateTime timestamp, string Message, Exception exception, StackTrace Stacktrace, params object[] list)
            {
                if(Active)
                    GCore.WinForms.Exceptions.ExceptionMessageBox.ShowExceptionMessageBox(exception, Message);
            }

            public void Fatal(DateTime timestamp, string Message, StackTrace Stacktrace, params object[] list)
            {
                if(Active)
                    MessageBox.Show(Message + "\r\n\r\n" + Stacktrace, "Fatal ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            public void Info(DateTime timestamp, string Message, StackTrace Stacktrace, params object[] list)
            {
            }

            public void Success(DateTime timestamp, string Message, StackTrace Stacktrace, params object[] list)
            {
            }

            public void Warn(DateTime timestamp, string Message, StackTrace Stacktrace, params object[] list)
            {
            }

            public void General(LogEntry logEntry)
            {
                
#if (DEBUG)
                if (!Active)
                    return;

                string Text = logEntry.TimeStamp.ToString("yyyy-MM-dd H:mm:ss.fff") + "\t";
                Text += logEntry.LogType.ToString() + "\t";
                Text += logEntry.Message + "\t";
                Text += logEntry.StackTrace.ToString().Split('\n')[1].Replace("\r", "").Trim() + "\t";
                Text += logEntry.Thread.Name + "\t";
                if (logEntry.Exception != null) Text += logEntry.Exception.ToString() + "\t";
                foreach (Object o in logEntry.Params)
                    Text += o.ToString() + ";";

                System.Diagnostics.Debug.WriteLine(Text);
#endif
            }
        }
    }
}