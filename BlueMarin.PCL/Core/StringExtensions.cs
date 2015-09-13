using System;

namespace BlueMarin.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrBlank(this String text)
		{
			return text == null || text.Trim().Length == 0 || text == "";
		}

		public static string ToMD5(this String text)
		{
			return MD5Core.GetHashString (text);
		}

		public static string OrEmpty(this String text)
		{
			return text ?? "";
		}
	}
}

