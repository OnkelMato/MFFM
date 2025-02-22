using Autofac;
using Mffm.DependencyInjection.Autofac;
using Mffm.Samples.Core.Logging;
using Mffm.Samples.Core.Services;
using Mffm.Samples.Extensions.GeoComponent;
using Mffm.Samples.Ui.EditUser;
using Mffm.Samples.Ui.Main;

namespace Mffm.Samples.Autofac
{
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
            serviceCollection.ConfigureDemoAppServices();

            // here is all the registration logic for the MFFM services and framework
            // this includes the user interface which is formModels and forms
            serviceCollection.ConfigureMffm(
                typeof(MainFormModel).Assembly,
                // the second assembly overrides the editform and adds some more mapping functionality
                typeof(GeolocationControl).Assembly);

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
        }

        #endregion
    }
}