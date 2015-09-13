using System;
using System.Runtime.CompilerServices;
using Cirrious.MvvmCross.ViewModels;
using System.Linq.Expressions;
using Cirrious.CrossCore.Core;
using System.ComponentModel;

namespace BlueMarin
{
	public static class ViewModelExtensions
	{
		//TODO : rename to RaisePropertyChanged
		public static void NotifyPropertyChanged(this MvxNotifyPropertyChanged vm, [CallerMemberName] String propertyNameOrNothing = "")
		{
			vm.RaisePropertyChanged (propertyNameOrNothing);
		}

		public static void NotifyPropertyChanged<T> (this MvxNotifyPropertyChanged vm, Expression<Func<T>> property)
		{
			string propertyNameFromExpression = MvxPropertyNameExtensionMethods.GetPropertyNameFromExpression<T> (vm, property);
			vm.RaisePropertyChanged (propertyNameFromExpression);
		}

		/*
		public static void NotifyPropertyChanged(this INotifyPropertyChanged vm, [CallerMemberName] String propertyNameOrNothing = "")
		{
			var propertyChanged = vm.PropertyChanged;
			if (propertyChanged != null) {
				propertyChanged (vm, new PropertyChangedEventArgs (propertyNameOrNothing));
			}
		}
		*/
	}
}

