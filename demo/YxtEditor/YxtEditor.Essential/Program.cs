using Mffm.DependencyInjection.Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using YxtEditor.Essential.Commands;
using YxtEditor.Essential.FormModels;
using YxtEditor.Essential.Models;
using YxtEditor.Essential.Properties;

namespace YxtEditor.Essential
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Initialize Dependency Injection
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureAppServices();
            serviceCollection.ConfigureMffm(typeof(Program).Assembly);
            serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Resources>() );

            // create the service provider aka container
            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.Run<MainFormModel>();
        }

        public static ServiceCollection ConfigureAppServices(this ServiceCollection services)
        {
            services.AddTransient<IFileTypeSupport, YamlFileTypeSupport>();
            services.AddTransient<IFileTypeSupport, TxtFileTypeSupport>();

            services.AddTransient<OpenDocumentCommand>();
            services.AddTransient<SaveDocumentCommand>();
            services.AddTransient<SaveAsDocumentCommand>();
            services.AddTransient<NewDocumentCommand>();

            return services;
        }
    }
}