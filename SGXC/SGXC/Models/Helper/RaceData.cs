﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGXC.Models.Helper
{
    public class RaceData:BindableBase
    {
        private string season;
        public string Season
        {
            get { return season; }
            set { SetProperty(ref season, value); }
        }

        private List<RaceResult> raceresults;
        public List<RaceResult> RaceResults
        {
            get { return raceresults; }
            set { SetProperty(ref raceresults, value); }
        }
    }
}
