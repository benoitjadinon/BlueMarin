using System;

namespace BlueMarin.Android
{
	public static class DisplayMetricsExtensions
	{
		public static int ConvertPixelsToDp(this global::Android.Util.DisplayMetrics @this, float pixelValue)
		{
			return (int) (pixelValue/@this.Density);
		}
	}
}

