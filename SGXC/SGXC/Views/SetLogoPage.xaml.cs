using Xamarin.Forms;

namespace SGXC.Views
{
    public partial class SetLogoPage : ContentPage
    {
        public SetLogoPage()
        {
            InitializeComponent();
            editor.Save(".jpg", new Size(48, 48));
            editor.SetToolbarItemVisibility("Reset,Undo,Redo", false);

        }
    }
}
