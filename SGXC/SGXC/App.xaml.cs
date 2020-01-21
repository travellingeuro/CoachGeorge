using Prism;
using Prism.Ioc;
using SGXC.Services;
using SGXC.ViewModels;
using SGXC.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SGXC
{
    public partial class App
    {

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTgzOTI4QDMxMzcyZTM0MmUzMElUT2xqNnFpcHdHZVM1OWU0WkI5Szh5ZS9ZZnd5SFVvZURGekZESTVnVXc9");

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");

        }

        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RunnerPage, RunnerPageViewModel>();
            containerRegistry.RegisterSingleton<ISQLite, SqlServices>();
            containerRegistry.Register<IEventServices, EventServices>();
            containerRegistry.Register<IRunnerServices, RunnerServices>();
            containerRegistry.Register<ITimeServices, TimeServices>();
            containerRegistry.Register<IPdfServices, PdfServices>();
            containerRegistry.RegisterForNavigation<AddRunnerPage, AddRunnerPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEventPage, AddEventPageViewModel>();
            containerRegistry.RegisterForNavigation<AddRunnerToEvent, AddRunnerToEventViewModel>();
            containerRegistry.RegisterForNavigation<AddTimesToEventPage, AddTimesToEventPageViewModel>();
            containerRegistry.RegisterForNavigation<RunEventPage, RunEventPageViewModel>();
            containerRegistry.RegisterForNavigation<EventsPage, EventsPageViewModel>();
            containerRegistry.RegisterForNavigation<SetLogoPage, SetLogoPageViewModel>();
            containerRegistry.RegisterForNavigation<EventDetailsPage, EventDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<RunnerStats, RunnerStatsViewModel>();
        }
    }
}
