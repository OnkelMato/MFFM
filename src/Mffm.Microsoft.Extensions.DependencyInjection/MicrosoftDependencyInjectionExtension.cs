using System.Reflection;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;
using Mffm.Core.ControlBindings;
using Microsoft.Extensions.DependencyInjection;

namespace Mffm.Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Abstraction for dependency injection framework and abstracts the MFFM related parts.
/// Reduces the main function to a minimum.
/// </summary>
public static class MicrosoftDependencyInjectionExtension
{
    public static void ConfigureMffm(this IServiceCollection services, params Assembly[] assemblies)
    {
        // todo use a DI adapter to we can move this registration code to the mffm core and make almost everything internal
        var formMapperFactory = new DefaultFormMapperBuilder();
        foreach (var assembly in assemblies)
            formMapperFactory.RegisterAssembly(assembly);
        services.AddSingleton(formMapperFactory.Build(t => services.AddTransient(t)));

        services.AddSingleton<IBindingManager, BindingManager>();
        services.AddSingleton<IWindowManager, WindowManager>();
        services.AddSingleton<IEventAggregator, EventAggregator>();

        // no service provider required as it is already implementing the correct "System.Component" interface by default
        // which is used later.

        // add additional (generic) commands
        services.AddTransient<CloseFormCommand>();

        // add all control bindings are added with reflection so it automatically picks up new bindings
        // the default binding needs to be registered first so it is the last one to be checked
        services.AddTransient<IControlBinding, DefaultBinding>();
        typeof(DefaultBinding).Assembly.GetTypes()
            .Where(t => typeof(IControlBinding).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false } && (t != typeof(DefaultBinding)))
            .ToList()
            .ForEach(t => services.AddTransient(typeof(IControlBinding), t));
    }

    public static IServiceProvider Run<TFormModel>(this IServiceProvider provider)
        where TFormModel : class, IFormModel
    {
        var windowManager = provider.GetService<IWindowManager>() ??
                            throw new ServiceNotFoundException("cannot find window manager for MFFM pattern");
        windowManager.Run<TFormModel>();

        return provider;
    }
}