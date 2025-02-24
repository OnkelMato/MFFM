using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            services.RegisterType<MenuStripFavouriteManager>().AsSelf();
        }

        public static TService GetService<TService>()
        {
            return _serviceProvider.Resolve<TService>();
        }
    }
}