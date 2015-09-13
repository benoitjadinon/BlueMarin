using System;
using System.Globalization;

namespace BlueMarin
{
	public static class DateTimeExtensions
	{
		public static string ToShortDateString (this DateTime? date)
		{
			if (null == date)
			{
				return string.Empty;
			}

			return date.Value.ToString (DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
		}

		public static string ToShortTimeString (this DateTime? date)
		{
			if (null == date)
			{
				return string.Empty;
			}

			return date.Value.ToString (DateTimeFormatInfo.CurrentInfo.ShortTimePattern);
		}

        public static DateTime AddWorkdays(this DateTime originalDate, int workDays)
        {
            DateTime tmpDate = originalDate;
            while (workDays > 0)
            {
                tmpDate = tmpDate.AddDays(1);
                if (tmpDate.DayOfWeek < DayOfWeek.Saturday &&
                    tmpDate.DayOfWeek > DayOfWeek.Sunday /*&&
                    tmpDate.IsHoliday()*/)
                    workDays--;
            }
            return tmpDate;
        }

		public static DateTime ResetHours(this DateTime date)
		{
			return date.ChangeTime();
		}

		public static DateTime ChangeTime(this DateTime dateTime, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			return new DateTime(
				dateTime.Year,
				dateTime.Month,
				dateTime.Day,
				hours,
				minutes,
				seconds,
				milliseconds,
				kind
			);
		}

		public static DateTime WithCurrentTime(this DateTime dateTime, DateTimeKind kind = DateTimeKind.Unspecified)
		{
			var now = DateTime.Now;
			return dateTime.ChangeTime(now.Hour, now.Minute, now.Second, now.Millisecond, kind);
		}
	}
}

