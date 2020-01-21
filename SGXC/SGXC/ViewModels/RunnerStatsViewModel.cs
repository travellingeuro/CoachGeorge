using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGXC.ViewModels
{
    public class RunnerStatsViewModel : BindableBase, INavigationAware
    {
        private Runner runner = new Runner();
        public Runner Runner
        {
            get { return runner; }
            set { SetProperty(ref runner, value); }
        }

        public RunnerStatsViewModel()
        {

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
