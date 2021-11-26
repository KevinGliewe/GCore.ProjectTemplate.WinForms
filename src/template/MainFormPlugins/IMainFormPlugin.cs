using System.Reflection;
using Autofac;
using GCore.ProjectTemplate.WinForms.Extensions;
using GCore.ProjectTemplate.WinForms.Handler.Attributes;
using Microsoft.Extensions.Configuration;

namespace GCore.ProjectTemplate.WinForms.MainFormPlugins;

public interface IMainFormPlugin
{
    void Loading(IMainForm form);
    void Closing(IMainForm form);

    public static void BuildServicePlugins(ContainerBuilder builder, IConfiguration config,
        IEnumerable<Assembly> assemblies)
    {
        foreach (var ass in assemblies)
        {
            foreach (var pluginType in ass.GetTypes().Where(t =>
                         !t.IsInterface && !t.IsAbstract && t.IsAssignableTo(typeof(IMainFormPlugin))))
            {
                builder.AddSingleton(pluginType, pluginType);
            }
        }
    }

    public static IEnumerable<Type> GetPluginTypes(IContainer services)
    {
        foreach (var registeredService in services.GetRegisteredServices())
        {
            if (!registeredService.IsInterface && !registeredService.IsAbstract &&
                registeredService.IsAssignableTo(typeof(IMainFormPlugin)))
                yield return registeredService;
        }
    }

    public static IEnumerable<IMainFormPlugin> GetPlugins(IContainer container)
    {
        foreach (var type in GetPluginTypes(container))
        {
            var service = container.Resolve(type) as IMainFormPlugin;
            if (service is not null)
                yield return service;
        }
    }
}