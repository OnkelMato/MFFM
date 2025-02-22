using Mffm.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Mffm.DependencyInjection.Microsoft.Extensions;

internal class DiRegistrationAdapter(IServiceCollection containerBuilder) : IServiceRegistrationAdapter
{
    private readonly IServiceCollection _containerBuilder = containerBuilder ?? throw new ArgumentNullException(nameof(containerBuilder));

    public void RegisterSingletonType(Type inf, Type impl)
    {
        _containerBuilder.AddSingleton(inf, impl);
    }

    public void RegisterTransientType(Type inf, Type impl)
    {
        _containerBuilder.AddTransient(inf, impl);
    }

    public void RegisterSingletonInstance(Type inf, object impl)
    {
        _containerBuilder.AddSingleton(inf, impl);
    }
}