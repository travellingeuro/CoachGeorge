using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class AddEventPage : ContentPage
    {

        public AddEventPage()

        {
            InitializeComponent();
            calendar.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            calendar.IsVisible = !calendar.IsVisible;
        }


        private void calendar_OnCalendarTapped(object sender, Syncfusion.SfCalendar.XForms.CalendarTappedEventArgs e)
        {
            calendar.IsVisible = !calendar.IsVisible;
        }

        private void calendar_OnMonthCellLoaded(object sender, Syncfusion.SfCalendar.XForms.MonthCellLoadedEventArgs e)
        {
            var cal = (Syncfusion.SfCalendar.XForms.SfCalendar)sender;            
            cal.MonthViewSettings.DateSelectionColor = Color.FromHex("#1b5e20");
            cal.MonthViewSettings.TodaySelectionBackgroundColor = Color.FromHex("#1b5e20");
            cal.MonthViewSettings.TodayBorderColor = Color.FromHex("1b5e20");
            cal.MonthViewSettings.TodayTextColor = Color.FromHex("1b5e20");
        }
    }
}
