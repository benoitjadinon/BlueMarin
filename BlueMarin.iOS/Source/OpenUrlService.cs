using System;
using Foundation;
using UIKit;

namespace BlueMarin.iOS
{
	public class OpenUrlService : IOpenUrlService
	{

		public void OpenUrl (string url, string mime = null, bool extra = false)
		{
			if (!url.Contains (":/")) {
				url = "http://" + url;
			}

			var nsUrl = new NSUrl(url);
			if (UIApplication.SharedApplication.CanOpenUrl(nsUrl))
				UIApplication.SharedApplication.OpenUrl(nsUrl);
		}
	}
}
