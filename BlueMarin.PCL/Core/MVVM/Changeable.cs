using System;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;

namespace XamarinTools
{
	public abstract class Changeable
	{
		public TInstance Set<TInstance, TProperty>(Expression<Func<TInstance, TProperty>> getter, TProperty value) where TInstance : Changeable
		{
			MemberExpression memberRef = null;
			if (getter.Body is UnaryExpression)
			{
				memberRef = (getter.Body as UnaryExpression).Operand as MemberExpression;
			}
			else if (getter.Body is MemberExpression)
			{
				memberRef = getter.Body as MemberExpression;
			}
			if (memberRef == null)
			{
				throw new NotImplementedException();
			}
			//Setting of new value can be made via caheable lambdas or via reflection
			/*			
            var instanceConstant = Expression.Constant(this, typeof (TInstance));
            var valueConstant = Expression.Constant(value, typeof(TProperty));
            var newMemberExpr = Expression.MakeMemberAccess(instanceConstant, newBody.Member);
            var setExpr = Expression.Assign(newMemberExpr, valueConstant);
            Expression.Lambda<Action>(setExpr).Compile()();
             */
			if (memberRef.Member is PropertyInfo)
			{
				(memberRef.Member as PropertyInfo).SetValue(this, value, null);
			}
			else 
			{
				throw new NotImplementedException();
			}
			PropertyChanged(this, new PropertyChangedEventArgs(memberRef.Member.Name) );
			return this as TInstance;
		}

		public event EventHandler<PropertyChangedEventArgs> PropertyChanged;
	}



	// This provides support for strongly-typed property changed setting with notification
	public static class ViewModelExtension
	{
		public static T SetProperty<T, TProperty>(this T observableBase, Expression<Func<T, TProperty>> expression, TProperty value) where T : Changeable
		{
			observableBase.Set<T, TProperty>(expression, value);
			return observableBase;
		}
	}
}

