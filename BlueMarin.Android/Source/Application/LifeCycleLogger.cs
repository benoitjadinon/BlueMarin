using Android.App;

namespace BlueMarin.Android
{
	//usage : in custom Application : this.RegisterActivityLifecycleCallbacks (new LifeCycleLogger ());
	public class LifeCycleLogger : Java.Lang.Object, global::Android.App.Application.IActivityLifecycleCallbacks
	{
		void Log (Activity activity, string action) 
		{
			global::Android.Util.Log.Info (activity.Class.SimpleName, action);
		}

		#region IActivityLifecycleCallbacks

		public void OnActivityCreated (Activity activity, global::Android.OS.Bundle savedInstanceState)
		{
			Log (activity, "Created");
		}

		public void OnActivityDestroyed (Activity activity) 
		{
			Log (activity, "Destroyed");
		}

		public void OnActivityPaused (Activity activity)
		{
			Log (activity, "Paused");
		}

		public void OnActivityResumed (Activity activity)
		{
			Log (activity, "Resumed");
		}

		public void OnActivitySaveInstanceState (Activity activity, global::Android.OS.Bundle outState) 
		{
			Log (activity, "SaveInstanceState");
		}

		public void OnActivityStarted (Activity activity) 
		{
			Log (activity, "Started");
		}

		public void OnActivityStopped (Activity activity) 
		{
			Log (activity, "Stopped");
		}

		#endregion IActivityLifecycleCallbacks
	}
}

