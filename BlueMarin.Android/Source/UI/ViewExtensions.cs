using System;
using Android.Views;

namespace BlueMarin.Android
{
	public static class ViewExtensions
	{
		public static void VisibilitySwap (this View @view, bool preferGoneToInvisible = true)
		{
			if (@view.Visibility == ViewStates.Visible)
				@view.Visibility = (preferGoneToInvisible) ? ViewStates.Gone : ViewStates.Invisible;
			else
				@view.Visibility = ViewStates.Visible;
		}

		//usage : view.FindViewHolderById<TextView> (Resource.Id.text1).Text = "blah";
		public static T FindViewHolderById<T> (this View @this, int resID)
			where T : View
		{
			return (@this.GetTag (resID) as T) ?? @this.SetTagAndReturnValue (resID, @this.FindViewById<T> (resID)) as T;
		}

		public static T SetTagAndReturnValue<T> (this T @this, int key, T view)
			where T : View
		{
			@this.SetTag (key, view);
			return view;
		}

		public static T SetTagAndReturnContainer<T> (this T @this, int key, Java.Lang.Object @object)
			where T : View
		{
			@this.SetTag (key, @object);
			return @this;
		}
	}
}

