using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Services;
using Syncfusion.XForms.PopupLayout;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SGXC.ViewModels
{
    public class EventsPageViewModel : BindableBase, INavigationAware
    {
        public INavigationService NavigationService { get; set; }
        public IEventServices EventServices { get; set; }

        public DelegateCommand NavigateToAddEventPageCommand { get; set; }
        public ICommand MenuItemSelectedCommand => new Command<Event>(OnSelectMenuItem);
        public ICommand OpenPopupCommand => new Command<Event>(OpenPopupMethod);
        public ICommand Delete => new Command<SfPopupLayout>(DeleteEventMethod);

        private ObservableCollection<Event> eventlist;
        public ObservableCollection<Event> EventList
        {
            get { return eventlist; }
            set { SetProperty(ref eventlist, value); }
        }

        private bool iseventempty;
        public bool IsEventEmpty
        {
            get { return iseventempty; }
            set { SetProperty(ref iseventempty, value); }
        }

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

        private Event eventtodelete;
        public Event EventToDelete
        {
            get { return eventtodelete; }
            set { SetProperty(ref eventtodelete, value); }
        }



        public EventsPageViewModel(INavigationService navigationService, EventServices eventservices)
        {
            this.NavigationService = navigationService;
            this.EventServices = eventservices;
            NavigateToAddEventPageCommand = new DelegateCommand(NavigateToAddEvent);
            IsBusy = true;

        }

        private void NavigateToAddEvent()
        {
            NavigationService.NavigateAsync("AddEventPage");
        }

        private void OnSelectMenuItem(Event obj)
        {
            var parameters = new NavigationParameters { { "event", obj } };
            if (obj.IsRan == false)
            {
                NavigationService.NavigateAsync("AddRunnerToEvent", parameters);
            }
            else
            {
                NavigationService.NavigateAsync("EventDetailsPage", parameters);
            }


        }

        private void OpenPopupMethod(Event obj)

        {
            CanShowPopup = true;
            EventToDelete = obj;
        }

        private async void DeleteEventMethod(SfPopupLayout parameter)

        {
            await EventServices.DeleteEvent(EventToDelete);
            GetEvents();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            GetEvents();
        }

        private async void GetEvents()
        {
            IsBusy = true;
            var events = await EventServices.GetAllEvents();
            EventList = new ObservableCollection<Event>(events);
            IsEventEmpty = EventList.Count == 0 ? true : false;
            IsBusy = false;
        }


    }
}
