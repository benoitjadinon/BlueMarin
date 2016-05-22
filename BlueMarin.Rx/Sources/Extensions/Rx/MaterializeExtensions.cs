using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;

namespace BlueMarin.Rx
{
	public static class MaterializeExtensions
	{
		//http://stackoverflow.com/questions/6052788/handling-errors-in-an-observable-sequence-using-rx
		public static IObservable<Notification<R>> Materialize<T, R>(
			this IObservable<T> source, Func<T, R> selector)
		{
			return source.Select(t => Notification.CreateOnNext(t)).Materialize(selector);
		}

		//http://stackoverflow.com/questions/6052788/handling-errors-in-an-observable-sequence-using-rx
		public static IObservable<Notification<R>> Materialize<T, R>(
			this IObservable<Notification<T>> source, Func<T, R> selector)
		{
			Func<Notification<T>, Notification<R>> f = nt =>
			{
				if (nt.Kind == NotificationKind.OnNext)
				{
					try
					{
						return Notification.CreateOnNext<R>(selector(nt.Value));
					}
					catch (Exception ex)
					{
						ex.Data["Value"] = nt.Value;
						ex.Data["Selector"] = selector;
						return Notification.CreateOnError<R>(ex);
					}
				}
				else
				{
					if (nt.Kind == NotificationKind.OnError)
					{
						return Notification.CreateOnError<R>(nt.Exception);
					}
					else
					{
						return Notification.CreateOnCompleted<R>();
					}
				}
			};
			return source.Select(nt => f(nt));
		}

		//http://leecampbell.blogspot.be/2010/05/rx-part-4-flow-control.html
		public static IObservable<T> Log<T>(this IObservable<T> stream)
		{
			return stream.Materialize ()
				.Do (d => Debug.WriteLine(d))
				.Dematerialize();
		}
	}
}