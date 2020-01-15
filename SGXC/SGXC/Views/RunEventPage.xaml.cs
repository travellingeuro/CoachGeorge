using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class RunEventPage : ContentPage
    {
        public RunEventPage()

        {
            InitializeComponent();

        }

        private void RunEvent_SizeChanged(object sender, System.EventArgs e)
        {
            //Landscape
            if (Width > Height)
            {
                WatchContainer.Orientation = StackOrientation.Horizontal;


            }
            else
            {
                WatchContainer.Orientation = StackOrientation.Vertical;
            }
        }
    }
}
