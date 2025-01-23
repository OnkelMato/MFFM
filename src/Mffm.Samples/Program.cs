using Mffm.Contracts;
using Mffm.Microsoft.Extensions.DependencyInjection;
using Mffm.Samples.Core.Logging;
using Mffm.Samples.Core.Services;
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
        // todo create ADRs using MADR framework from github

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Initialize Dependency Injection
        var serviceCollection = new ServiceCollection();
        serviceCollection.ConfigureDemoAppServices();

        // here is all the registration logic for the MFFM services and framework
        // this includes the user interface which is formModels and forms
        serviceCollection.ConfigureMffm(typeof(Program).Assembly);

        // create the service provider aka container
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get window manager and run application
        var windowManager = serviceProvider.GetService<IWindowManager>() ??
                            throw new ServiceNotFoundException("cannot find window manager for MFFM pattern");
        windowManager.Run<MainFormModel>();
    }

    #region service configurations

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