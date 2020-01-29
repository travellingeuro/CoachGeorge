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


        private List<RaceData> racetest=new List<RaceData>();
        public List<RaceData> RaceTest
        {
            get { return racetest; }
            set { SetProperty(ref racetest, value); }
        }

        public RunnerStatsViewModel(INavigationService navigationService, IStatsService statsService)
        {
            NavigationService = navigationService;
            StatsService = statsService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public async void LoadData(int id)
        {
           PracticeData= await StatsService.GetPracticeData(id);           
           RaceTest = await StatsService.GetRaceData(id);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("runner"))

            {
                Runner = (Runner)parameters["runner"];
                LoadData(Runner.Id);
            }
        }
    }
}
