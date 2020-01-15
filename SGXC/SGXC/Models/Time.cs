using Prism.Mvvm;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace SGXC.Models
{
    public class Time : BindableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private TimeSpan time;
        public TimeSpan Times
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

        private int repetitions;
        public int Repetitions
        {
            get { return repetitions; }
            set { SetProperty(ref repetitions, value); }
        }

        private int runnerId;
        [ForeignKey(typeof(Runner))]
        public int RunnerId
        {
            get { return runnerId; }
            set { SetProperty(ref runnerId, value); }
        }

        private Runner runner;
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Runner Runner
        {
            get { return runner; }
            set { SetProperty(ref runner, value); }
        }

        private int? eventId;
        [ForeignKey(typeof(Event))]
        public int? EventId
        {
            get { return eventId; }
            set { SetProperty(ref eventId, value); }
        }

        private Event events;
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Event Event
        {
            get { return events; }
            set { SetProperty(ref events, value); }
        }






    }
}
