using System;
using System.Reactive.Linq;

namespace BlueMarin.Rx
{
	public static class RxExtensions
	{
		public static IObservable<bool> Where (this IObservable<bool> @this, bool value = true)
		{
			return @this.Where(v => v == value);
		}
	}
}

