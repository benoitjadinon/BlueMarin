using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace BlueMarin
{
	public abstract class Enumeration : IComparable
	{
		//TODO: make thread safe
		private static int autovalue = 0;


		protected Enumeration ()
		{
			Value = autovalue++;
		}

		protected Enumeration (string displayName) : this()
		{
			Name = displayName;
		}

		protected Enumeration (int value, string displayName)
		{
			Value = value;
			Name = displayName;
		}

		public int Value { get; set; }

		public string Name { 
			get;
			set;
		}

		/*
		public override string ToString ()
		{
			return Name;
		}
		*/

		public static IEnumerable<T> GetAll<T> () where T : Enumeration, new()
		{
			var type = typeof(T);
			var fields = type.GetFields (BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
			foreach (var info in fields) {
				var instance = new T ();
				var locatedValue = info.GetValue (instance) as T;
				if (locatedValue != null) {
					yield return locatedValue;
				}
			}
		}

		public override bool Equals (object obj)
		{
			var otherValue = obj as Enumeration;
			if (otherValue == null) {
				return false;
			}
			var typeMatches = GetType ().Equals (obj.GetType ());
			var valueMatches = Value.Equals (otherValue.Value);
			return typeMatches && valueMatches;
		}

		public override int GetHashCode ()
		{
			return Value.GetHashCode ();
		}

		public static int AbsoluteDifference (Enumeration firstValue, Enumeration secondValue)
		{
			var absoluteDifference = Math.Abs (firstValue.Value - secondValue.Value);
			return absoluteDifference;
		}

		public static T FromValue<T> (int value) where T : Enumeration, new()
		{
			var matchingItem = parse<T, int> (value, "value", item => item.Value == value);
			return matchingItem;
		}

		public static T FromDisplayName<T> (string displayName) where T : Enumeration, new()
		{
			var matchingItem = parse<T, string> (displayName, "display name", item => item.Name == displayName);
			return matchingItem;
		}

		private static T parse<T, TK> (TK value, string description, Func<T, bool> predicate) where T : Enumeration, new()
		{
			var matchingItem = GetAll<T> ().FirstOrDefault (predicate);
			if (matchingItem == null) {
				var message = string.Format ("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
				throw new ArgumentException (message);
			}
			return matchingItem;
		}

		public int CompareTo (object other)
		{
			return Value.CompareTo (((Enumeration)other).Value);
		}

	}
}

