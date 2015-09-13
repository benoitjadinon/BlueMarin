using System;
using System.Linq;
using System.Reflection;

namespace BlueMarin.Extensions
{
	public static class EnumValuesExtensions
	{
		public static Array GetEnumValues(this Type type)
		{
			if (!type.IsEnum)
				throw new ArgumentException("GetEnumValues: Type '" + type.Name + "' is not an enum");

			return
				(
					from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
					where field.IsLiteral
					select (object)field.GetValue(null)
				)
				.ToArray();
		}

		public static string[] GetEnumNames(this Type type)
		{
			if (!type.IsEnum)
				throw new ArgumentException("GetEnumNames: Type '" + type.Name + "' is not an enum");

			return
				(
					from field in type.GetFields(BindingFlags.Public | BindingFlags.Static)
					where field.IsLiteral
					select field.Name
				)
				.ToArray<string>();
		}
	}
}

