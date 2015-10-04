using System;
using System.Linq;
using System.Collections.Generic;

namespace BlueMarin
{
	public static class TypeExtensions
	{
		public static IEnumerable<Type> DoesNotContain (this IEnumerable<Type> types, string name)
		{
			return from x in types
				where !x.Name.Contains(name)
				select x;
		}
	}
}

