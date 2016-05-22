using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Reactive.Disposables;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace BlueMarin.Rx
{
	public static class RxExtensions
	{
		public static IObservable<bool> Where (this IObservable<bool> @this, bool value = true)
		{
			return @this.Where(v => v == value);
		}

		// http://kent-boogaart.com/page10/
		public static T AddTo<T>(this T @this, CompositeDisposable compositeDisposable)
			where T : IDisposable
		{
			compositeDisposable.Add(@this);
			return @this;
		}

	}
}

