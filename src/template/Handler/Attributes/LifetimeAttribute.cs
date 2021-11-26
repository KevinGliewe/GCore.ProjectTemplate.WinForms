using System.Reflection;

namespace GCore.ProjectTemplate.WinForms.Handler.Attributes;

[AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
public class LifetimeAttribute : Attribute
{
    public enum Lifetime
    {
        Scoped,
        Singleton,
        Transient,
        Default
    }

    public Lifetime Attribute { get; private set; }

    public LifetimeAttribute(Lifetime attribute)
    {
        Attribute = attribute;
    }

    public static Lifetime GetLifetime(Type type) =>
        type.GetCustomAttribute<LifetimeAttribute>()?.Attribute ?? Lifetime.Default;
}