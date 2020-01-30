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

        private double mintime;
        public double MinTime
        {
            get { return mintime; }
            set { SetProperty(ref mintime, value); }
        }

        private double maxtime;
        public double MaxTime
        {
            get { return maxtime; }
            set { SetProperty(ref maxtime, value); }
        }
    }
}



    
