using System;
using System.Threading;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace BlueMarin
{
	public delegate void TimerCallback(object state);

	//https://forums.xamarin.com/discussion/17227/timer-in-portable-class-library
	public sealed class Timer : CancellationTokenSource, IDisposable
	{
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			Contract.Assert(period == -1, "This stub implementation only supports dueTime.");
			Task.Delay(dueTime, Token)
				.ContinueWith((t, s) => {
					var tuple = (Tuple<TimerCallback, object>)s;
					tuple.Item1(tuple.Item2);
				},
				Tuple.Create(callback, state), 
				CancellationToken.None,
				TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
				TaskScheduler.Default
			);
		}

		public new void Dispose() { Cancel(); }
	}
}