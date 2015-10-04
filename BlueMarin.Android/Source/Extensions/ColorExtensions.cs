using System;
using Android.Graphics;

namespace BlueMarin.Android
{
	public static class ColorExtensions
	{
		public static Color AddAlpha (this Color color, byte alpha = 0xFF)
		{
			color.A = alpha;

			return color;
		}

		public static Color ToNative(this long hexValue) 
		{
			return Color.Argb(
				((int)(hexValue & 0xff000000) >> 24),
				((int)(hexValue & 0xff0000) >> 16),
				((int)(hexValue & 0xff00) >> 8),
				((int)(hexValue & 0xff)));
		}

		public static Color ToColor(this string hexString) 
		{
			return Color.ParseColor (hexString);
		}
	}
}

