using System.Collections;
using System.Windows.Forms.Design;
using Microsoft.Extensions.DependencyInjection;
using WinFormsMffmPrototype.Core.Logging;
using WinFormsMffmPrototype.Core.Services;
using WinFormsMffmPrototype.Exceptions;
using WinFormsMffmPrototype.MvvmFramework;
using WinFormsMffmPrototype.MvvmFramework.EventAggregators;
using WinFormsMffmPrototype.Ui.EditUser;
using WinFormsMffmPrototype.Ui.Main;
using WinFormsMffmPrototype.Ui.Protocol;

namespace WinFormsMffmPrototype
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Initialize Dependency Injection
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureMvvmFramework();
            serviceCollection.ConfigureUserInterface();
            serviceCollection.ConfigureServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Initialize Application
            var mainView = serviceProvider.GetService<MainForm>() ?? throw new MainViewNotFoundException();
            Application.Run(mainView);
        }

        #region service configurations

        private static void ConfigureMvvmFramework(this IServiceCollection services)
        {
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IMffmLogger, TraceLogger>();

            services.AddTransient<CloseFormCommand>();

        }
        private static void ConfigureUserInterface(this IServiceCollection services)
        {
            // Register services and view models
            services.AddTransient<MainFormModel>();
            services.AddTransient<EditFormModel>();
            services.AddTransient<ProtocolFormModel>();

            services.AddTransient<MainForm>();
            services.AddTransient<EditForm>();
            services.AddTransient<ProtocolForm>();
        }
        private static void ConfigureServices(this IServiceCollection services)
        {
            // additional services
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IGreetingRepository, GreetingRepository>();

            // this is the mediatr registration
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        }

        #endregion
    }

    public class UiServices : IUIService
    {
        public bool CanShowComponentEditor(object component)
        {
            return false;
        }

        public IWin32Window GetDialogOwnerWindow()
        {
            return null;
        }

        public void SetUIDirty()
        {
        }

        public bool ShowComponentEditor(object component, IWin32Window parent)
        {
            return false;
        }

        public DialogResult ShowDialog(Form form)
        {
            return DialogResult.None;
        }

        public void ShowError(string message)
        {
        }

        public void ShowError(Exception ex)
        {
        }

        public void ShowError(Exception ex, string message)
        {
        }

        public void ShowMessage(string message)
        {
        }

        public void ShowMessage(string message, string caption)
        {
        }

        public DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons)
        {
            return DialogResult.None;
        }

        public bool ShowToolWindow(Guid toolWindow)
        {
            return false;
        }

        public IDictionary Styles { get; }
    }
}