using System;

namespace XamarinTools
{
	public class NotConnectedException : Exception
	{
		public NotConnectedException ():base()
		{
		}

		public override string Message {
			get {
				return base.Message ?? "Internet connection issue";
			}
		}
	}
}

