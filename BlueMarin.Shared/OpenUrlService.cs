using System;
using System.Threading.Tasks;

#if __ANDROID__
using Android.Content;

#elif __IOS__
using UIKit;
using Foundation;
#endif

namespace BlueMarin
{
	public interface IOpenUrlService
	{
		void OpenUrl (string url, string mime = null);
	}

	public class OpenUrlService : IOpenUrlService
	{
		#if __ANDROID__

		readonly Context mContext;

		public OpenUrlService (Context context)
		{
			this.mContext = context;		
		}

		#else
		
		public OpenUrlService ()
		{			
		}

		#endif
		
		public void OpenUrl (string url, string mime = null)
		{
			if (!url.StartsWith ("http")) {
				url = "http://" + url;
			}

			#if __ANDROID__

			var uri = global::Android.Net.Uri.Parse (url);
			Intent intent = new Intent (Intent.ActionView);
			if (mime != null)
				intent.SetDataAndType (uri, mime);
			else 
				intent.SetData (uri);

			intent.AddFlags (ActivityFlags.NewTask);

			mContext.StartActivity (intent);
			//Intent chooser = Intent.CreateChooser (intent, "Open with");
			//mContext.StartActivity(chooser);

			#elif __IOS__

			var nsUrl = new NSUrl(url);
			if (UIApplication.SharedApplication.CanOpenUrl(nsUrl))
				UIApplication.SharedApplication.OpenUrl(nsUrl);

			#endif
		}
	}
}

