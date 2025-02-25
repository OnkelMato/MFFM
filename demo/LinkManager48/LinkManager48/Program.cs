using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkManager48.FormModels;
using LinkManager48.Models;
using Mffm.DependencyInjection.Autofac;

namespace LinkManager48
{
    internal static class Program
    {
        private static IContainer _serviceProvider;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceCollection = new ContainerBuilder();
            serviceCollection.ConfigureAppServices();

            // here is all the registration logic for the MFFM services and framework
            // this includes the user interface which is formModels and forms
            serviceCollection.ConfigureMffm(typeof(MainFormModel).Assembly);

            // create the service provider aka container
            _serviceProvider = serviceCollection.Build();

            // Run the application directly on service provider
            return _serviceProvider.Run<MainFormModel>();
        }


        private static void ConfigureAppServices(this ContainerBuilder services)
        {
            // additional services
            services.RegisterType<MainFormMenuLinkManager>().AsSelf();
            services.RegisterType<LinkFactory>().As<ILinkFactory>();
            services.RegisterType<DefaultHttpClient>().As<IHttpClient>();
        }

        /// <summary>
        /// Get a service from the service provider. This is a helper method to avoid direct access to the container.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
            where TService : class
        {
            return _serviceProvider?.Resolve<TService>();
        }
    }
}