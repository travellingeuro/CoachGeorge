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

        private double time;
        public double Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

        private string distance;
        public string Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }
    }
}
