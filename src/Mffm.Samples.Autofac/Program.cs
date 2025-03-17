using Autofac;
using Mffm.Contracts;
using Mffm.DependencyInjection.Autofac;
using Mffm.Samples.Core.Logging;
using Mffm.Samples.Core.Services;
using Mffm.Samples.Extensions.GeoComponent;
using Mffm.Samples.Ui.EditUser;
using Mffm.Samples.Ui.Main;

namespace Mffm.Samples.Autofac;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static int Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Initialize Dependency Injection
        var serviceCollection = new ContainerBuilder();

        // here is all the registration logic for the MFFM services and framework
        // this includes the user interface which is formModels and forms
        serviceCollection.ConfigureMffm(
            typeof(MainFormModel).Assembly);//,
                                            // the second assembly overrides the editform and adds some more mapping functionality
                                            //typeof(GeolocationControl).Assembly);
        serviceCollection.ConfigureDemoAppServices();

        // create the service provider aka container
        var serviceProvider = serviceCollection.Build();

        // Run the application directly on service provider
        return serviceProvider.Run<MainFormModel>();
    }

    #region Microsoft DI service configurations

    private static void ConfigureDemoAppServices(this ContainerBuilder services)
    {
        // additional services
        services.RegisterType<TraceLogger>().As<IBmLogger>().SingleInstance();
        services.RegisterType<DateTimeProvider>().As<IDateTimeProvider>().SingleInstance();
        services.RegisterType<GreetingRepository>().As<IGreetingRepository>().SingleInstance();

        services.RegisterType<SavePersonCommand>().AsSelf();

        services.RegisterType<I18nBinding>().AsImplementedInterfaces();
        services.RegisterType<TranslationService>().AsImplementedInterfaces();

        services.RegisterType<MenuFormAdapter>().AsSelf();
    }

    #endregion
}

public class I18nBinding : IControlBinding
{
    private readonly ITranslationService _translationService;

    public I18nBinding(ITranslationService translationService)
    {
        _translationService = translationService ?? throw new ArgumentNullException(nameof(translationService));
    }

    public bool Bind(Control control, IFormModel formModel)
    {
        if (control is Label or Button or GroupBox)
        {
            var formName = control.Parent.Name;
            var controlName = control.Name;
            var language = "de";
            var translation = _translationService.Translate($"{formName}.{controlName}", language);
            control.Text = translation;
        };

        return false;
    }
}

public class TranslationService : ITranslationService
{
    public string Translate(string control, string language)
    {
        return $"{control} [{language}]";
    }
}

public interface ITranslationService
{
    string Translate(string control, string language);
}
