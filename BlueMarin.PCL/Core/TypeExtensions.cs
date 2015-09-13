using System;
using System.Linq;
using System.Collections.Generic;

namespace BlueMarin.Extensions
{
	public static class TypeExtensions
	{
		public static IEnumerable<Type> DoesNotContain (this IEnumerable<Type> types, string contains)
		{
			return from x in types
					where !x.Name.Contains(contains)
				select x;
		}
	}
}

