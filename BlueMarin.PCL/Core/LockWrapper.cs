using System;
using System.Threading;

namespace BlueMarin
{
	/**
	 * usage :
	 *  
	 * 
		public class MyClass
		{
		    private readonly object _lockPoint = new object();

	        public IDisposable Lock()
	        {
	            return new LockWrapper(_lockPoint);
	        }
		}

		// using (MyClass.Lock()){ ... }


@see for lock with key : http://codereview.stackexchange.com/questions/58964/is-this-a-sensible-way-to-throttle-duplicate-requests-in-a-httpmodule
	 * */

	public class LockWrapper : IDisposable
	{
		private readonly object _lockPoint;

		public LockWrapper (object lockPoint)
		{
			_lockPoint = lockPoint;
			Monitor.Enter(_lockPoint);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			Monitor.Exit(_lockPoint);
		}

		#endregion
	}
}

