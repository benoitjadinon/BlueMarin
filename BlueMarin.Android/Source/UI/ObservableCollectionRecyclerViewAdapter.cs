using System;
using Android.Support.V7.Widget;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Content;

namespace BlueMarin.Android
{
	public abstract class ObservableCollectionRecyclerViewAdapter<T, VH> : RecyclerView.Adapter
		where VH : BindableViewHolder<T>
	{
		protected readonly ObservableCollection<T> items;


		protected ObservableCollectionRecyclerViewAdapter (ObservableCollection<T> items)
		{
			this.items = items;
			this.items.CollectionChanged += this.OnCollectionChanged;
		}


		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) 
		{
			this.NotifyDataSetChanged();
		}

		private void OnItemChanged(object sender, EventArgs e) 
		{
			this.NotifyDataSetChanged();
		}

		public T this[int index] 
		{
			get { return this.items[index]; }
		}

		#region implemented abstract members of Adapter

		public override int ItemCount 
		{
			get { return this.items.Count; }
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			return OnCreate (LayoutInflater.From (parent.Context), parent, viewType);
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			(holder as VH).Bind (items.ElementAt (position), position, this.ItemCount);
		}

		#endregion

		protected abstract VH OnCreate (LayoutInflater inflater, ViewGroup parent, int viewType);


		public void UpdateList (IEnumerable<T> items)
		{
			this.items.Clear ();

			AddAll (items);
		}

		public void AddAll (IEnumerable<T> newItems)
		{
			var startAt = this.items.Count;
			var numVideosReceived = newItems.Count();

			this.items.AddAll (newItems);

			this.NotifyItemRangeInserted (startAt, numVideosReceived);
		}

		#region IDisposable

		protected override void Dispose(bool disposing) 
		{
			if (disposing) {
				this.items.CollectionChanged -= this.OnCollectionChanged;
			}

			base.Dispose(disposing);
		}

		#endregion
	}

	public abstract class BindableViewHolder<T> : RecyclerView.ViewHolder
	{
		protected BindableViewHolder (View itemView)
			:base(itemView)
		{			
		}

		public abstract void Bind (T item, int index, int totalItems);
	}
}

