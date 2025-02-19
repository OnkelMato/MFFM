using System.Reflection;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;
using Mffm.Core.ControlBindings;

namespace Mffm;

/// <summary>
/// Abstraction for dependency injection framework and abstracts the MFFM related parts.
/// Reduces the main function to a minimum.
/// </summary>
public static class MffmDependencyInjectionRegistrations
{
    public static IServiceRegistrationAdapter RegisterServices(this IServiceRegistrationAdapter containerBuilder)
    {
        // register core singleton services
        containerBuilder.RegisterSingletonType(typeof(IBindingManager), typeof(BindingManager));
        containerBuilder.RegisterSingletonType(typeof(IWindowManager), typeof(WindowManager));
        containerBuilder.RegisterSingletonType(typeof(IEventAggregator), typeof(EventAggregator));
        containerBuilder.RegisterSingletonType(typeof(ICommandResolver), typeof(CommandResolver));

        // add additional (generic) commands
        containerBuilder.RegisterTransientType(typeof(CloseFormCommand), typeof(CloseFormCommand));

        // register internal control bindings
        containerBuilder.RegisterTransientType(typeof(IControlBinding), typeof(DefaultBinding));
        typeof(DefaultBinding).Assembly.GetTypes()
            .Where(t => typeof(IControlBinding).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false } &&
                        (t != typeof(DefaultBinding)))
            .ToList()
            .ForEach(t => containerBuilder.RegisterTransientType(typeof(IControlBinding), t));
        return containerBuilder;
    }

    public static IServiceRegistrationAdapter RegisterExtensions(
        this IServiceRegistrationAdapter containerBuilder,
        Assembly[] assemblies)
    {
        // register forms and form models in a dedicated factory
        var formMapperFactory = new DefaultFormMapperBuilder();
        foreach (var assembly in assemblies)
            formMapperFactory.RegisterAssembly(assembly);

        var formMapper = formMapperFactory.Build(containerBuilder);
        containerBuilder.RegisterSingletonInstance(typeof(IFormMapper), formMapper);

        // register all control bindings from the assemblies
        assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(t => typeof(IControlBinding).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false } &&
                        (t != typeof(DefaultBinding)))
            .ToList()
            .ForEach(t => containerBuilder.RegisterTransientType(typeof(IControlBinding), t));

        return containerBuilder;
    }
}
