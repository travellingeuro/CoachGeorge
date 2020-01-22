using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGXC.Models.Helper
{
    public class PracticeData:BindableBase
    {
        private string distance;
        public string Distance
        {
            get { return distance; }
            set { SetProperty(ref distance, value); }
        }

        private List<PracticeResult> practiceResults;
        public List<PracticeResult> PracticeResults
        {
            get { return practiceResults; }
            set { SetProperty(ref practiceResults, value); }
        }
    }
}
