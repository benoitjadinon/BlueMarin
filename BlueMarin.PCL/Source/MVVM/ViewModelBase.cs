using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;

namespace BlueMarin
{
	public abstract class ViewModelBase : INotifyPropertyChanged //PropertyChangedBase {
	{
		/// <summary>
		/// Event for when IsBusy changes
		/// </summary>
		public event Action<bool> IsBusyChanged;

		/// <summary>
		/// Event for when IsValid changes
		/// </summary>
		public event EventHandler IsValidChanged;

		readonly List<string> errors = new List<string> ();
		bool isBusy = false;

		/// <summary>
		/// Default constructor
		/// </summary>
		public ViewModelBase ()
		{
			//Make sure validation is performed on startup
			Validate ();
		}

		/// <summary>
		/// Returns true if the current state of the ViewModel is valid
		/// </summary>
		public bool IsValid
		{
			get { return errors.Count == 0; }
		}

		/// <summary>
		/// A list of errors if IsValid is false
		/// </summary>
		protected List<string> Errors
		{
			get { return errors; }
		}

		/// <summary>
		/// An aggregated error message
		/// </summary>
		public virtual string Error
		{
			get
			{
				return errors.Aggregate (new StringBuilder (), (b, s) => b.AppendLine (s)).ToString ().Trim ();
			}
		}

		/// <summary>
		/// Protected method for validating the ViewModel
		/// - Fires PropertyChanged for IsValid and Errors
		/// </summary>
		protected virtual void Validate ()
		{
			OnPropertyChanged ("IsValid");
			OnPropertyChanged ("Errors");

			var method = IsValidChanged;
			if (method != null)
				method (this, EventArgs.Empty);
		}

		/// <summary>
		/// Other viewmodels should call this when overriding Validate, to validate each property
		/// </summary>
		/// <param name="validate">Func to determine if a value is valid</param>
		/// <param name="error">The error message to use if not valid</param>
		protected virtual void ValidateProperty (Func<bool> validate, string error)
		{
			if (validate ()) {
				if (!Errors.Contains (error))
					Errors.Add (error);
			} else {
				Errors.Remove (error);
			}
		}

		/// <summary>
		/// Value inidicating if a spinner should be shown
		/// </summary>
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				if (isBusy != value) {
					isBusy = value;

					OnPropertyChanged ("IsBusy");
					OnIsBusyChanged (isBusy);

					SetLoadingState (value);
				}
			}
		}

		/// <summary>
		/// Other viewmodels can override this if something should be done when busy
		/// </summary>
		protected virtual void OnIsBusyChanged (bool state)
		{
			var method = IsBusyChanged;
			if (method != null)
				IsBusyChanged (state);
		}

		public event PropertyChangedEventHandler PropertyChanged = (sender, args) => { };

		protected void RaiseChanged<TProperty>(Expression<Func<TProperty>> propertyExpresion)
		{
			var property = propertyExpresion.Body as MemberExpression;
			if (property == null || !(property.Member is PropertyInfo) ||
				!IsPropertyOfThis(property))
			{
				throw new ArgumentException(string.Format(
					CultureInfo.CurrentCulture,
					"Expression must be of the form 'this.PropertyName'. Invalid expression '{0}'.",
					propertyExpresion), "propertyExpression");
			}

			this.OnPropertyChanged(property.Member.Name);
		}

		private bool IsPropertyOfThis(MemberExpression property)
		{
			var constant = RemoveCast(property.Expression) as ConstantExpression;
			return constant != null && constant.Value == this;
		}

		private Expression RemoveCast(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Convert ||
				expression.NodeType == ExpressionType.ConvertChecked)
				return ((UnaryExpression)expression).Operand;

			return expression;
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void SetLoadingState (bool value){
		}
	}
}

