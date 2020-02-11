using Xamarin.Essentials;

namespace SGXC
{
    public static class AppSettings
    {

        const string defaultLogo = "event.jpg";
        const string defaultName = "Your Team Name";

        //- Test Ad for Android
        //const string defaultAndroidAd = "ca-app-pub-3940256099942544/6300978111";
        //Actual key for ads
        const string defaultAndroidAd = "ca-app-pub-9800707284712065/1215247625";
        const string defalutIOsAd = "ca-app-pub-9800707284712065/2113396320";



        public static string TeamLogo
        {
            get => Preferences.Get(nameof(TeamLogo), defaultLogo);
            set => Preferences.Set(nameof(TeamLogo), value);
        }

        public static string TeamName
        {
            get => Preferences.Get(nameof(TeamName), defaultName);
            set => Preferences.Set(nameof(TeamName), value);
        }

        public static string AndroidAdUnitId
        {
            get => Preferences.Get(nameof(AndroidAdUnitId), defaultAndroidAd);
            set => Preferences.Set(nameof(AndroidAdUnitId), value);
        }
        
        public static string IOsAdUnitId
        {
            get => Preferences.Get(nameof(IOsAdUnitId), defalutIOsAd);
            set => Preferences.Set(nameof(IOsAdUnitId), value);
        }
    }
}
