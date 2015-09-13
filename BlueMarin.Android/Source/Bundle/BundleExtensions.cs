using System;
using Android.OS;
using System.Collections.Generic;
using System.Linq.Expressions;
using Java.Lang;
using Android.Runtime;
using Android.Util;
using Newtonsoft.Json;

namespace BlueMarin.Android
{
	public static class BundleExtensions
	{
		const string SERIALIZED_KEY = "__JSONSERIALIZED__";

		static List<IMapItem> typesMapping;

		//TODO: put in order of preference (cf string/charseq , serializable/parcelable, ...) because the for loop will stop at the first recognized type
		static BundleExtensions() {
			typesMapping = new List<IMapItem>{
				new TypeMap<IBinder>              ((b, i, v) => b.PutBinder(i, v),                (b, i) => b.GetBinder(i)),
				new TypeMap<bool>                 ((b, i, v) => b.PutBoolean(i, v),               (b, i) => b.GetBoolean(i)),
				new TypeMap<bool[]>               ((b, i, v) => b.PutBooleanArray(i, v),          (b, i) => b.GetBooleanArray(i)),
				new TypeMap<Bundle>               ((b, i, v) => b.PutBundle(i, v),                (b, i) => b.GetBundle(i)),
				new TypeMap<sbyte>                ((b, i, v) => b.PutByte(i, v),                  (b, i) => b.GetByte(i)),
				new TypeMap<byte[]>               ((b, i, v) => b.PutByteArray(i, v),             (b, i) => b.GetByteArray(i)),
				new TypeMap<char>                 ((b, i, v) => b.PutChar(i, v),                  (b, i) => b.GetChar(i)),
				new TypeMap<char[]>               ((b, i, v) => b.PutCharArray(i, v),             (b, i) => b.GetCharArray(i)),
				new TypeMap<ICharSequence>        ((b, i, v) => b.PutCharSequence(i, v),          (b, i) => CharSequence.ArrayFromStringArray(new []{ b.GetCharSequence(i) })[0]),
				new TypeMap<ICharSequence[]>      ((b, i, v) => b.PutCharSequenceArray(i, v),     (b, i) => CharSequence.ArrayFromStringArray(b.GetCharSequenceArray(i))),
				new TypeMap<IList<ICharSequence>> ((b, i, v) => b.PutCharSequenceArrayList(i, v), (b, i) => b.GetCharSequenceArrayList(i)),
				new TypeMap<double>               ((b, i, v) => b.PutDouble(i, v),                (b, i) => b.GetDouble(i)),
				new TypeMap<double[]>             ((b, i, v) => b.PutDoubleArray(i, v),           (b, i) => b.GetDoubleArray(i)),
				new TypeMap<float>                ((b, i, v) => b.PutFloat(i, v),                 (b, i) => b.GetFloat(i)),
				new TypeMap<float[]>              ((b, i, v) => b.PutFloatArray(i, v),            (b, i) => b.GetFloatArray(i)),
				new TypeMap<int>                  ((b, i, v) => b.PutInt(i, v),                   (b, i) => b.GetInt(i)),
				new TypeMap<int[]>                ((b, i, v) => b.PutIntArray(i, v),              (b, i) => b.GetIntArray(i)),
				new TypeMap<IList<Integer>>       ((b, i, v) => b.PutIntegerArrayList(i, v),      (b, i) => b.GetIntegerArrayList(i)),
				new TypeMap<long>                 ((b, i, v) => b.PutLong(i, v),                  (b, i) => b.GetLong(i)),
				new TypeMap<long[]>               ((b, i, v) => b.PutLongArray(i, v),             (b, i) => b.GetLongArray(i)),
				new TypeMap<IParcelable>          ((b, i, v) => b.PutParcelable(i, v),            (b, i) => (IParcelable)b.GetParcelable(i)),
				new TypeMap<IParcelable[]>        ((b, i, v) => b.PutParcelableArray(i, v),       (b, i) => b.GetParcelableArray(i)),
				new TypeMap<IList<IParcelable>>   ((b, i, v) => b.PutParcelableArrayList(i, v),   (b, i) => (IList<IParcelable>)b.GetParcelableArrayList(i)),
				new TypeMap<Java.IO.ISerializable>((b, i, v) => b.PutSerializable(i, v),          (b, i) => b.GetSerializable(i)),
				new TypeMap<short>                ((b, i, v) => b.PutShort(i, v),                 (b, i) => b.GetShort(i)),
				new TypeMap<short[]>              ((b, i, v) => b.PutShortArray(i, v),            (b, i) => b.GetShortArray(i)),
				new TypeMap<SparseArray>          ((b, i, v) => b.PutSparseParcelableArray(i, v), (b, i) => b.GetSparseParcelableArray(i)),
				new TypeMap<string>               ((b, i, v) => b.PutString(i, v),                (b, i) => b.GetString(i)),
				new TypeMap<string[]>             ((b, i, v) => b.PutStringArray(i, v),           (b, i) => b.GetStringArray(i)),
				new TypeMap<IList<string>>        ((b, i, v) => b.PutStringArrayList(i, v),       (b, i) => b.GetStringArrayList(i)),
			};
		}


		interface IMapItem 
		{
			void Put(Bundle b, string name, object value, out bool success);
			object Get(Bundle b, Type t, string name, out bool success);
		}

		class TypeMap<T> : IMapItem 
		{
			readonly Action<Bundle, string, T> PutExpr;
			readonly Func<Bundle, string, T> GetExpr;

			public TypeMap (Action<Bundle, string, T> put, Func<Bundle, string, T> get)
			{
				this.PutExpr = put;
				this.GetExpr = get;
			}

			#region IMapItem implementation

			public void Put (Bundle b, string key, object value, out bool success)
			{
				if (value is T) {
					PutExpr (b, key, (T)value);
					success = true;
					return;
				}
				success = false;
			}

			public object Get (Bundle b, Type t, string key, out bool success)
			{
				if (t == typeof(T)){
					var val = (T)GetExpr.Invoke(b, key);
					success = true;
					return val;
				}
				success = false;
				return null;
			}

			#endregion
		}


		public static T GetValue<T> (this Bundle bundle, string propName, T defaultValue = default(T))
		{
			if (bundle != null) {
				bool hasConverted = false;
				if (bundle.ContainsKey (propName)) {
					foreach (var listItem in typesMapping) {
						var res = listItem.Get(bundle, typeof(T), propName, out hasConverted);
						if (hasConverted)
							return (T)res;
					}
				}
				if (bundle.ContainsKey (GetSerializedPropName(propName))) {
					return JsonConvert.DeserializeObject<T>(bundle.GetString(GetSerializedPropName(propName)));
				}
			}

			return defaultValue;
		}

		public static T GetValue<T> (this Bundle bundle, Expression<Func<T>> property, T defaultValue = default(T))
		{
			return GetValue(bundle, GetExpressionName<T>(property.Body), defaultValue);
		}

		public static void FillValue<T> (this Bundle bundle, Expression<Action<T>> property, T defaultValue = default(T))
		{
			property.Compile().Invoke(GetValue(bundle, GetExpressionName<T>(property.Body), defaultValue));
		}

		public static Bundle PutValue<T> (this Bundle bundle, string propName, T value)
		{
			if (bundle == null)
				bundle = new Bundle ();

			bool hasConverted = false;

			foreach (var listItem in typesMapping) {
				listItem.Put (bundle, propName, value, out hasConverted);
				if (hasConverted) {
					return bundle;
				}
			}

			if (!hasConverted) {
				bundle.PutString(GetSerializedPropName(propName), JsonConvert.SerializeObject(value));
				throw new NotSupportedException (string.Format ("type {0} not supported for param {1}", typeof(T).Name, propName));
			}

			return bundle;
		}

		public static Bundle PutValue<T> (this Bundle bundle, Expression<Func<T>> property)
		{
			bundle.PutValue(GetExpressionName<T>(property.Body), property.Compile()());
			return bundle;
		}

		public static Bundle Fill<T> (Bundle bundle, string key, T value, Expression<Func<Bundle, Action<string, T>>> put)
		{
			put.Compile().Invoke(bundle).Invoke(key, value);
			return bundle;
		}

		static string GetSerializedPropName (string propName)
		{
			return SERIALIZED_KEY + propName;
		}


		static string GetExpressionName<T> (Expression exp)
		{
			if (exp is MemberExpression)
				return ((MemberExpression)exp).Member.Name;

			// TODO: better handling ?
			throw new InvalidCastException();
		}
	}
}

