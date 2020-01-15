using Prism.Mvvm;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SGXC.Models
{
    public class Bib : BindableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private int number;
        public int Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }

        private int runnerId;
        [ForeignKey(typeof(Runner))]
        public int RunnerId
        {
            get { return runnerId; }
            set { SetProperty(ref runnerId, value); }
        }

        private Runner runner;
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Runner Runner
        {
            get { return runner; }
            set { SetProperty(ref runner, value); }
        }

        private int eventId;
        [ForeignKey(typeof(Event))]
        public int EventId
        {
            get { return eventId; }
            set { SetProperty(ref eventId, value); }
        }

        private Event events;
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Event Event
        {
            get { return events; }
            set { SetProperty(ref events, value); }
        }

    }
}
