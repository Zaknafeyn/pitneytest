using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using PitneyTest.DataAccess.API;
using PitneyTest.Tablet.Services;
using PitneyTest.Tablet.View;

namespace PitneyTest.Tablet
{
    sealed partial class App
    {
        private static readonly IUnityContainer _container = new UnityContainer();

        public App()
        {
            InitializeComponent();

            ConfigureContainer();
            ConfigureViewModelLocator();

            UnhandledException += CurrentOnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedException;
        }

        public static IUnityContainer Container
        {
            get { return _container; }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            NavigationService.Navigate(typeof (LoginView));
            Window.Current.Activate();
        }

        private static void ConfigureContainer()
        {
            Container.RegisterType<DataRetrieval>(new ContainerControlledLifetimeManager());
            Container.RegisterType<AuthContext>(new ContainerControlledLifetimeManager());
        }

        private static void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}",
                    viewName.Replace(".View.", ".ViewModel."), viewAssemblyName);
                return Type.GetType(viewModelName);
            });

            ViewModelLocationProvider.SetDefaultViewModelFactory(type => _container.Resolve(type));
        }

        private void CurrentOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            HandleException(e.Exception);
        }

        private void OnTaskSchedulerUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            HandleException(e.Exception);
        }

        public static async void HandleException(Exception exception)
        {
            var dialog = new MessageDialog("Unhandled Exception occured.", "Unhandled Exception");
            await dialog.ShowAsync();
        }
    }
}