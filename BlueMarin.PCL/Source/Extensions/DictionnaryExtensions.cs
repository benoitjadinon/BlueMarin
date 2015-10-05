using System;
using System.Collections.Generic;

namespace BlueMarin
{
	public static class DictionnaryExtensions
	{
		// https://gist.github.com/kpespisa/c7419c9467ac619ec468
		public static TValue GetValueOrDefault<TKey, TValue> (this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue ret;
			dictionary.TryGetValue(key, out ret);
			return ret;
		}
	}
}

