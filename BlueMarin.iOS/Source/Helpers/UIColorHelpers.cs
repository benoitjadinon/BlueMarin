using System;
using UIKit;

namespace BlueMarin.iOS
{
	public static class UIColorHelpers
	{
		// https://gist.github.com/Sankra/3467666fb70140c01479
		public static UIColor ColorForRatio(float[] startColor, float[] endColor, float colorRatio) {
			return UIColor.FromRGB(startColor[0] + (endColor[0] - startColor[0]) * colorRatio, 
				startColor[1] + (endColor[1] - startColor[1]) * colorRatio, 
				startColor[2] + (endColor[2] - startColor[2]) * colorRatio);
		}
	}
}