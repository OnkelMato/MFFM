using System.Reflection;
using Mffm.Commands;
using Mffm.Contracts;
using Mffm.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Mffm.Microsoft.Extensions.DependencyInjection;

public static class MicrosoftDependencyInjectionExtension
{
    private static readonly DefaultFormLookup FormLookup = new();

    // todo allow multiple assemblies to be registered
    public static void ConfigureMffm(this IServiceCollection services, Assembly? assembly = null)
    {
        // todo make a better way to register the forms and services. add a base class in Mffm and inject lamda for singleton and transient registration.
        if (assembly != null)
        {
            FormLookup.RegisterAssembly(assembly);
            FormLookup.GetForms().ToList().ForEach(t => services.AddTransient(t));
            FormLookup.GetFormModels().ToList().ForEach(t => services.AddTransient(t));
        }

        services.AddSingleton<IWindowManager, WindowManager>();
        services.AddSingleton<IBindingManager, BindingManager>();
        services.AddSingleton<IEventAggregator, EventAggregator>();

        // we add the static instance here because we want to use the same instance in all places
        services.AddSingleton<IFormLookup>(FormLookup);

        // todo document: no service provider required as it is already implementing the correct "System.Component" interface by default

        services.AddTransient<CloseFormCommand>();
    }
}