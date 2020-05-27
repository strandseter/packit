using System;
using Microsoft.Extensions.DependencyInjection;
using Packit.App.Services;
using Packit.App.ViewModels;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Packit.App
{
    public sealed partial class App : Application //No time to fix the naming conflict. But I am fully aware that this is an issue.
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService2);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            ServiceProvider = RegisterServices();

            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        private static IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ItemsViewModel>();
            serviceCollection.AddTransient<DetailTripViewModel>();
            serviceCollection.AddTransient<BackpacksViewModel>();
            serviceCollection.AddTransient<NewItemViewModel>();
            serviceCollection.AddTransient<NewTripViewModel>();
            serviceCollection.AddTransient<NewBackpackViewModel>();
            serviceCollection.AddTransient<TripsMainViewModel>();
            serviceCollection.AddTransient<SelectBackpacksViewModel>();
            serviceCollection.AddTransient<SelectItemsViewModel>();
            serviceCollection.AddTransient<MainViewModel>();
            serviceCollection.AddTransient<LoginViewModel>();
            serviceCollection.AddTransient<RegisterUserViewModel>();
            serviceCollection.AddSingleton<IPopUpService, PopUpService>();
            return serviceCollection.BuildServiceProvider();
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private ActivationService CreateActivationService2()
        {
            return new ActivationService(this, typeof(Views.LoginPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
