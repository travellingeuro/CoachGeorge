using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Services;
using SQLite;
using Syncfusion.XForms.PopupLayout;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SGXC.ViewModels
{
    public class RunnerPageViewModel : BindableBase, INavigationAware
    {

        public INavigationService NavigationService { get; set; }
        public DelegateCommand NavigateToAddRunnerPageCommand { get; set; }
        public ICommand MenuItemSelectedCommand => new Command<Runner>(OnSelectMenuItem);
        public ICommand OpenPopupCommand => new Command<Runner>(OpenPopupMethod);
        public DelegateCommand<Runner> NavigateToStatsCommand { get; set; }
        public ICommand Delete => new Command<SfPopupLayout>(DeleteEventMethod);



        private ISQLite SqlServices { get; set; }

        private IRunnerServices RunnerServices { get; set; }

        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set { SetProperty(ref isbusy, value); }
        }

        private bool canshowpopup;
        public bool CanShowPopup
        {
            get { return canshowpopup; }
            set { SetProperty(ref canshowpopup, value); }
        }

        private Runner runnertodelete;
        public Runner RunnerToDelete
        {
            get { return runnertodelete; }
            set { SetProperty(ref runnertodelete, value); }
        }

        private List<Runner> runners;
        public List<Runner> Runners
        {
            get { return runners; }
            set { SetProperty(ref runners, value); }
        }

        private bool isrunnerempty;
        public bool IsRunnerEmpty
        {
            get { return isrunnerempty; }
            set { SetProperty(ref isrunnerempty, value); }
        }

        private SQLiteAsyncConnection connection;
        public SQLiteAsyncConnection Connection
        {
            get { return connection; }
            set { SetProperty(ref connection, value); }
        }

        public RunnerPageViewModel(ISQLite dbservices, IRunnerServices runnerServices, INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigateToAddRunnerPageCommand = new DelegateCommand(NavigateToAddRunnerPage);
            NavigateToStatsCommand = new DelegateCommand<Runner>(NavigateToStatsMethod);
            SqlServices = dbservices;
            RunnerServices = runnerServices;
            Connection = SqlServices.GetConnection();
            IsBusy = true;
        }


        private void TableCreator(SQLiteAsyncConnection connection)
        {
            SqlServices.CreateTables(connection);
        }

        public async void GetRunners()
        {
            IsBusy = true;
            Runners = await RunnerServices.GetAllRunners();
            IsBusy = false;
            IsRunnerEmpty = Runners.Count == 0 ? true : false;

        }

        private async void NavigateToAddRunnerPage()
        {
            await NavigationService.NavigateAsync("AddRunnerPage");
        }

        private void OnSelectMenuItem(Runner obj)

        {
            var parameters = new NavigationParameters { { "runner", obj } };
            NavigationService.NavigateAsync("AddRunnerPage", parameters, useModalNavigation: true);

        }

        private void OpenPopupMethod(Runner obj)

        {
            CanShowPopup = true;
            RunnerToDelete = obj;
        }

        private void NavigateToStatsMethod(Runner obj)
        {
            var parameters = new NavigationParameters { { "runner", obj } };
            NavigationService.NavigateAsync("RunnerStats",parameters);
        }

        private async void DeleteEventMethod(SfPopupLayout obj)

        {
            await RunnerServices.DeleteRunner(RunnerToDelete);
            GetRunners();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)

        {
            TableCreator(Connection);
            GetRunners();
        }
    }
}
