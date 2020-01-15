using Prism.Mvvm;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace SGXC.Models
{
    public class Event : BindableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private DateTime racedate;
        public DateTime RaceDate
        {
            get { return racedate; }
            set { SetProperty(ref racedate, value); }
        }

        private string eventname;
        public string EventName
        {
            get { return eventname; }
            set { SetProperty(ref eventname, value); }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        private List<Runner> participants;

        [ManyToMany(typeof(RunnerEvent), CascadeOperations = CascadeOperation.CascadeInsert)]
        public List<Runner> Participants
        {
            get { return participants; }
            set { SetProperty(ref participants, value); }
        }

        private List<Time> distancetime;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Time> DistanceTime

        {
            get { return distancetime; }
            set { SetProperty(ref distancetime, value); }
        }


        private List<Bib> bibnumber;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Bib> BibNumber
        {
            get { return bibnumber; }
            set { SetProperty(ref bibnumber, value); }
        }

        private string season;
        public string Season
        {
            get { return season; }
            set { SetProperty(ref season, value); }
        }

        private bool isran;
        public bool IsRan
        {
            get { return isran; }
            set { SetProperty(ref isran, value); }
        }

        private bool israce;
        public bool IsRace
        {
            get { return israce; }
            set { SetProperty(ref israce, value); }
        }


    }
}
