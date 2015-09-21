using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlueMarin
{
	// ReSharper disable once CheckNamespace
	// ReSharper disable once InconsistentNaming
	public static class TODO
	{

		#if DEBUG

		// for mandatory TODOs, will break release compilation AND remind when debugging
		public static void BeforeRelease (string whatsLeftToDo = null, [CallerMemberName] string callerName = null)
		{
			RemindMe(whatsLeftToDo, callerName);
		}

		#endif

		// for non mandatory implementations, will only break when debugging
		public static void RemindMe (string whatsLeftToDo = null, [CallerMemberName] string callerName = null)
		{
			Debug.WriteLine("TODO [{0}] {1}", callerName??"Reminder", whatsLeftToDo??"");

			if (Debugger.IsAttached)
				Debugger.Break();
		}
	}
}

