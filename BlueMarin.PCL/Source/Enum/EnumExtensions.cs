using System;

namespace BlueMarin
{
	// http://blog.raz-l.com/2010/08/enumerationextensions.html
	/*
	[Flags]
	public enum Positions
	{
		None = 0,
	    Left = 1,
	    Right = 2,
	    Top = 4,
	    Bottom = 8,
    }
    Positions twoPositions = Positions.Left | Position.Top;
    Console.WriteLine(twoPositions.Contains(Position.Top)) // true
	*/
	public static class EnumExtensions
	{
		public static T Append<T>(this Enum type, T value)
		{
			return (T)(object)(((int)(object)type | (int)(object)value));
		}

		public static T Remove<T>(this Enum type, T value)
		{
			return (T)(object)(((int)(object)type & ~(int)(object)value));
		}

		public static bool Contains<T>(this Enum type, T value)
		{
			return (((int)(object)type & (int)(object)value) == (int)(object)value);
		}

		public static bool ContainsAny<T>(this Enum type, T value)
		{
			return (((int)(object)type & (int)(object)value) != 0);
		}

		public static bool ContainsNot<T>(this Enum type, T value)
		{
			return !Contains(type, value);
		}

		public static bool Is<T>(this Enum type, T value)
		{
			return (((int)(object)type == (int)(object)value));
		}
	}
}

