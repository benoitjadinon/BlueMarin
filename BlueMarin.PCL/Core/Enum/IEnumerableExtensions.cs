using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace BlueMarin.Extensions
{
	public static class IEnumerableExtensions
	{
		///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
		///<param name="items">The enumerable to search.</param>
		///<param name="predicate">The expression to test the items against.</param>
		///<returns>The index of the first matching item, or -1 if no items match.</returns>
		public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate) {
			if (items == null) throw new ArgumentNullException("items");
			if (predicate == null) throw new ArgumentNullException("predicate");

			int retVal = 0;
			foreach (var item in items ?? new List<T>()) {
				if (predicate(item)) return retVal;
				retVal++;
			}
			return -1;
		}

		///<summary>Finds the index of the first occurence of an item in an enumerable.</summary>
		///<param name="items">The enumerable to search.</param>
		///<param name="item">The item to find.</param>
		///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
		public static int IndexOf<T>(this IEnumerable<T> items, T item) { 
			return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i)); 
		}
			
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			return source == null || !source.Any();
		}

		// http://www.ilker.de/csharp-trickshot-nullsafe-linq-query.html
		// from customer in GetCustomersBy(regionAndNameFilter).OrEmpty()
		public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> t) {
			return t ?? Enumerable.Empty<T> ();
		}
		
		public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> source)
		{
			return source ?? new T[0];
		}

		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items.AsNotNull()) action(item);
		}
	}
}

