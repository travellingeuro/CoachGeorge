using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Models.Helper;
using SGXC.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGXC.ViewModels
{
    public class RunnerStatsViewModel : BindableBase, INavigationAware
    {
        INavigationService NavigationService { get; set; }

        IStatsService StatsService { get; set; }

        private Runner runner = new Runner();
        public Runner Runner
        {
            get { return runner; }
            set { SetProperty(ref runner, value); }
        }

        private List<PracticeData> practicedata;
        public List<PracticeData> PracticeData
        {
            get { return practicedata; }
            set { SetProperty(ref practicedata, value); }
        }

        public RunnerStatsViewModel(INavigationService navigationService, IStatsService statsService)
        {
            NavigationService = navigationService;
            StatsService = statsService;

        }
        public async void LoadPracticeData(int id)
        {
            PracticeData = await StatsService.GetPracticeData(id);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("runner"))

            {
                Runner = (Runner)parameters["runner"];
                LoadPracticeData(Runner.Id);                
            }
        }
    }
}
