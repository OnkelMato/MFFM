using Autofac;
using System;
using System.Windows.Forms;
using LinkManager48.FormModels;
using LinkManager48.FormModels.Commands;
using LinkManager48.FormModels.FormAdapters;
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

            // here is all the registration logic for the MFFM services and framework
            // this includes the user interface which is formModels and forms
            serviceCollection.ConfigureMffm(typeof(MainFormModel).Assembly);
            serviceCollection.ConfigureAppServices();

            // create the service provider aka container
            _serviceProvider = serviceCollection.Build();

            // Run the application directly on service provider
            return _serviceProvider.Run<MainFormModel>();
        }

        private static void ConfigureAppServices(this ContainerBuilder services)
        {
            // register control models as this is not part of the MFFM framework, yet
            services.RegisterType<LinkDetailControlModel>().AsSelf();

            // bunch of commands
            services.RegisterType<CreateCategoryCommand>().AsSelf();

            // adapter to the rest of the system
            services.RegisterType<MainFormMenuLinkManager>().AsSelf();
            services.RegisterType<LinkDragAndDropManager>().AsSelf();

            // additional services
            services.RegisterType<DefaultHttpClient>().As<IHttpClient>();
            services.RegisterType<JsonLinkRepository>().As<ILinkRepository>().SingleInstance();
        }

        ///// <summary>
        ///// Get a service from the service provider.
        ///// This is a helper method to avoid direct access to the container.
        ///// </summary>
        ///// <typeparam name="TService"></typeparam>
        ///// <returns></returns>
        //public static TService GetService<TService>()
        //    where TService : class
        //{
        //    return _serviceProvider?.Resolve<TService>();
        //}
    }
}