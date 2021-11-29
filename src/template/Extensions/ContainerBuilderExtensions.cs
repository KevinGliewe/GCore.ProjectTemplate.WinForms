using Autofac;
using GCore.ProjectTemplate.WinForms.Handler.Attributes;

namespace GCore.ProjectTemplate.WinForms.Extensions;

public static class ContainerBuilderExtensions
{
    public static ContainerBuilder Add(this ContainerBuilder self, Type service, Type implementation, LifetimeAttribute.Lifetime lifetime)
    {
        switch (lifetime)
        {
            case LifetimeAttribute.Lifetime.Singleton:
                self.AddSingleton(service, implementation);
                break;
            case LifetimeAttribute.Lifetime.Transient:
                self.AddTransient(service, implementation);
                break;
            case LifetimeAttribute.Lifetime.Scoped:
                self.AddScoped(service, implementation);
                break;
            case LifetimeAttribute.Lifetime.Default:
                self.Add(service, implementation, LifetimeAttribute.DEFAULT);
                break;
        }
        return self;
    }

    public static ContainerBuilder AddSingleton<T>(this ContainerBuilder self, T instance) where T : class
    {
        self.RegisterInstance(instance).As<T>().SingleInstance();
        return self;
    }

    public static ContainerBuilder AddSingleton(this ContainerBuilder self, object instance, Type service)
    {
        self.RegisterInstance(instance).As(service).SingleInstance();
        return self;
    }

    public static ContainerBuilder AddSingleton<T>(this ContainerBuilder self) where T : class
    {
        self.RegisterType<T>().SingleInstance();
        return self;
    }

    public static ContainerBuilder AddSingleton<TService, TImpl>(this ContainerBuilder self) where TService : class where TImpl : class
    {
        self.RegisterType<TImpl>().As<TService>().SingleInstance();
        return self;
    }

    public static ContainerBuilder AddSingleton(this ContainerBuilder self, Type service, Type implementation)
    {
        self.RegisterType(implementation).As(service).SingleInstance();
        return self;
    }

    public static ContainerBuilder AddTransient(this ContainerBuilder self, Type service, Type implementation)
    {
        self.RegisterType(implementation).As(service).InstancePerDependency();
        return self;
    }

    public static ContainerBuilder AddScoped(this ContainerBuilder self, Type service, Type implementation)
    {
        self.RegisterType(implementation).As(service).InstancePerLifetimeScope();
        return self;
    }
}