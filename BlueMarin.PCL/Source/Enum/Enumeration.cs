using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BlueMarin
{
	//TODO: compare with https://github.com/HeadspringLabs/Tarantino/blob/master/src/Tarantino.Core/Commons/Model/Enumerations/Enumeration.cs
	// https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
	//TODO: make thread safe
	//TODO: make serializable !!! this is not, and very dangerous
	public abstract class Enumeration : IComparable
	{
		private static int autovalue = 0;

		protected Enumeration ()
		{
			Value = autovalue++;
		}

		private Enumeration (int value){
			Value = value;
			if (Value > autovalue)
				autovalue = ++Value;
		}
			
		protected Enumeration (string displayName) : this()
		{
			Name = displayName;
		}

		private Enumeration (int value, string displayName) : this (value)
		{
			Name = displayName;
		}

		public int Value { get; protected set; }

		string name;
		public string Name { 
			get{ 
				return name ?? GetAllFields ().First (p => ((Enumeration)p.GetValue(this)).Value == Value).Name;
			}
			protected set { 
				name = value;
			}
		}

		public IEnumerable<FieldInfo> GetAllFields()
		{
			var type = this.GetType ();
			var fields = type.GetRuntimeFields ();
			return fields;
		}
		public FieldInfo GetField(Func<FieldInfo, bool> predicate)
		{
			return GetAllFields().First (predicate);
		}

		public static IEnumerable<T> GetAll<T> () where T : Enumeration, new()
		{
			var type = typeof(T);
			return type.GetRuntimeFields ().Select(p => (T)p.GetValue (new T()));
		}

		public override bool Equals (object obj)
		{
			var otherValue = obj as Enumeration;
			if (otherValue == null) {
				return false;
			}
			//TODO: for type match, also check parent types
			//var typeMatches = GetType ().Equals (obj.GetType ());
			var valueMatches = Value.Equals (otherValue.Value);
			return /*typeMatches &&*/ valueMatches;
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
			var matchingItem = Parse<T, int> (value, "value", item => item.Value == value);
			return matchingItem;
		}

		public static T FromDisplayName<T> (string displayName) where T : Enumeration, new()
		{
			var matchingItem = Parse<T, string> (displayName, "item name", item => item.Name == displayName);
			return matchingItem;
		}

		private static T Parse<T, TK> (TK value, string description, Func<T, bool> predicate) where T : Enumeration, new()
		{
			var matchingItem = GetAll<T> ().FirstOrDefault (predicate);
			if (matchingItem == null) {
				var message = string.Format ("'{0}' is not an existing {1} in {2}", value, description, typeof(T));
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