using Android.Content;
using System.Security;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SGXC.Controls.CalenderDatePicker), typeof(SGXC.Droid.CalenderDatePickerRenderer))]

namespace SGXC.Droid
{
    /// <summary>
    /// Implementation of Calender Data picker control.
    /// </summary>
    public class CalenderDatePickerRenderer : DatePickerRenderer
    {
        public CalenderDatePickerRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Used to customize the date picker control.
        /// </summary>
        /// <param name="e">The DatePicker</param>
        /// 
        [SecurityCritical]
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                this.Control.SetBackground(null);
                Control.Text = (e.NewElement as Controls.CalenderDatePicker).PlaceHolderText;
                Control.SetTextColor(new Android.Graphics.Color(76, 140, 74));
                Control.Gravity = Android.Views.GravityFlags.CenterVertical;
                Control.SetPadding(0, 0, 0, 0);
                this.Control.SetHighlightColor(new Android.Graphics.Color(76, 140, 74));
                               
            }
        }
    }
}