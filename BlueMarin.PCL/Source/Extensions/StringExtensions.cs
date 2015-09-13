using System;

namespace BlueMarin
{
	public static class StringExtensions
	{
		public static bool IsNullOrBlank(this String text)
		{
			return text == null || text.Trim().Length == 0 || text == "";
		}

		public static bool IsEmpty(this String text)
		{
			return text.IsNullOrBlank ();
		}

		public static string ToMD5(this String text)
		{
			return MD5Core.GetHashString (text);
		}

		public static string OrEmpty(this String text)
		{
			return text ?? "";
		}

		public static string Substring (this string @this, string startString, string endString)
		{
			var start = @this.IndexOf (startString) + startString.Length;
			return @this.Substring (start, @this.IndexOf (endString, start) - start);
		}
	}
}

