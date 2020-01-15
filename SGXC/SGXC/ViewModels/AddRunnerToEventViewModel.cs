using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SGXC.ViewModels
{
    public class AddRunnerToEventViewModel : BindableBase, INavigationAware
    {
        INavigationService NavigationService { get; set; }
        IEventServices EventServices { get; set; }
        IRunnerServices RunnerServices { get; set; }
        ITimeServices TimeServices { get; set; }
        public DelegateCommand SaveEventCommand { get; set; }
        public ICommand MenuItemSelectedCommand => new Command<Runner>(OnSelectMenuItem);
        public DelegateCommand<Runner> RemoveRunnerCommand { get; set; }

        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set { SetProperty(ref isbusy, value); }
        }


        private Event @event;
        public Event Event
        {
            get { return @event; }
            set { SetProperty(ref @event, value); }
        }

        private ObservableCollection<Runner> runners;
        public ObservableCollection<Runner> Runners
        {
            get { return runners; }
            set { SetProperty(ref runners, value); }
        }
        
        private List<Time> timesbyevent ;
        public List<Time> TimesByEvent

        {
            get { return timesbyevent; }
            set { SetProperty(ref timesbyevent, value); }
        }

        private ObservableCollection<Runner> selectedrunners;
        public ObservableCollection<Runner> SelectedRunners
        {
            get { return selectedrunners; }
            set { SetProperty(ref selectedrunners, value); }
        }

        private int selectedrunnerscount = 0;
        public int SelectedRunnersCount
        {
            get { return selectedrunnerscount; }
            set { SetProperty(ref selectedrunnerscount, value); }
        }

        public AddRunnerToEventViewModel(INavigationService NavigationService, IEventServices eventServices, IRunnerServices runnerServices, ITimeServices timeServices)

        {
            this.NavigationService = NavigationService;
            this.EventServices = eventServices;
            this.RunnerServices = runnerServices;
            this.TimeServices = timeServices;    
            SaveEventCommand = new DelegateCommand(SaveEventMethod);
            RemoveRunnerCommand = new DelegateCommand<Runner>(RemoveRunnerMethod);
            IsBusy = true;
        }

        private void RemoveRunnerMethod(Runner obj)

        {
            SelectedRunners.Remove(obj);
            Runners.Add(obj);
            SelectedRunnersCount--;
        }

        private void OnSelectMenuItem(Runner obj)

        {
            SelectedRunners.Add(obj);
            Runners.Remove(obj);
            SelectedRunnersCount++;
        }

        private async void SaveEventMethod()

        {
                var sr = new List<Runner>(SelectedRunners);
                await EventServices.UpdateRunnersToEvent(Event, sr);
                NavigateNextPage();

        }

        private async void NavigateNextPage()

        {
            var sr = new List<Runner>(SelectedRunners);

            var parameters = new NavigationParameters
            {
                { "Event", Event },
                { "SelectedRunners", sr }
            };

            if (TimesByEvent != null)
            {
                parameters.Add("StoredTimes", TimesByEvent);
            }
            await NavigationService.NavigateAsync("AddTimesToEventPage", parameters);
        }
      
        private async Task<ObservableCollection<Runner>> GetAllRunners()
        {
               IsBusy = true;            
               Runners = new ObservableCollection<Runner>(await RunnerServices.GetAllRunners()); 
               IsBusy = false;
               return Runners;
        }

        private async Task<List<Runner>> GetRunnersbyEvent()

        {
            var returned = await RunnerServices.GetRunnersByEventID(Event.Id);
            SelectedRunners =new ObservableCollection<Runner>( returned);
            SelectedRunnersCount = SelectedRunners.Count;
            foreach (var runner in SelectedRunners)
                    {                       
                        Runners.Remove(Runners.SingleOrDefault(i => i.Id == runner.Id));
                    }
                
            IsBusy = false;
            return returned; 
        }

        private async void GetTimesbyRunner()

        {                   
                TimesByEvent = new List<Time>( await TimeServices.GetListOfTimesbyEventId(Event.Id));
                IsBusy = false; 
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            
            IsBusy = true;

            if (parameters != null)
            {
                Event = (Event)parameters["event"];
            }
            await GetAllRunners();
            await GetRunnersbyEvent();
            GetTimesbyRunner();
        }
 
    }
}
