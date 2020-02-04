using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SGXC.Models;
using SGXC.Models.Helper;
using SGXC.Services;
using System;
using System.Collections.ObjectModel;

namespace SGXC.ViewModels
{
    public class EventDetailsPageViewModel : BindableBase, INavigationAware
    {
        //Services
        ITimeServices TimeServices { get; set; }
        IEventServices EventServices { get; set; }
        IPdfServices PdfServices { get; set; }

        //Command

        public DelegateCommand CreatePdfCommand { get; set; }

        //properties

        private bool isbusy;
        public bool IsBusy
        {
            get { return isbusy; }
            set { SetProperty(ref isbusy, value); }
        }
        private Event runevent;
        public Event RunEvent
        {
            get { return runevent; }
            set { SetProperty(ref runevent, value); }
        }

        private ObservableCollection<EventWithRunners> runs;
        public ObservableCollection<EventWithRunners> Runs
        {
            get { return runs; }
            set { SetProperty(ref runs, value); }
        }

        //constructor
        public EventDetailsPageViewModel(ITimeServices timeServices, IEventServices eventServices, IPdfServices pdfServices)
        {
            this.TimeServices = timeServices;
            this.EventServices = eventServices;
            this.PdfServices = pdfServices;
            CreatePdfCommand = new DelegateCommand(CreatePdfMethod);
            IsBusy = true;

        }

        private void CreatePdfMethod()

        {
            if (RunEvent!=null)
            {
                PdfServices.CreatePDF(RunEvent.Id);
            }
	    }
        

        //Methods
        private async void GetTimes(int eventId)

        {
            Runs = new ObservableCollection<EventWithRunners>(await TimeServices.GetTimesByEventId(eventId));
            IsBusy = false;
        }


        public void OnNavigatedFrom(INavigationParameters parameters)

        {
            var navmode = parameters.GetNavigationMode();
            if (navmode == NavigationMode.Back)
            {
                parameters.Add("Event", RunEvent);
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters)

        {
            IsBusy = true;
            RunEvent = (Event)parameters["event"];
            GetTimes(RunEvent.Id);
        }


    }
}
