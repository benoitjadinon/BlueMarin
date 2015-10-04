using System;

namespace BlueMarin
{
	public static class StringExtensions
	{
		public static bool IsNullOrBlank(this String @this)
		{
			return @this == null || @this.Trim().Length == 0 || @this == "";
		}

		public static bool IsEmpty(this String @this)
		{
			return @this.IsNullOrBlank ();
		}

		public static string OrEmpty(this String text)
		{
			return text ?? "";
		}

		public static string Substring (this string @this, string startString, string endString)
		{
			if (@this == null)
				return null;

			var start = @this.IndexOf (startString) + startString.Length;
			var nextOcc = @this.IndexOf (endString, start);
			return @this.Substring (start, nextOcc - start);
		}

		public static string Reverse (this string @this)
		{
			if (@this == null)
				return null;
			
			char[] charArray = @this.ToCharArray();
			Array.Reverse( charArray );
			return new string( charArray );
		}

		public static string ToBase64 (this string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
	}
}