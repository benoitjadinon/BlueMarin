using System;
using System.Collections.Generic;

namespace BlueMarin
{
	public static class TimeSpanExtensions
	{
		// based on : http://stackoverflow.com/questions/5438363/timespan-pretty-time-format-in-c-sharp
		public static string Humanize (this TimeSpan span, 
			IDictionary<TimeTypes, string> mappings = null,
			bool pluralize = false,
			string spaceBetweenValueAndUnits = " ",
			string spaceBetweenTypes = " ",
			TimeTypes minUnit = TimeTypes.MilliSecond
		){
			if (mappings == null)
				mappings = TimeMappings;

			if (span == TimeSpan.Zero) 
				return string.Format ("0 {0}{1}", mappings.ContainsKey (TimeTypes.Minute) ? mappings[TimeTypes.Minute] : "", pluralize ? "s" : string.Empty);

			const string format = "{0}{1}{2}";

			var list = new List<string> (); 

			if (span.Days > 0    && (int)minUnit <= (int)TimeTypes.Day)
				list.Add (string.Format(format, span.Days, spaceBetweenValueAndUnits, mappings.ContainsKey (TimeTypes.Day) ? mappings[TimeTypes.Day] : "", pluralize && span.Days > 1 ? "s" : String.Empty));

			if (span.Hours > 0   && (int)minUnit <= (int)TimeTypes.Hour)
				list.Add (string.Format(format, span.Hours, spaceBetweenValueAndUnits, mappings.ContainsKey (TimeTypes.Hour) ? mappings[TimeTypes.Hour] : "", pluralize && span.Hours > 1 ? "s" : String.Empty));

			if (span.Minutes > 0 && (int)minUnit <= (int)TimeTypes.Minute)
				list.Add (string.Format(format, span.Minutes, spaceBetweenValueAndUnits, mappings.ContainsKey (TimeTypes.Minute) ? mappings[TimeTypes.Minute] : "", pluralize && span.Minutes > 1 ? "s" : String.Empty));

			if (span.Seconds > 0 && (int)minUnit <= (int)TimeTypes.Second)
				list.Add (string.Format(format, span.Seconds, spaceBetweenValueAndUnits, mappings.ContainsKey (TimeTypes.Second) ? mappings[TimeTypes.Second] : "", pluralize && span.Seconds > 1 ? "s" : String.Empty));

			if (span.Milliseconds > 0 && (int)minUnit <= (int)TimeTypes.MilliSecond)
				list.Add (string.Format(format, span.Milliseconds, spaceBetweenValueAndUnits, mappings.ContainsKey (TimeTypes.MilliSecond) ? mappings[TimeTypes.MilliSecond] : "", pluralize && span.Milliseconds > 1 ? "s" : String.Empty));

			return string.Join (spaceBetweenTypes, list);
		}

		public static IDictionary<TimeTypes, string> TimeMappings = new Dictionary<TimeTypes, string>{
			[TimeTypes.MilliSecond] = "millisecond",
			[TimeTypes.Second] = "second",
			[TimeTypes.Minute] =  "minute",
			[TimeTypes.Hour] = "hour",
			[TimeTypes.Day] =  "day",
		};
		public static IDictionary<TimeTypes, string> TimeMappingsAbbr = new Dictionary<TimeTypes, string>{
			[TimeTypes.MilliSecond] = "ms",
			[TimeTypes.Second] = "s",
			[TimeTypes.Minute] =  "m",
			[TimeTypes.Hour] = "h",
			[TimeTypes.Day] =  "d",
		};
	}

	[Flags]
	public enum TimeTypes
	{
		All = 0,
		MilliSecond,// = 1,
		Second,// =  1000,
		Minute,// = 60000,
		Hour,// = 3600000,
		Day,// = 86400000,
	}
}

