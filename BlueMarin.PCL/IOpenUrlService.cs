using System;
using System.Threading.Tasks;

#if __ANDROID__
using Android.App;
using Android.OS;
using Android.Content;
using Android.Util;
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
	
}
