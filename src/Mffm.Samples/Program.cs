using Mffm.DependencyInjection.Microsoft.Extensions;
using Mffm.Samples.Core.Logging;
using Mffm.Samples.Core.Services;
using Mffm.Samples.Extensions.GeoComponent;
using Mffm.Samples.Ui.EditUser;
using Mffm.Samples.Ui.Main;
using Microsoft.Extensions.DependencyInjection;

namespace Mffm.Samples;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Initialize Dependency Injection
        var serviceCollection = new ServiceCollection();
        serviceCollection.ConfigureDemoAppServices();

        // here is all the registration logic for the MFFM services and framework
        // this includes the user interface which is formModels and forms
        serviceCollection.ConfigureMffm(
            typeof(Program).Assembly,
            // the second assembly overrides the editform and adds some more mapping functionality
            typeof(GeolocationControl).Assembly);

        // create the service provider aka container
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Run the application directly on service provider
        serviceProvider.Run<MainFormModel>();
    }

    #region Microsoft DI service configurations

    private static void ConfigureDemoAppServices(this IServiceCollection services)
    {
        // additional services
        services.AddSingleton<IBmLogger, TraceLogger>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IGreetingRepository, GreetingRepository>();

        services.AddTransient<SavePersonCommand>();
    }

    #endregion
}
