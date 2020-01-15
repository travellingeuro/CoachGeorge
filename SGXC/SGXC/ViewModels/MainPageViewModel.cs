using Prism.Commands;
using Prism.Navigation;
using SGXC.Services;
using SQLite;
using System;

namespace SGXC.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand NavigateToRunnerPageCommand { get; set; }

        public DelegateCommand NavigateToAddEventPageCommand { get; set; }

        public DelegateCommand DeleteAllCommand { get; set; }

        public DelegateCommand NavigateToEventsPageCommand { get; set; }        
        
        public INavigationService navigationService { get; }

        public ISQLite dbservices { get; set; }

        public IEventServices EventServices { get; set; }
        
        private SQLiteAsyncConnection connection;
        public SQLiteAsyncConnection Connection
        {
            get { return connection; }
            set { SetProperty(ref connection, value); }
        }

        private int notranevents;
        public int NotRanEvents
        {
            get { return notranevents; }
            set { SetProperty(ref notranevents, value); }
        }

        private string teamlogo;
        public string TeamLogo
        {
            get { return teamlogo; }
            set { SetProperty(ref teamlogo, value); }
        }

        private string teamname;
        public string TeamName
        {
            get { return teamname; }
            set { SetProperty(ref teamname, value); }
        }

        public MainPageViewModel(INavigationService navigationService, ISQLite dbservices, IEventServices eventServices, IPdfServices pdfServices)
            : base(navigationService)
        {
            Title = "Main Menu";
            NavigateToRunnerPageCommand = new DelegateCommand(NavigateToRunnerPage);            
            DeleteAllCommand = new DelegateCommand(DeleteAllMethod);
            NavigateToEventsPageCommand = new DelegateCommand(NavigateToEventsPage);            
            this.navigationService = navigationService;
            this.dbservices = dbservices;            
            Connection = dbservices.GetConnection();
            dbservices.CreateTables(Connection);
            this.EventServices = eventServices;
            GetNotRanEvents();
        }        

        private async void GetNotRanEvents()
        {
            NotRanEvents = await EventServices.GetNotRanEvents();
            TeamLogo = AppSettings.TeamLogo;
            TeamName = AppSettings.TeamName;
        }

        private void DeleteAllMethod()
        {
            dbservices.DeleteAll(Connection);
        }
        private void NavigateToEventsPage()

        {
            navigationService.NavigateAsync("EventsPage");
        }

        private void NavigateToRunnerPage()
        {
            navigationService.NavigateAsync("RunnerPage");
        }
       
    }
}
