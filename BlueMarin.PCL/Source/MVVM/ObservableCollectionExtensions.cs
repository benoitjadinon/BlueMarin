using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlueMarin
{
	public static class ObservableCollectionExtensions
	{
		public static ObservableCollection<T> ReplaceAll<T> (this ObservableCollection<T> coll, IList<T> list)
		{
			coll.Clear ();
			coll.AddAll (list);
			return coll;
		}
		public static ObservableCollection<T> AddAll<T> (this ObservableCollection<T> coll, IList<T> list)
		{
			foreach (T item in list) {
				coll.Add (item);
			}
			return coll;
		}
	}
}

