using System;
using System.Threading.Tasks;

#if __ANDROID__
using Android.OS;
using Android.Content;

#elif __IOS__
using UIKit;
using Foundation;
#endif

namespace BlueMarin
{
	public interface IOpenUrlService
	{
		void OpenUrl (string url, string mime = null, bool extra = false);
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
		
		public void OpenUrl (string url, string mime = null, bool extra = false)
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

			if (extra){
				Intent intent2 = new Intent (Intent.ActionView);
				intent2.SetData (uri);
				Intent[] intentArray =  { intent2 }; 
				intent = Intent.CreateChooser (intent, "Open");
				intent.PutExtra(Intent.ExtraInitialIntents, intentArray);
			}

			try {
				mContext.StartActivity (intent);
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				Intent intent3 = new Intent (Intent.ActionView);
				mContext.StartActivity (intent3);
			}

			#elif __IOS__

			var nsUrl = new NSUrl(url);
			if (UIApplication.SharedApplication.CanOpenUrl(nsUrl))
				UIApplication.SharedApplication.OpenUrl(nsUrl);

			#endif
		}
	}
}

