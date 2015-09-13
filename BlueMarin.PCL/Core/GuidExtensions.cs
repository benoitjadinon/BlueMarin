using System;

namespace XamarinTools
{
	public static class GuidExtensions
	{
		const string Separator = "-";

		public static string ToShorter (this Guid guid)
		{
			if (guid.ToString() == null)
				return null;

			return guid.ToString ().Substring (0, guid.ToString ().IndexOf (Separator, StringComparison.Ordinal));
		}
	}
}

