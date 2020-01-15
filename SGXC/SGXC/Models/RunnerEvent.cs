using Prism.Mvvm;
using SQLiteNetExtensions.Attributes;

namespace SGXC.Models
{
    public class RunnerEvent : BindableBase
    {

        private int runnerId;
        [ForeignKey(typeof(Runner))]
        public int RunnerId
        {
            get { return runnerId; }
            set { SetProperty(ref runnerId, value); }
        }
        private int eventId;
        [ForeignKey(typeof(Event))]
        public int EventId
        {
            get { return eventId; }
            set { SetProperty(ref eventId, value); }
        }

    }
}