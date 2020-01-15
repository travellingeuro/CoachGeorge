using Prism.Mvvm;

namespace SGXC.Models.Helper
{
    public class EventWithRunners : BindableBase
    {
        private string category;
        public string Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }


        private Runner runner;
        public Runner Runner
        {
            get { return runner; }
            set { SetProperty(ref runner, value); }
        }

        private Time rantime;
        public Time RanTime
        {
            get { return rantime; }
            set { SetProperty(ref rantime, value); }
        }

        private bool canbesplited = true;
        public bool CanBeSplited
        {
            get { return canbesplited; }
            set { SetProperty(ref canbesplited, value); }
        }

        private int namesort;
        public int NameSort
        {
            get
            {
                return (RanTime.Repetitions);
            }

            set { SetProperty(ref namesort, value); }
        }

    }
}
