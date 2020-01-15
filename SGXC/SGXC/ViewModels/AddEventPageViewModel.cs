using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Services;
using System;

namespace SGXC.ViewModels
{
    public class AddEventPageViewModel : BindableBase
    {
        public INavigationService NavigationService { get; set; }

        public IEventServices EventServices { get; set; }

        public DelegateCommand AddRunnersToEventCommand { get; set; }

        private Event newevent = new Event() { RaceDate = DateTime.Today };
        public Event NewEvent
        {
            get { return newevent; }
            set { SetProperty(ref newevent, value); }
        }

        public AddEventPageViewModel(INavigationService navigationService, IEventServices eventServices)
        {
            this.NavigationService = navigationService;
            this.EventServices = eventServices;
            AddRunnersToEventCommand = new DelegateCommand(SaveEvent, EnableNextButton).ObservesProperty(() => NewEvent.EventName);
        }

        private bool EnableNextButton()
        {
            return !string.IsNullOrWhiteSpace(NewEvent.EventName);
        }

        private void SaveEvent()
        {
            EventServices.CreateEvent(NewEvent);
            var navigationparams = new NavigationParameters
            {
                { "event", NewEvent }
            };
            NavigationService.NavigateAsync("AddRunnerToEvent", navigationparams);
        }



    }
}
