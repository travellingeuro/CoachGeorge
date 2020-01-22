using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGXC.Models.Helper
{
    public class RaceResult:BindableBase
    {
        private Event eventid;
        public Event EventId
        {
            get { return eventid; }
            set { SetProperty(ref eventid, value); }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }
    }
}
