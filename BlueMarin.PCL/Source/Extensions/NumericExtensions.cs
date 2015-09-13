using System;
using System.Collections.Generic;

namespace BlueMarin
{
	public static class NumericExtensions
	{
		/// <summary>
		/// Convert to Radians.
		/// </summary>
		/// <param name="val">The value to convert to radians</param>
		/// <returns>The value in radians</returns>
		public static double DegreesToRadians(this double angle)
		{
			return (Math.PI / 180) * angle;
		}

		public static double RadiansToDegrees(this double angle)
		{
			return angle * (180.0 / Math.PI);
		}

		public static double Clamp(this double self, double min, double max)
		{
			return Math.Min(max, Math.Max(self, min));
		}

		public static int Clamp(this int self, int min, int max)
		{
			return Math.Min(max, Math.Max(self, min));
		}


		private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
		{
			typeof(int),
			typeof(uint),
			typeof(double),
			typeof(decimal),
			//TODO: other numeric types (nint,...) https://msdn.microsoft.com/en-us/library/ms228360(v=vs.90).aspx
		};

		internal static bool IsNumericType(Type type)
		{
			throw new NotImplementedException ("IsNumericType not ready");
			return NumericTypes.Contains(type)
				|| NumericTypes.Contains(Nullable.GetUnderlyingType(type));
		}
	}
}

