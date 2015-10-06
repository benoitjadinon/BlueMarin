using System;

namespace BlueMarin
{
	public class DisposableBoolean : IDisposable
	{
		readonly Action<bool> getter;
		readonly Func<bool, bool> getterSetter;

		readonly bool initialValue = true;

		public DisposableBoolean (Action<bool> getter, bool initialValue = true)
		{
			this.initialValue = initialValue;
			this.getter = getter;
			getter (initialValue);
		}

		public DisposableBoolean (Func<bool, bool> getterSetter, bool initialValue = true)
		{
			this.initialValue = initialValue;
			this.getterSetter = getterSetter;
			getterSetter (initialValue);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			getterSetter?.Invoke (!initialValue);
			getter?.Invoke (!initialValue);
		}

		#endregion
	}
}

