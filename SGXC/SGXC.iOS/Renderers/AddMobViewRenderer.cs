using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using Google.MobileAds;
using SGXC.Controls;
using SGXC.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace SGXC.iOS
{
	public class AdMobViewRenderer : ViewRenderer<AdMobView, BannerView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
		{
			base.OnElementChanged(e);
			if (Control == null)
			{
				SetNativeControl(CreateBannerView());
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(BannerView.AdUnitID))
				Control.AdUnitID = AppSettings.IOsAdUnitId;
		}

		private BannerView CreateBannerView()
		{
			var bannerView = new BannerView(AdSizeCons.SmartBannerPortrait)
			{
				AdUnitID = AppSettings.IOsAdUnitId,
				RootViewController = GetVisibleViewController()
			};

			bannerView.LoadRequest(GetRequest());

			Request GetRequest()
			{
				var request = Request.GetDefaultRequest();
				return request;
			}

			return bannerView;
		}

		private UIViewController GetVisibleViewController()
		{
			var windows = UIApplication.SharedApplication.Windows;
			foreach (var window in windows)
			{
				if (window.RootViewController != null)
				{
					return window.RootViewController;
				}
			}
			return null;
		}
	}
}