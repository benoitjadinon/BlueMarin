using Xamarin.Forms;

namespace BlueMarin
{
	public static class ThicknessExtensions
	{
		public static Thickness WithBottom (this Thickness thickness, double bottom)
		{
			thickness.Bottom = bottom;
			return thickness;
		}
		public static Thickness WithTop (this Thickness thickness, double top)
		{
			thickness.Top = top;
			return thickness;
		}
	}
}

