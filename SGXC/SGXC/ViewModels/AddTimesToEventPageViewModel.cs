
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SGXC.ViewModels
{
    public class AddTimesToEventPageViewModel : BindableBase, INavigationAware
    {

        INavigationService NavigationService { get; set; }
        ITimeServices TimeServices { get; set; }
        IRunnerServices RunnerServices { get; set; }
        public DelegateCommand AddDistanceCommand { get; set; }
        public DelegateCommand RunEventCommand { get; set; }
        public DelegateCommand SaveEventCommand { get; set; }
        public DelegateCommand<ListofRaces> RemoveDistanceCommand { get; set; }

        private ListofRaces listofraces = new ListofRaces();
        public ListofRaces ListofRaces
        {
            get { return listofraces; }
            set { SetProperty(ref listofraces, value); }
        }

        private ObservableCollection<ListofRaces> showlist = new ObservableCollection<ListofRaces>();
        public ObservableCollection<ListofRaces> ShowList
        {
            get { return showlist; }
            set { SetProperty(ref showlist, value); }
        }

        private Event newevent;
        public Event NewEvent
        {
            get { return newevent; }
            set { SetProperty(ref newevent, value); }
        }

        private List<Runner> selectedrunners;
        public List<Runner> SelectedRunners
        {
            get { return selectedrunners; }
            set { SetProperty(ref selectedrunners, value); }
        }


        private int showrunnerscount;
        public int ShowRunnersCount
        {
            get { return showrunnerscount; }
            set { SetProperty(ref showrunnerscount, value); }
        }

        private ObservableCollection<Time> storedtimes;
        public ObservableCollection<Time> StoredTimes
        {
            get { return storedtimes; }
            set { SetProperty(ref storedtimes, value); }
        }

        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set { SetProperty(ref isbusy, value); }
        }

        private bool shownumeric;
        public bool ShowNumeric
        {
            get { return shownumeric; }
            set { SetProperty(ref shownumeric, value); }
        }

        private bool showpopup;
        public bool ShowPopUp
        {
            get { return showpopup; }
            set { SetProperty(ref showpopup, value); }
        }



        public AddTimesToEventPageViewModel(INavigationService NavigationService, ITimeServices TimeServices, IRunnerServices runnerServices)
        {
            this.NavigationService = NavigationService;
            this.TimeServices = TimeServices;
            this.RunnerServices = runnerServices;
            AddDistanceCommand = new DelegateCommand(AddDistanceMethod, CanExecuteAddDistance).ObservesProperty(() => ListofRaces.DistanceName);
            RunEventCommand = new DelegateCommand(RunEventMethod, CanExecuteRunEvent).ObservesProperty(() => ShowList.Count);
            RemoveDistanceCommand = new DelegateCommand<ListofRaces>(RemoveDistanceMethod);
            SaveEventCommand = new DelegateCommand(SaveEventMethod, CanExecuteSaveEvent).ObservesProperty(() => ShowList.Count);
            
        }



        private async void RunEventMethod()
        {
            var parameters = new NavigationParameters { { "Event", NewEvent }, { "Runners", SelectedRunners } };
            await NavigationService.NavigateAsync("/RunEventPage", parameters);
        }
        private bool CanExecuteRunEvent()
        {
            return ShowList.Count != 0;
        }
        private void AddDistanceMethod()

        {
            SetTimes(listofraces.Reps); //Pensar en fijar listofraces.Reps en 1 para NewEvent.Israce=true

        }
        private bool CanExecuteAddDistance()

        {
            return !string.IsNullOrWhiteSpace(ListofRaces.DistanceName);
        }

        private async void SetTimes(int obj)

        {
            var time = new Time();

            for (int j = 0; j < obj; j++)
            {
                for (int i = 0; i < SelectedRunners.Count; i++)
                {
                    time.Times = new TimeSpan(0, 0, 0);
                    time.Distance = listofraces.DistanceName;
                    time.Repetitions = j + 1;
                    await TimeServices.AddTime(time);
                    time.Runner = SelectedRunners[i];
                    time.Event = NewEvent;
                    await TimeServices.AddEventToTime(time);
                }
            }
            ShowList.Add(new ListofRaces() { DistanceName = listofraces.DistanceName, Reps = listofraces.Reps, EventId = NewEvent.Id });
            listofraces.Reps = 1;
            listofraces.DistanceName = null;
        }

        private async void ModifyTimes(int id)

        {
            var distances = StoredTimes.Select(e => e.Distance).Distinct();
            foreach (var distance in distances)

            {
                var reps = StoredTimes.Where(e => e.Distance == distance).Select(r => r.Repetitions).Max();
                ShowList.Add(new ListofRaces() { DistanceName = distance, Reps = reps, EventId = id });
            }

            //borrar null EventId Times
            await TimeServices.DeleteNullEventIdTimes();

            //correr SetTimes por cada linea de Showlist.
            foreach (var race in ShowList)

            {
                ReSetTimes(race, race.Reps);
            }

            IsBusy = false;

        }

        private async void ReSetTimes(ListofRaces race, int reps)
        {
            var time = new Time();

            for (int j = 0; j < reps; j++)
            {
                for (int i = 0; i < SelectedRunners.Count; i++)
                {
                    time.Times = new TimeSpan(0, 0, 0);
                    time.Distance = race.DistanceName;
                    time.Repetitions = j + 1;
                    await TimeServices.AddTime(time);
                    time.Runner = SelectedRunners[i];
                    time.Event = NewEvent;
                    await TimeServices.AddEventToTime(time);
                }
            }

        }

        private async void RemoveDistanceMethod(ListofRaces item)
        {
            var distance = item.DistanceName;
            var @event = item.EventId;
            await TimeServices.DeleteTime(distance, @event);
            ShowList.Remove(item);
        }

        private async void SaveEventMethod()
        {
            await NavigationService.NavigateAsync("/MainPage");
        }

        private bool CanExecuteSaveEvent()
        {
            return ShowList.Count != 0;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            var navmode = parameters.GetNavigationMode();
            if (navmode == NavigationMode.Back)
            {
                parameters.Add("event", NewEvent);
            }

        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;

            if (parameters["Event"] != null)
            {
                NewEvent = (Event)parameters["Event"];
                SelectedRunners = (List<Runner>)parameters["SelectedRunners"];              
                StoredTimes = new ObservableCollection<Time>((List<Time>)parameters["StoredTimes"]);                                              
                ModifyTimes(NewEvent.Id);
                IsBusy = false;
                ShowNumeric = !NewEvent.IsRace;
                ShowPopUp = NewEvent.IsRace;
            }
            ShowRunnersCount = SelectedRunners.Count();
        }
    }
}
