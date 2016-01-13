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

		// https://gist.github.com/kpespisa/e87059db1de761690b70
		public static double ToUnixTime(this DateTime date)
		{
			TimeSpan diff = date - DateTimeHelper.EpochTime;
			return Math.Floor(diff.TotalSeconds);
		}

		/// <summary>
		/// Adds the weekdays.
		/// http://dotnetslackers.com/articles/aspnet/5-Helpful-DateTime-Extension-Methods.aspx
		/// </summary>
		/// <example>
		/// var newYearsEve2010 = new DateTime(2010, 12, 31);
		/// var firstWeekdayAfterNewYearsEve2010 = newYearsEve2010.AddWeekdays(1);
		/// </example>
		/// <returns>The weekdays.</returns>
		/// <param name="days">Days.</param>
		public static DateTime AddWeekdays(this DateTime date, int days)
		{
			var sign = days < 0 ? -1 : 1;
			var unsignedDays = Math.Abs(days);
			var weekdaysAdded = 0;
			while (weekdaysAdded < unsignedDays)
			{
				date = date.AddDays(sign);
				if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
					weekdaysAdded++;
			}
			return date;
		}

		/// <summary>
		/// http://dotnetslackers.com/articles/aspnet/5-Helpful-DateTime-Extension-Methods.aspx
		/// </summary>
		/// <example>var quittingTime = DateTime.Now.SetTime(5);</example>
		/// <returns>The time.</returns>
		/// <param name="hour">Hour.</param>
		public static DateTime SetTime(this DateTime date, int hour)
		{
			return date.SetTime(hour, 0, 0, 0);
		}
		/// <example>var quittingTime = DateTime.Now.SetTime(5, 45);</example>
		public static DateTime SetTime(this DateTime date, int hour, int minute)
		{
			return date.SetTime(hour, minute, 0, 0);
		}
		public static DateTime SetTime(this DateTime date, int hour, int minute, int second)
		{
			return date.SetTime(hour, minute, second, 0);
		}
		public static DateTime SetTime(this DateTime date, int hour, int minute, int second, int millisecond)
		{
			return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
		}

		/// <summary>
		/// http://dotnetslackers.com/articles/aspnet/5-Helpful-DateTime-Extension-Methods.aspx
		/// </summary>
		/// <example>
		/// var firstDayOfThisMonth = DateTime.Now.FirstDayOfMonth();
		/// <returns>The day of month.</returns>
		public static DateTime FirstDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}
		/// <example>
		/// var lastDayOfThisMonth = DateTime.Now.LastDayOfMonth();</example>
		/// <returns>The day of month.</returns>
		public static DateTime LastDayOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
		}

		/// <summary>
		/// http://dotnetslackers.com/articles/aspnet/5-Helpful-DateTime-Extension-Methods.aspx
		/// </summary>
		/// <example>
		/// DateTime? nullableDate = ...;
		/// string formattedDate = nullableDate.ToString("...");
		/// </example>
		public static string ToString(this DateTime? date)
		{
			return date.ToString(null, DateTimeFormatInfo.CurrentInfo);
		}
		public static string ToString(this DateTime? date, string format)
		{
			return date.ToString(format, DateTimeFormatInfo.CurrentInfo);
		}
		public static string ToString(this DateTime? date, IFormatProvider provider)
		{
			return date.ToString(null, provider);
		}
		public static string ToString(this DateTime? date, string format, IFormatProvider provider)
		{
			if (date.HasValue)
				return date.Value.ToString(format, provider);
			else
				return string.Empty;
		}
	}
}

