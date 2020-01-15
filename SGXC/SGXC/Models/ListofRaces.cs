using Prism.Mvvm;
using System.Collections.Generic;

namespace SGXC.Models
{
    public class ListofRaces : BindableBase
    {
        private int reps;
        public int Reps
        {
            get { return reps; }
            set { SetProperty(ref reps, value); }
        }

        private string distancename;
        public string DistanceName
        {
            get { return distancename; }
            set { SetProperty(ref distancename, value); }
        }

        private int eventid;
        public int EventId
        {
            get { return eventid; }
            set { SetProperty(ref eventid, value); }
        }

        private List<string> racelist = new List<string> { "100m", "200m", "300m", "400m (1/4 mile)", "600m" ,"800m (1/2 mile)",
                                                        "1K","1.2K","1.5K","1 mile","2K", "2 mile"};
        public List<string> RaceList
        {
            get { return racelist; }
            set { SetProperty(ref racelist, value); }
        }



    }
}
