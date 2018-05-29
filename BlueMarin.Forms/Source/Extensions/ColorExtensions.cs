﻿using System;
using Xamarin.Forms;

namespace BlueMarin
{
	public static class ColorExtensions
	{
		private static Random randonGen = new Random();

		public static Color RandomDebugColor (this Color color)
		{
			return GetRandomDebugColor(color);
		}
		public static Color GetRandomDebugColor (Color fallback)
		{
#if DEBUG
			return Color.FromRgb(randonGen.Next(255), randonGen.Next(255), randonGen.Next(255));
			#else
			return fallback;
#endif
		}
	}
}

