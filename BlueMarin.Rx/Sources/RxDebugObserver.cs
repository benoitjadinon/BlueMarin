using System;
using System.Diagnostics;
using System.Reactive.Linq;

namespace BlueMarin.Rx
{
	public class RxDebugObserver<T> : IObserver<T>
	{
		string id;

		public RxDebugObserver (string id = "Rx Sequence")
		{
			this.id = id;			
		}

		#region IObserver implementation

		public void OnNext (T value)
		{
			Debug.WriteLine ("{0} received ", id, value);
		}

		public void OnCompleted ()
		{
			Debug.WriteLine ("{0}", id);
		}

		public void OnError (Exception error)
		{
			Debug.WriteLine ("{0} faulted with ", id, error);
		}

		#endregion
	}
}

