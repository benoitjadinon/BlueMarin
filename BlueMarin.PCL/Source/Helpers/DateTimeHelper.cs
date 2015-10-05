using System;
using System.Globalization;

namespace BlueMarin
{
	// https://gist.github.com/kpespisa/e87059db1de761690b70
	public static class DateTimeHelper
	{
		public static readonly DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime UnixTimeToDateTime(string text)
		{
			double seconds = double.Parse(text, CultureInfo.InvariantCulture);
			var time = EpochTime.AddSeconds(seconds);

			return time;
		}

		public static double DateTimeToUnixTime(DateTime date)
		{
			TimeSpan diff = date - EpochTime;
			return Math.Floor(diff.TotalSeconds);
		}
	}
}

