using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SGXC.Converters
{
    [Preserve(AllMembers = true)]
    public class BooleanToTextConverter : IValueConverter
    {
        public string TrueText { get; set; }

        public string FalseText { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? TrueText : FalseText;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
