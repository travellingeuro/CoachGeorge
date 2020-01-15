using Prism.Mvvm;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SGXC.Models
{
    public class Runner : BindableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string grade;
        public string Grade
        {
            get { return grade; }
            set { SetProperty(ref grade, value); }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set { SetProperty(ref gender, value); }
        }

        private string season;
        public string Season
        {
            get { return season; }
            set { SetProperty(ref season, value); }
        }

        private string contacts;
        public string Contacts
        {
            get { return contacts; }
            set { SetProperty(ref contacts, value); }
        }

        private List<Event> events;
        [ManyToMany(typeof(RunnerEvent), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Event> Events
        {
            get { return events; }
            set { SetProperty(ref events, value); }
        }


        private List<Time> distancetime;
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
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

    }

}
