using Autofac;
using GCore.Extensions.StringShEx;
using GCore.Logging;
using GCore.Logging.Logger;
using GCore.ProjectTemplate.WinForms.Config;
using GCore.ProjectTemplate.WinForms.Config.Attributes;
using GCore.ProjectTemplate.WinForms.Extensions;
using GCore.ProjectTemplate.WinForms.Handler.Attributes;
using GCore.ProjectTemplate.WinForms.MainFormPlugins;
using Microsoft.Extensions.Configuration;

namespace GCore.ProjectTemplate.WinForms
{
    internal static class Program
    {
        public static readonly string ENV_PREFIX = "GCore.ProjectTemplate.WinForms";
        public static AppSystemManager SystemManager { get; private set; }

        private static MainForm? _mainForm = null;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (DEBUG)
            Log.LoggingHandler.Add(new DebugLogger());
#endif

            try
            {
                SystemManager = new AppSystemManager(ConfigBuild, ServiceBuild);

                var host = SystemManager.Services.GetService<Handler.ApplicationHandler>();
                host.Start();

                ApplicationConfiguration.Initialize();
                Application.Run(SystemManager.Services.GetService<IMainForm>() as MainForm);

                host.Stop();
            }
            catch (Exception ex)
            {
                GCore.WinForms.Exceptions.ExceptionMessageBox.ShowExceptionMessageBox(ex, "System Error!");
            }
        }

        private static void ConfigBuild(IConfigurationBuilder builder)
        {
            var prefix = ENV_PREFIX.ToUpper().Replace('.', '_') + "_";

            var env = Environment.GetEnvironmentVariable($"{prefix}ENV") ??
#if (DEBUG)
            "Development";
#else
            "Production";
#endif
            Log.Info("Environment: " + env);

            builder.AddInMemoryCollection(new Dictionary<string, string>() {
                    { "Application:Option", "InMemory" },
                    { "Handlers:InstancedHandler", "GCore.ProjectTemplate.ConsoleApp.Handler.InstancedHandler" },
                })
                .AddJsonFile("appsettings.json", optional: true)
                .AddXmlFile("appsettings.xml", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddXmlFile($"appsettings.{env}.xml", optional: true)
                .AddEnvironmentVariables(prefix)
                .AddEnvironmentVariables($"{prefix}{env}_")
                .AddCommandLine(Environment.GetCommandLineArgs());
        }

        private static void ServiceBuild(ContainerBuilder builder, IConfiguration config)
        {
            builder
                .AddSingleton<Handler.ApplicationHandler>()
                .AddSingleton<IMainForm, MainForm>();

            var assemblies = new[] {typeof(Program).Assembly};

            ConfigOptionAttribute.BuildServiceOptions(builder, config, assemblies);
            HandlerAttribute.BuildServiceHandlers(builder, config, assemblies);
            IMainFormPlugin.BuildServicePlugins(builder, config, assemblies);
        }
    }
}