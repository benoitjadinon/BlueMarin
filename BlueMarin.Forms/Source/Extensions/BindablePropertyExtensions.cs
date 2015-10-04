using System;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using System.Reflection;

namespace BlueMarin
{
	/*
	public class PropertyBinding
	{
		public BindableProperty TargetProperty {get;set;}
		public Binding Binding {get;set;}

		public PropertyBinding ()
		{
		}
		public PropertyBinding (BindableProperty targetProperty, string path, BindingMode mode = BindingMode.Default, IValueConverter converter = null, object converterParameter = null, string stringFormat = null)
		{
			TargetProperty = targetProperty;
			Binding = new Binding (path, mode, converter, converterParameter, stringFormat);
		}
	}

	public class PropertyBinding<TVM> : PropertyBinding
		where TVM:INotifyPropertyChanged
	{
		public PropertyBinding ()
		{
		}
		public PropertyBinding (BindableProperty targetProperty, Expression<Func<TVM, Object>> sourceProperty, BindingMode mode = BindingMode.Default, IValueConverter converter = null, object converterParameter = null, string stringFormat = null)
		{
			TargetProperty = targetProperty;
			string path = BindablePropertyExtensions.GetPathFromSourceProperty<TVM, Object> (sourceProperty);
			Binding = new Binding (path, mode, converter, converterParameter, stringFormat);
		}
	}
	*/

	public static class BindablePropertyExtensions
	{
		/*
		public static TOUT WithBindings<TOUT, TVM>(this TOUT obj, IList<PropertyBinding<TVM>> bindings) 
			where TOUT:BindableObject 
			where TVM:INotifyPropertyChanged
		{
			if (bindings == null || bindings.Count == 0)
				return obj;

			foreach (var binding in bindings) {
				obj.SetBinding (binding.TargetProperty, binding.Binding);
			}
			return obj;
		}

		public static TOUT WithBindings<TOUT>(this TOUT obj, IList<PropertyBinding> bindings) 
			where TOUT:BindableObject 
		{
			if (bindings == null || bindings.Count == 0)
				return obj;

			foreach (var binding in bindings) {
				obj.SetBinding (binding.TargetProperty, binding.Binding.Path, binding.Binding.Mode, binding.Binding.Converter, binding.Binding.StringFormat);
			}
			return obj;
		}
		*/

		// chainable, with a string for property
		// usage : new Button().WithBinding(Button.CommandProperty, "SomeCommand"),
		public static T WithBinding<T>(this T obj, 
			BindableProperty bindableProperty, 
			string path, 
			BindingMode mode = BindingMode.Default, 
			IValueConverter converter = null, 
			object converterParameter = null, 
			string stringFormat = null
		)
			where T:BindableObject
		{
			obj.SetBinding (bindableProperty, new Binding (path, mode, converter, converterParameter, stringFormat));
			return obj;
		}

		// chainable, with a lambda for property
		// usage : new Button().WithBinding(Button.CommandProperty, (MyViewModel vm) => vm.SomeCommand),
		public static TOUT WithBinding<TOUT, TVM, TO>(this TOUT obj, 
			BindableProperty bindableProperty, 
			Expression<Func<TVM, TO>> sourceProperty, 
			BindingMode mode = BindingMode.Default, 
			IValueConverter converter = null, 
			object converterParameter = null, 
			string stringFormat = null
		)
			where TOUT:BindableObject
			where TVM:INotifyPropertyChanged
		{
			string path = GetPathFromSourceProperty<TVM, TO> (sourceProperty);
			obj.SetBinding (bindableProperty, new Binding (path, mode, converter, converterParameter, stringFormat));
			return obj;
		}

		// chainable, with a string for property
		// usage : new Button().WithBinding(Button.CommandProperty, "SomeCommand"),
		public static T WithTemplateBinding<T>(this T obj, 
			BindableProperty bindableProperty, 
			string path, 
			BindingMode mode = BindingMode.Default, 
			IValueConverter converter = null, 
			object converterParameter = null, 
			string stringFormat = null
		)
			where T:DataTemplate
		{
			obj.SetBinding (bindableProperty, new Binding (path, mode, converter, converterParameter, stringFormat));
			return obj;
		}

		// chainable, with a lambda for property
		// usage : new Button().WithBinding(Button.CommandProperty, (MyViewModel vm) => vm.SomeCommand),
		public static TOUT WithTemplateBinding<TOUT, TVM, TO>(this TOUT obj, 
			BindableProperty bindableProperty, 
			Expression<Func<TVM, TO>> sourceProperty, 
			BindingMode mode = BindingMode.Default, 
			IValueConverter converter = null, 
			object converterParameter = null, 
			string stringFormat = null
		)
			where TOUT:DataTemplate
			//where TVM:INotifyPropertyChanged
		{
			string path = GetPathFromSourceProperty<TVM, TO> (sourceProperty);
			obj.SetBinding (bindableProperty, new Binding (path, mode, converter, converterParameter, stringFormat));
			return obj;
		}
			
		/*
		// BINDABLEOBJECT

		public static T WithBinding<T, TSource, TO> (this T self, BindableProperty targetProperty, Expression<Func<TSource, TO>> sourceProperty, BindingMode mode = BindingMode.Default, IValueConverter converter = null, string stringFormat = null)
			where T:BindableObject
		{
			var body = sourceProperty.Body.ToString ();
			string name;
			if (body.Contains ("."))
				name = body.Substring (body.IndexOf (".", StringComparison.Ordinal) + 1);
			else
				name = body;

			self.SetBinding(targetProperty, name, mode, converter, stringFormat);
			return self;
		}

		public static T WithBinding<T> (this T self, BindableProperty targetProperty, string path, BindingMode mode = BindingMode.Default, IValueConverter converter = null, object converterParameter = null, string stringFormat = null)
			where T:BindableObject
		{
			self.SetBinding (targetProperty, new Binding(path, mode, converter, converterParameter, stringFormat));
			return self;
		}

		//DATA TEMPLATE

		public static T WithBinding<T, TSource> (this T self, BindableProperty targetProperty, Expression<Func<TSource, object>> sourceProperty)
			where T:DataTemplate
		{
			var body = sourceProperty.Body.ToString ();
			string name;
			if (body.Contains ("."))
				name = body.Substring (body.IndexOf (".", StringComparison.Ordinal) + 1);
			else
				name = body;

			self.SetBinding(targetProperty, name);
			return self;
		}
		*/
		/*public static T WithBinding<T> (this T self, BindableProperty targetProperty, string path)
			where T:DataTemplate
		{
			self.SetBinding (targetProperty, path);
			return self;
		}*/

		public static string GetPathFromSourceProperty<TVM, TO> (Expression<Func<TVM, TO>> sourceProperty)
		{
			var body = sourceProperty.Body.ToString ();
			string name;
			if (body.Contains ("."))
				name = body.Substring (body.IndexOf (".", StringComparison.Ordinal) + 1);
			else
				name = body;
			return name;
		}
	}
}