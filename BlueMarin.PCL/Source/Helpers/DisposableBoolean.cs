using System;

namespace BlueMarin
{
	public class DisposableBoolean : IDisposable
	{
		readonly Action<bool> action;

		public DisposableBoolean (Action<bool> action)
		{
			this.action = action;
			action (true);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			action (false);
		}

		#endregion
	}
}

