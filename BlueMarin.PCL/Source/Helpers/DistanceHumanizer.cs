using System;
using System.Collections.Generic;
using System.Globalization;

namespace BlueMarin
{
	public class HumanizedDistance
	{
		protected IDictionary<DistanceTypes, string> AbbrMappings = new Dictionary<DistanceTypes, string>{
			[DistanceTypes.Millimeters] = "mm",
			[DistanceTypes.Centimeters] = "cm",
			[DistanceTypes.Decimeters] =  "dm",
			[DistanceTypes.Meters] = "m",
			[DistanceTypes.Kilometers] =  "km",
		};

		readonly double valInMillimeters;

		public HumanizedDistance (double valSource, DistanceTypes typeSource = DistanceTypes.Meters)
		{
			this.valInMillimeters = (valSource * (System.Convert.ToDouble ((int)typeSource)));
		}

		public double Convert(DistanceTypes typeTarget = DistanceTypes.Best, double rounding = default(double))
		{
			double res = valInMillimeters / System.Convert.ToDouble ((int)typeTarget);
			if (res > (Math.Truncate (res) - rounding) && res < (Math.Truncate (res) + rounding))
				res = Math.Truncate (res);
			return res;
		}

		public string Humanize(DistanceTypes typeTarget = DistanceTypes.Best, string format = "{0:G} {1}", double rounding = .07f, CultureInfo cultureInfo = null, int decimals = 1, IDictionary<DistanceTypes, string> abbrMappings = null)
		{
			if (typeTarget == DistanceTypes.Best)
				typeTarget = CalculateBest ();

			var res = Convert (typeTarget, rounding);
			res = Math.Truncate (res * (decimals*10)) / (decimals*10);

			abbrMappings = abbrMappings ?? AbbrMappings;
			string type = "";
			if (abbrMappings.ContainsKey (typeTarget))
				type = abbrMappings [typeTarget];

			return string.Format (cultureInfo ?? CultureInfo.InvariantCulture, format, res, type);
		}

		public DistanceTypes CalculateBest()
		{
			/* TODO
			var values = Enum.GetValues(typeof(DistanceTypes)).;
			for (int i = 1; i < values.Length-1; i++) {
				var item = (int)values [i];
				var itemAfter = (int)values [i+1];
				if (valInMillimeters > item + (item * rounding) && valInMillimeters <= itemAfter + (item * rounding))
					return (DistanceTypes)item;
			}
			return DistanceTypes.Meters;
			*/

			var rounding = .8;

			if (valInMillimeters < rounding * (int)DistanceTypes.Centimeters)
				return DistanceTypes.Millimeters;
			else if (valInMillimeters < rounding * (int)DistanceTypes.Decimeters)
				return DistanceTypes.Centimeters;
			else if (valInMillimeters < rounding * (int)DistanceTypes.Meters)
				return DistanceTypes.Decimeters;
			else if (valInMillimeters < rounding * (int)DistanceTypes.Kilometers)
				return DistanceTypes.Meters;
			//else if (valInMillimeters < rounding * (int)DistanceTypes.Kilometers)
			return DistanceTypes.Kilometers;
		}
	}

	public enum DistanceTypes
	{
		Best =        0,
		Millimeters = 1,
		Centimeters = 10,
		Decimeters =  100,
		Meters =      1000,
		Kilometers =  1000000,
		//TODO: more
		//Infinity =    int.MaxValue,
	}
}

