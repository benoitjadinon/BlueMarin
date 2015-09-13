using System;

namespace XamarinTools
{
	public static class IntExtensions
	{
		public static int HoursToMinutes(this int hours)
		{
			return hours * 60;
		}

		public static int MinutesToSeconds (this int minutes)
		{
			return minutes * 60;
		}

		public static int SecondsToMilliseconds(this int seconds)
		{
			return seconds * 1000;
		}

		public static int MinutesToMilliseconds (this int minutes)
		{
			return minutes.MinutesToSeconds().SecondsToMilliseconds();
		}

		public static int HoursToSeconds(this int hours)
		{
			return hours.HoursToMinutes().MinutesToSeconds();
		}
		public static int HoursToMilliseconds(this int hours)
		{
			return hours.HoursToMinutes().MinutesToSeconds().SecondsToMilliseconds();
		}
	}
}

