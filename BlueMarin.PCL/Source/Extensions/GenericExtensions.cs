using System;

namespace BlueMarin
{
	public static class GenericExtensions
	{
		// http://arstechnica.com/civis/viewtopic.php?t=127188
		/* usage :
		Properties.Settings.Default
			.Chain(s => s.User = currentUser)
			.Save();
		 */
		public static T Chain<T>(this T @this, Action<T> action) 
			where T : class
		{
			if (action != null)
				action(@this);
			return @this;
		}
	}
}

