using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Models.Helper;
using SGXC.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace SGXC.ViewModels
{
    public class RunEventPageViewModel : BindableBase, INavigatedAware
    {
        INavigationService NavigationService { get; set; }
        ITimeServices TimeServices { get; set; }
        IEventServices EventServices { get; set; }
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand FinalizedCommnad { get; set; }
        public DelegateCommand<object> SplitTimeCommand { get; set; }
        public DelegateCommand NavigateToEventDetailsCommand { get; set; }


        private readonly DateTime zerotime = DateTime.MinValue;
        private DateTime clickedtime;

        private TimeSpan time = new TimeSpan(0, 0, 0);
        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

        private TimeSpan elapsedtime;
        public TimeSpan ElapsedTime

        {
            get { return elapsedtime; }
            set { SetProperty(ref elapsedtime, value); }
        }


        private ObservableCollection<EventWithRunners> runs;
        public ObservableCollection<EventWithRunners> Runs
        {
            get { return runs; }
            set { SetProperty(ref runs, value); }
        }


        private int total = 0;
        public int Total
        {
            get { return total; }
            set { SetProperty(ref total, value); }
        }

        private int runnersperleg = 0;
        public int RunnersPerLeg
        {
            get { return runnersperleg; }
            set { SetProperty(ref runnersperleg, value); }
        }

        private int splits = 0;
        public int Splits
        {
            get { return splits; }
            set { SetProperty(ref splits, value); }
        }

        private int legs;
        public int Legnumber
        {
            get { return legs; }
            set { SetProperty(ref legs, value); }
        }

        private bool isrunning = false;
        public bool IsRunning
        {
            get { return isrunning; }
            set { SetProperty(ref isrunning, value); }
        }

        private bool islegfinished;
        public bool IsLegFinished
        {
            get { return islegfinished; }
            set { SetProperty(ref islegfinished, value); }
        }

        private bool shownextlegpopup;
        public bool ShowNextLegPopUp
        {
            get { return shownextlegpopup; }
            set { SetProperty(ref shownextlegpopup, value); }
        }

        private bool showendpopup;
        public bool ShowEndPopup
        {
            get { return showendpopup; }
            set { SetProperty(ref showendpopup, value); }
        }

        private bool isresultvisible;
        public bool IsResultsVisible
        {
            get { return isresultvisible; }
            set { SetProperty(ref isresultvisible, value); }
        }

        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set { SetProperty(ref isbusy, value); }
        }



        public Event RunningEvent { get; private set; }

        public RunEventPageViewModel(INavigationService NavigationService, ITimeServices timeServices, IEventServices eventservices)
        {
            this.NavigationService = NavigationService;
            this.TimeServices = timeServices;
            this.EventServices = eventservices;
            StartCommand = new DelegateCommand(StartMethod, CanExecuteStart).ObservesProperty(() => IsRunning);
            FinalizedCommnad = new DelegateCommand(FinalizedMethod, CanExecuteStop).
                                                  ObservesProperty(() => IsRunning).ObservesProperty(() => Total);
            SplitTimeCommand = new DelegateCommand<object>(SplitTimeMethod, CanExecuteSplitTime).
                                                  ObservesProperty(() => Total).ObservesProperty(() => IsRunning);
            NavigateToEventDetailsCommand = new DelegateCommand(NavigateToEventDetailMethod);
            IsBusy = true;
        }


        private async void NavigateToEventDetailMethod()
        {
            //var events = await EventServices.GetEventById(RunningEvent.Id);
            var parameters = new NavigationParameters { { "event", RunningEvent } };
            await NavigationService.NavigateAsync("EventDetailsPage", parameters);
        }

        private async void GetTimes(int eventId)
        {
            Runs = new ObservableCollection<EventWithRunners>(await TimeServices.GetTimesByEventId(eventId));
            Total = Runs.Count();
            RunnersPerLeg = Runs.Select(n => n.Runner.Id).Distinct().Count();
            Legnumber = 1;
            IsBusy = false;
        }

        private void StartWatch()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(785), Callback);
        }

        private bool Callback()
        {

            if (Total == 0)
            {
                return false;
            }

            if (IsLegFinished == true)

            {
                IsLegFinished = false;
                return false;
            }


            var timesinceclicked = clickedtime - zerotime;
            var timenow = DateTime.Now - zerotime;
            Time = timenow - timesinceclicked;
            return IsRunning;
        }

        private void StartMethod()
        {
            clickedtime = DateTime.Now;
            IsRunning = true;
            StartWatch();
        }

        private bool CanExecuteStart()
        {
            return IsRunning != true;
        }

        private void StopMethod()
        {
            IsRunning = false;
            Time = new TimeSpan(0, 0, 0);
        }

        private void FinalizedMethod()
        {
            IsRunning = false;
            Total = 0;
            ShowResultsAlert();
            IsResultsVisible = true;
        }

        private bool CanExecuteStop()
        {
            return Total != 0 && IsRunning == true && IsLegFinished == false;
        }

        private void SplitTimeMethod(object obj)

        {
            var item = obj as EventWithRunners;
            ElapsedTime = Time;
            item.CanBeSplited = false;
            var time = item.RanTime;
            time.Times = ElapsedTime;
            TimeServices.UpdateTime(time);

            Splits++;
            Total--;

            if (!RunningEvent.IsRace)
            {           
                IsLegFinished = (Splits % RunnersPerLeg == 0) ? true : false;
                if (IsLegFinished && Total != 0)
                {
                    StopMethod();
                    Legnumber = Splits / RunnersPerLeg;
                    ShowNextLegAlert();
                }
            }

            if (Total == 0)
            {
                ShowResultsAlert();
                IsResultsVisible = true;
            }
        }

        private async void ShowResultsAlert()

        {
            ShowEndPopup = true;
            RunningEvent.IsRan = true;
            await EventServices.UpdateEvent(RunningEvent);

        }

        private bool CanExecuteSplitTime(object item)
        {
            return Total != 0 && IsRunning == true && IsLegFinished == false;
        }

        private void ShowNextLegAlert()
        {
            ShowNextLegPopUp = true;
            IsLegFinished = false;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)

        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            IsRunning = false;
            RunningEvent = (Event)parameters["Event"];
            GetTimes(RunningEvent.Id);
        }
    }
}
