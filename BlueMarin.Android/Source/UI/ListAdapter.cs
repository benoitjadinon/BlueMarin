using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Android.Content;
using Android.Widget;

namespace BlueMarin.Android
{
	public abstract class ListAdapter<T, VH> : ArrayAdapter<T>
		where VH : ViewHolder<T>
	{
		public IEnumerable<T> List {
			set {
				Clear ();
				AddAll (value.ToArray ());
				NotifyDataSetChanged ();
			}
		}

		//readonly Expression<Func<T, int>> uniqueIdProp;

		protected ListAdapter (Context context, int layoutId, IEnumerable<T> list = null, Expression<Func<T, int>> uniqueIdProp = null, int tvId = default(int))
			: base (context, layoutId, tvId > 0 ? tvId : global::Android.Resource.Id.Text1, list.ToList ())
		{
			//this.uniqueIdProp = uniqueIdProp;
		}

		/*
		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			if (uniqueIdProp == null)
				return default(long);
			return uniqueIdProp.Compile ().Invoke (list.ElementAt(position));
		}
			
		#endregion
		*/
	}

	public class ViewHolder<T>
	{
		public virtual void Inflate()
		{
			
		}
		public virtual void Bind(T item)
		{
			
		}
	}
}


