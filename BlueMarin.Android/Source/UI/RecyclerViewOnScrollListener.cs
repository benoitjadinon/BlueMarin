using System;
using Android.Support.V7.Widget;

namespace BlueMarin.Android
{
	//https://gist.github.com/martijn00/a45a238c5452a273e602
	public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
	{
		public delegate void LoadMoreEventHandler(RecyclerViewOnScrollListener sender, int total);
		public event LoadMoreEventHandler LoadMoreEvent;

		private readonly LinearLayoutManager LayoutManager;

		public RecyclerViewOnScrollListener (LinearLayoutManager layoutManager)
		{
			LayoutManager = layoutManager;
		}

		public override void OnScrolled (RecyclerView recyclerView, int dx, int dy)
		{
			base.OnScrolled (recyclerView, dx, dy);

			var visibleItemCount = recyclerView.ChildCount;
			var totalItemCount = recyclerView.GetAdapter().ItemCount;
			var pastVisiblesItems = LayoutManager.FindFirstVisibleItemPosition();

			if ((visibleItemCount + pastVisiblesItems) >= totalItemCount) {
				LoadMoreEvent (this, totalItemCount);
			}
		}
	}
}

