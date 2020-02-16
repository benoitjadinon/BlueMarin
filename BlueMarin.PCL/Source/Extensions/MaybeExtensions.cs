using System;

namespace BlueMarin
{
	public static class Maybe
	{

		//http://www.codeproject.com/Articles/109026/Chained-null-checks-and-the-Maybe-monad

		/*
		string postCode = this.With(x => person)
                      .With(x => x.Address)
                      .With(x => x.PostCode);
		*/
		public static TResult With<TInput, TResult>(this TInput o, 
			Func<TInput, TResult> evaluator)
			where TResult : class where TInput : class
		{
			if (o == null) return null;
			return evaluator(o);
		}

		/*
		string postCode = this.With(x => person).With(x => x.Address)
                      .Return(x => x.PostCode, string.Empty);
		*/
		public static TResult Return<TInput,TResult>(this TInput o, 
			Func<TInput, TResult> evaluator, TResult failureValue) where TInput: class
		{
			if (o == null) return failureValue;
			return evaluator(o);
		}

		/*
string postCode = this.With(x => person)
    .If(x => HasMedicalRecord(x))
    .With(x => x.Address)
    .Do(x => CheckAddress(x))
    .With(x => x.PostCode)
    .Return(x => x.ToString(), "UNKNOWN");
		*/

		public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) 
			where TInput : class
		{
			if (o == null) return null;
			return evaluator(o) ? o : null;
		}

		public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator)
			where TInput : class
		{
			if (o == null) return null;
			return evaluator(o) ? null : o;
		}

		public static TInput Do<TInput>(this TInput o, Action<TInput> action) 
			where TInput: class
		{
			if (o == null) return null;
			action(o);
			return o;
		}

		/*
		public static Maybe From<T>(T value) where T : class
		{
			return new Maybe(value);
		}

		public static TResult SelectMany<TIn, TOut, TResult>(this TIn @in, Func<TIn, TOut> remainder, Func<TIn, TOut, TResult> resultSelector)
			where TIn : class
			where TOut : class
			where TResult : class 
		{
			var @out = @in != null ? remainder(@in) : null;
			return @out != null ? resultSelector(@in, @out) : null;
		}
		*/
	}

	/*
	public struct Maybe<T> where T : class
	{
		private readonly T _value;

		public Maybe(T value)
		{
			_value = value;
		}

		public Maybe Select<>(Func<T, TResult> getter) where TResult : class
		{
			return new Maybe((_value == null) ? null : getter(_value));
		}

		public TResult Select(Func getter, TResult alternative)
		{
			return (_value == null) ? alternative : getter(_value);
		}

		public void Do(Action action)
		{
			if (_value != null)
				action(_value);
		}
	}
	*/
}

