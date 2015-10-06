using System;

namespace BlueMarin
{
	/*
	using (var loader = new DisposableBoolean (b => this.IsLoading = b)) 
	{
	   // do stuff async
	}
	*/
	public class DisposableBoolean : IDisposable
	{
		readonly Func<bool, bool> getterSetter;

		readonly bool initialValue = true;

		public DisposableBoolean (Action<bool> setter, bool initialValue = true)
			: this(new Func<bool, bool>(b => {
				setter?.Invoke(b);
				return b;
			}), initialValue)
		{
		}

		private DisposableBoolean (Func<bool, bool> getterSetter, bool initialValue = true)
		{
			this.initialValue = initialValue;
			this.getterSetter = getterSetter;
			getterSetter?.Invoke (initialValue);
		}

		public DisposableBoolean (Func<bool> getter, bool initialValue = true)
			: this(new Func<bool, bool>(b => {
				getter?.Invoke();
				return b;
			}), initialValue)
		{
		}

		public static DisposableBoolean Create(Action<bool> setter, bool initialValue = true) {
			return new DisposableBoolean(setter, initialValue);
		}
		public static DisposableBoolean Create(Func<bool, bool> getterSetter, bool initialValue = true) {
			return new DisposableBoolean(getterSetter, initialValue);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			getterSetter?.Invoke (!initialValue);
		}

		#endregion
	}
}

