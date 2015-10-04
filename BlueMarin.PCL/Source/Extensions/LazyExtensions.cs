using System;

namespace BlueMarin
{
	public static class LazyExtensions
	{
		public static T V<T> (this Lazy<T> layz)
		{
			if (layz.IsValueCreated)
				return layz.Value;

			return default(T);
		}
	}
}

