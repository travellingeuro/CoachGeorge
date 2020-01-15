using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Services;
using SQLite;

namespace SGXC.ViewModels
{
    public class AddRunnerPageViewModel : BindableBase, INavigationAware
    {
        public INavigationService NavigationService { get; set; }
        private ISQLite SqlServices { get; set; }

        private IRunnerServices RunnerServices { get; set; }

        public DelegateCommand SaveRunnerCommand { get; set; }


        private SQLiteAsyncConnection connection;
        public SQLiteAsyncConnection Connection
        {
            get { return connection; }
            set { SetProperty(ref connection, value); }
        }

        private Runner runner = new Runner();
        public Runner Runner
        {
            get { return runner; }
            set { SetProperty(ref runner, value); }
        }


        public AddRunnerPageViewModel(ISQLite sQLite, IRunnerServices RunnerServices, INavigationService navigationService)
        {
            SqlServices = sQLite;
            NavigationService = navigationService;
            this.RunnerServices = RunnerServices;
            Connection = SqlServices.GetConnection();
            SaveRunnerCommand = new DelegateCommand(SaveRunnerMethod);
        }



        private async void SaveRunnerMethod()

        {
            await RunnerServices.SaveRunner(Runner);
            await NavigationService.NavigateAsync("../RunnerPage");

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("runner"))

            {
                Runner = (Runner)parameters["runner"];

            }


        }
    }
}
