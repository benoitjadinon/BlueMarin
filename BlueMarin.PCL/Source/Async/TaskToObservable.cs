using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace BlueMarin
{
	// https://github.com/paulcbetts/refit/blob/d5ebf1ddf6087dc4834d575d9ce4d63a12bfaf11/Refit/RequestBuilderImplementation.cs
	public class TaskToObservable<T> : IObservable<T>
	{
		readonly Func<CancellationToken, Task<T>> taskFactory;

		public TaskToObservable(Func<CancellationToken, Task<T>> taskFactory) 
		{
			this.taskFactory = taskFactory;
		}

		public IDisposable Subscribe(IObserver<T> observer)
		{
			var cts = new CancellationTokenSource();
			taskFactory(cts.Token).ContinueWith(t => {
				if (cts.IsCancellationRequested) return;

				if (t.Exception != null) {
					observer.OnError(t.Exception.InnerExceptions.First());
					return;
				}

				try {
					observer.OnNext(t.Result);
				} catch (Exception ex) {
					observer.OnError(ex);
				}

				observer.OnCompleted();
			});

			return new AnonymousDisposable(cts.Cancel);
		}
	}

	sealed class AnonymousDisposable : IDisposable
	{
		readonly Action block;

		public AnonymousDisposable(Action block)
		{
			this.block = block;
		}

		public void Dispose()
		{
			block();
		}
	}
}

