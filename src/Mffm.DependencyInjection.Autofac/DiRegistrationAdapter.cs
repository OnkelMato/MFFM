using Autofac;
using Mffm.Contracts;

namespace Mffm.DependencyInjection.Autofac;

internal class DiRegistrationAdapter(ContainerBuilder containerBuilder) 
    : IServiceRegistrationAdapter
{
    private readonly ContainerBuilder _containerBuilder = containerBuilder ?? throw new ArgumentNullException(nameof(containerBuilder));

    public void RegisterSingletonType(Type inf, Type impl)
    {
        _containerBuilder.RegisterType(impl).As(inf).SingleInstance();
    }

    public void RegisterTransientType(Type inf, Type impl)
    {
        if (inf.IsGenericTypeDefinition)
            _containerBuilder.RegisterGeneric(impl).As(inf).InstancePerDependency();
        else
            _containerBuilder.RegisterType(impl).As(inf).InstancePerDependency();
    }

    public void RegisterSingletonInstance(Type inf, object impl)
    {
        _containerBuilder.RegisterInstance(impl).As(inf).SingleInstance();
    }
}