using System;
using System.Text;
using static System.Text.Encoding;

namespace BlueMarin
{
	public static class StringExtensions
	{
		[Obsolete("use IsEmpty instead")]
		public static bool IsNullOrBlank (this String @this) => @this.IsEmpty ();

		//https://github.com/praeclarum/Praeclarum/blob/master/Praeclarum/StringHelper.cs
		public static bool IsEmpty (this String @this)
		{
			if (@this == null) return true;
			var len = @this.Length;
			if (len == 0) return true;
			for (var i = 0; i < len; i++)
				if (!char.IsWhiteSpace (@this[i]))
					return false;
			return true;
		}

		public static string OrEmpty (this String text) => text.Or ();

		public static string Or (this String text, string replacement = "") => text ?? replacement;
	
		public static string Substring (this string @this, string startString, string endString)
		{
			if (@this == null)
				return null;

			var start = @this.IndexOf (startString);
			if (start == -1)
				return null;
			start += startString.Length;
			var nextOcc = @this.IndexOf (endString, start);
			if (nextOcc == -1)
				return null;
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

		public static string ToBase64 (this string plainText) => System.Convert.ToBase64String(UTF8.GetBytes(plainText));
	}
}