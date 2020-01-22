using Prism.Mvvm;
using System;

namespace SGXC.Models.Helper
{
    public class PracticeResult : BindableBase
    {
        private DateTime  date;
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



    
