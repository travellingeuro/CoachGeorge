using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SGXC.Controls
{
    [Preserve(AllMembers = true)]
    public class AdMobView : View
    {
        public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
                   nameof(AdUnitId),
                   typeof(string),
                   typeof(AdMobView),
                   string.Empty);

        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }
    }
}
