using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace BlueMarin.Android
{
	public class ActivityLifecycleCallbacks : Java.Lang.Object, Application.IActivityLifecycleCallbacks
	{
		public ActivityLifecycleCallbacks (Application application, Activity activity = null)
		{
			this.CurrentApplication = application;
			application.RegisterActivityLifecycleCallbacks (this);
			if (activity != null)
				CurrentTopActivity = activity;
		}


		public Application CurrentApplication { get; protected set; }

		public Activity CurrentTopActivity { get; protected set; }

		public Context CurrentContext {
			get {
				return CurrentTopActivity ?? CurrentApplication?.ApplicationContext;
			}
		}

		ActivityState CurrentActivityState = ActivityState.None;

		#region Application.IActivityLifecycleCallbacks

		public virtual void OnActivityCreated (Activity activity, Bundle savedInstanceState)
		{
			OnActivityCreatedOrResumed (activity, ActivityState.Created);
			CurrentActivityState = ActivityState.Created;
		}

		public virtual void OnActivityResumed (Activity activity)
		{
			OnActivityCreatedOrResumed (activity, ActivityState.Resumed);
			CurrentActivityState = ActivityState.Resumed;
		}

		public virtual void OnActivityPaused (Activity activity)
		{
			CurrentActivityState = ActivityState.Paused;
		}

		public virtual void OnActivityDestroyed (Activity activity)
		{
			CurrentActivityState = ActivityState.Destroyed;
		}

		public virtual void OnActivitySaveInstanceState (Activity activity, Bundle outState)
		{
			CurrentActivityState = ActivityState.SaveInstanceState;
		}

		public virtual void OnActivityStarted (Activity activity)
		{
			CurrentActivityState = ActivityState.Started;
		}

		public virtual void OnActivityStopped (Activity activity)
		{
			CurrentActivityState = ActivityState.Stopped;
		}

		#endregion Application.IActivityLifecycleCallbacks


		public virtual void OnActivityCreatedOrResumed (Activity activity, ActivityState state)
		{
			CurrentTopActivity = activity;
		}
	}

	public enum ActivityState
	{
		None,
		Created,
		Resumed,
		Paused,
		Destroyed,
		SaveInstanceState,
		Started,
		Stopped,
	}
}

