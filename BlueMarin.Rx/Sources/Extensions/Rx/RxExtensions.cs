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

		// http://stackoverflow.com/questions/18750718/get-iobservable-from-all-property-changed-events-on-t-myproperty-in-sortedlistm
		public static IObservable<T> OnAnyPropertyChanges<T> (this T source)
			where T : INotifyPropertyChanged
		{
			return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs> (
								handler => handler.Invoke,
								h => source.PropertyChanged += h,
								h => source.PropertyChanged -= h)
							.Select (_ => source);
		}

		// https://github.com/LeeCampbell/RxCookbook/blob/master/Model/PropertyChange.md
		/// <summary>
		/// Gets property information for the specified <paramref name="property"/> expression.
		/// </summary>
		/// <typeparam name="TSource">Type of the parameter in the <paramref name="property"/> expression.</typeparam>
		/// <typeparam name="TValue">Type of the property's value.</typeparam>
		/// <param name="property">The expression from which to retrieve the property information.</param>
		/// <returns>Property information for the specified expression.</returns>
		/// <exception cref="ArgumentException">The expression is not understood.</exception>
		public static PropertyInfo GetPropertyInfo<TSource, TValue> (this Expression<Func<TSource, TValue>> property)
		{
			if (property == null) {
				throw new ArgumentNullException ("property");
			}

			var body = property.Body as MemberExpression;
			if (body == null) {
				throw new ArgumentException ("Expression is not a property", "property");
			}

			var propertyInfo = body.Member as PropertyInfo;
			if (propertyInfo == null) {
				throw new ArgumentException ("Expression is not a property", "property");
			}

			return propertyInfo;
		}

		/// <summary>
		/// Returns an observable sequence of the value of a property when <paramref name="source"/> raises <seealso cref="INotifyPropertyChanged.PropertyChanged"/> for the given property.
		/// </summary>
		/// <typeparam name="T">The type of the source object. Type must implement <seealso cref="INotifyPropertyChanged"/>.</typeparam>
		/// <typeparam name="TProperty">The type of the property that is being observed.</typeparam>
		/// <param name="source">The object to observe property changes on.</param>
		/// <param name="property">An expression that describes which property to observe.</param>
		/// <returns>Returns an observable sequence of the property values as they change.</returns>
		public static IObservable<TProperty> OnPropertyChanges<T, TProperty> (this T source, Expression<Func<T, TProperty>> property)
			where T : INotifyPropertyChanged
		{
			return Observable.Create<TProperty> (o => {
				var propertyName = property.GetPropertyInfo ().Name;
				var propertySelector = property.Compile ();

				return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs> (
								handler => handler.Invoke,
								h => source.PropertyChanged += h,
								h => source.PropertyChanged -= h)
							.Where (e => e.EventArgs.PropertyName == propertyName)
							.Select (e => propertySelector (source))
							.Subscribe (o);
			});
		}

		public static IObservable<PropertyChangedEventArgs> OnPropertyChanges<T> (this T source)
			where T : INotifyPropertyChanged
		{
			return Observable.Create<PropertyChangedEventArgs> (observer => {
				PropertyChangedEventHandler handler = (s, e) => observer.OnNext (e);
				source.PropertyChanged += handler;
				return Disposable.Create (() => source.PropertyChanged -= handler);
			});
		}
	}
}

