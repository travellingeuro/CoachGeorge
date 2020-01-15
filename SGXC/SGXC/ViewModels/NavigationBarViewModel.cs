using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace SGXC.ViewModels
{

    public class NavigationBarViewModel : BindableBase
    {
        //public DelegateCommand NavigateHomeCommand { get; set; }

        public INavigationService NavigationService { get; }

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


        public NavigationBarViewModel(INavigationService navigationService)
        {
            //NavigateHomeCommand = new DelegateCommand(NavigateHomeMethod);
            this.NavigationService = navigationService;
            TeamName = AppSettings.TeamName;
            TeamLogo = AppSettings.TeamLogo;
        }

        //private void NavigateHomeMethod()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
