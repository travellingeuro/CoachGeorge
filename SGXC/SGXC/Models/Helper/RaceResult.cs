using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGXC.Models.Helper
{
    public class RaceResult:BindableBase
    {
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }
    }
}
