using System;

namespace BlueMarin
{
	public static class DelegateExtensions
	{
		// http://mikehadlow.blogspot.be/2015/09/partial-application-in-c.html
		// https://en.wikipedia.org/wiki/Currying
		public static Func<A, Func<B, C>> Curry <A, B, C>(this Func<A, B, C> func) => a => b => func(a, b);
		public static Func<A, Func<B, Func<C, D>>> Curry <A, B, C, D>(this Func<A, B, C, D> func) => a => b => c => func(a, b, c);
		public static Func<A, Func<B, Func<C, Func<D, E>>>> Curry <A, B, C, D, E>(this Func<A, B, C, D, E> func) => a => b => c => d => func(a, b, c, d);
	}
}

