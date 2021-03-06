using Syncfusion.SfChart.XForms.iOS.Renderers;
using Foundation;
using Prism;
using Prism.Ioc;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfGauge.XForms.iOS;
using Syncfusion.SfImageEditor.XForms.iOS;
using Syncfusion.SfKanban.XForms.iOS;
using Syncfusion.SfNavigationDrawer.XForms.iOS;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Expander;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;


namespace SGXC.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU0ODY4QDMxMzcyZTMzMmUzMEU4QWhNRUEwN05VM2cwaTFzV1RSWk1oemxOYklnbTA4SHpsc3hHTFZiNW89");
            Google.MobileAds.MobileAds.Configure("ca-app-pub-9800707284712065~3426477994");
global::Xamarin.Forms.Forms.Init();
SfCardLayoutRenderer.Init();
SfChartRenderer.Init();
            SfSwitchRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfImageEditorRenderer.Init();
            SfKanbanRenderer.Init();
            SfDigitalGaugeRenderer.Init();
            SfNavigationDrawerRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfNumericUpDownRenderer.Init();
            SfExpanderRenderer.Init();
            SfRatingRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfListViewRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfComboBoxRenderer.Init();
            SfCalendarRenderer.Init();
            SfButtonRenderer.Init();
            SfCardViewRenderer.Init();
            SfBorderRenderer.Init();


            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
