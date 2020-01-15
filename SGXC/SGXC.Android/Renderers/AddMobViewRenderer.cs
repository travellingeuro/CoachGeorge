using Android.Content;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Widget;

[assembly: ExportRenderer(typeof(SGXC.Controls.AdMobView), typeof(SGXC.Droid.AdMobViewRenderer))]
namespace SGXC.Droid
{
	public class AdMobViewRenderer : ViewRenderer<Controls.AdMobView, AdView>
	{
		public AdMobViewRenderer(Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdMobView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null && Control == null)
				e.NewElement.HeightRequest = GetSmartBannerDpHeight();
				SetNativeControl(CreateAdView());
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(AdView.AdUnitId))
				Control.AdUnitId = AppSettings.AndroidAdUnitId;
		}

		private int GetSmartBannerDpHeight()
		{
			var dpHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;

			if (dpHeight <= 400) return 32;
			if (dpHeight > 400 && dpHeight <= 720) return 50;
			return 90;
		}

		private AdView CreateAdView()
		{
			var adView = new AdView(Context)
			{
				AdSize = AdSize.SmartBanner,
				AdUnitId = AppSettings.AndroidAdUnitId
			};

			adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
			
			adView.LoadAd(new AdRequest.Builder().Build());
			
			return adView;

		}
	}
}