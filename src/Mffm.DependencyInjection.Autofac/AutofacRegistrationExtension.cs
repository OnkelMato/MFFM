using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Mffm.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Mffm.DependencyInjection.Autofac
{
    /// <summary>
    /// Abstraction for dependency injection framework and abstracts the MFFM related parts.
    /// Reduces the main function to a minimum.
    /// </summary>
    public static class AutofacRegistrationExtension
    {
        /// <summary>
        /// The method and naming convention is used to register all required containerBuilder for MFFM.
        /// </summary>
        public static void ConfigureMffm(this ContainerBuilder containerBuilder, params Assembly[] assemblies)
        {
            // use adapter pattern so we can use different DI frameworks
            var adapter = new DiRegistrationAdapter(containerBuilder);

            // uses the adapter to register the containerBuilder. So the implementations can be private
            adapter.RegisterServices();
            adapter.RegisterExtensions(assemblies);

            // this makes IServiceProvider and IServiceScopeFactory available in the container
            containerBuilder.Populate(new ServiceCollection());
        }

        /// <summary>
        /// Create a method to run the application with the given form model. This hides the information about resolving a window manager.
        /// </summary>
        /// <typeparam name="TFormModel">View model to run the application. Former the "MainForm"</typeparam>
        /// <param name="provider">Service provider to resolve the instance</param>
        /// <returns></returns>
        /// <exception cref="ServiceNotFoundException"></exception>
        public static int Run<TFormModel>(this IContainer provider)
            where TFormModel : class, IFormModel
        {
            var windowManager = provider.Resolve<IWindowManager>() ?? throw new ServiceNotFoundException("cannot find window manager for MFFM pattern");
            // let's exit with code 0
            return (int)windowManager.Run<TFormModel>() - 1;
        }
    }
}