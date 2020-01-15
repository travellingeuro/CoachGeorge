using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGXC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : ContentView
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        private void clickToShowPopup_Clicked(object sender, EventArgs e)
        {
            popup.Show(0, 20);
        }
    }
}