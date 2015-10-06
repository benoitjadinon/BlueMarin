using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlueMarin
{
	public static class ObservableCollectionExtensions
	{
		public static ObservableCollection<T> ReplaceAll<T> (this ObservableCollection<T> @this, IEnumerable<T> list)
		{
			@this.Clear ();
			@this.AddAll (list);
			return @this;
		}

		public static ObservableCollection<T> AddAll<T> (this ObservableCollection<T> @this, IEnumerable<T> list)
		{
			foreach (T item in list) {
				@this.Add (item);
			}
			return @this;
		}
	}
}

