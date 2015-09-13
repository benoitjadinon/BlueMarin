using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BlueMarin
{
	public static class INotifyPropertyChangedExtensions
	{
		public static void Raise(this PropertyChangedEventHandler handler, object sender, string propertyName)
		{
			if (handler != null)
			{
				handler(sender, new PropertyChangedEventArgs(propertyName));
			}
		}
	
		/*
		 * 
		 * http://stackoverflow.com/questions/4925106/why-cant-i-invoke-propertychanged-event-from-an-extension-method
		 * 
		public static virtual void NotifyPropertyChanged(this INotifyPropertyChanged vm, [CallerMemberName] String propertyNameOrNothing = "")
		{
			InternalNotifyPropertyChanged (vm, propertyNameOrNothing);
		}

		public static virtual void NotifyPropertyChanged<T> (this INotifyPropertyChanged vm, Expression<Func<T>> property)
		{
			InternalNotifyPropertyChanged (vm, property.Name);
		}

		protected static virtual void InternalNotifyPropertyChanged(INotifyPropertyChanged vm, string propertyName)
		{
			if (vm.PropertyChanged != null)
			{
				vm.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		*/
		// TODO : finish this :
		/*
		// http://blog.tonysneed.com/2011/01/20/mvvm-simple-is-beautiful/
		protected static virtual void AssociateProperties <TModelResult, TViewModelResult>
		(this INotifyPropertyChanged vm, 
		 Expression<Func<TModel, TModelResult>> modelProperty,
		 Expression<Func<TViewModel, TViewModelResult>> viewModelProperty)
		{
			// Convert expressions to a property names
			string modelPropertyName = ((MemberExpression)modelProperty.Body).Member.Name;
			string viewModelPropertyName = ((MemberExpression)viewModelProperty.Body).Member.Name;

			// Propagate model to view-model property change
			vm.PropertyChanged += (s, ea) =>
			{
				if (ea.PropertyName == modelPropertyName)
				{
					InternalNotifyPropertyChanged(vm, viewModelPropertyName);
				}
			};
		}
		*/
	}
}

