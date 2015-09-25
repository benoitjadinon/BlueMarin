using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BlueMarin
{
	public static class ExceptionWrapper
	{
		public static void IgnoreException (Action action, bool print = true)
		{
			try {
				action ();
			} catch (Exception e) {
				//ignore
				Debug.WriteLine (e);
			}
		}

		public static T IgnoreException<T> (Func<T> func, bool print = true)
		{
			try {
				return func ();
			} catch (Exception e) {
				//ignore
				if (print)
					Debug.WriteLine (e);
			}
			return default(T);
		}

		public static async Task IgnoreExceptionAsync (Func<Task> func, bool print = true)
		{
			try {
				await func ();
			} catch (Exception e) {
				//ignore
				if (print)
					Debug.WriteLine (e);
			}
		}

		public static async Task<T> IgnoreExceptionAsync<T> (Func<Task<T>> func, bool print = true)
		{
			try {
				return await func ();
			} catch (Exception e) {
				//ignore
				if (print)
					Debug.WriteLine (e);
			}
			return default(T);
		}
	}
}

