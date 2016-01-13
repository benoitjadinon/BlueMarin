using System;

namespace BlueMarin
{
	public static class ExceptionExtensions
	{
		public static string Info (this Exception ex, bool showStack = false)
		{
			if (ex == null) 
				return "exception is NULL !";

			string info = "";

			if (ex.Message.IsEmpty ())
				info += "exception message is empty\n";
			else
				info += ex.Message.Substring (0, Math.Min(ex.Message.Length, 150));

			if (showStack && !ex.StackTrace.IsEmpty ())
				info += ex.StackTrace.Substring (0, Math.Min (ex.StackTrace.Length, 150));

			return info;
		}
	}
}

