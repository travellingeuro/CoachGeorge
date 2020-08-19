using Android.App;
using Android.Content.PM;
using Android.OS;

using Prism;
using Prism.Ioc;
using System.Security;

namespace SGXC.Droid
{

    [Activity(Label = "SGXC", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        [SecurityCritical]
        protected override void OnCreate(Bundle bundle)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzA1NjcyQDMxMzgyZTMyMmUzMFpHYS9NcWNCNXI4N2hldnp1UnNpbWNNK1o1dmQ5OU5qU240bURlY2p4L2M9");
            Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, "ca-app-pub-9800707284712065~2057556009");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        [SecurityCritical]
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

