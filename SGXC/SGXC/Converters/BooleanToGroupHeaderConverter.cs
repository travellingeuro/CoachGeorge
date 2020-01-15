using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SGXC.Converters
{
    [Preserve(AllMembers = true)]
    public class BooleanToGroupHeaderConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {


                if ((bool)value)
                {
                    return "Past events";
                }
                else
                {
                    return "Events not ran";
                }

            }

            return string.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
