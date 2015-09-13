using System;

namespace XamarinTools
{
	public class AuthException : Exception
	{
		public AuthException (string message = null) : base(message)
		{
		}

		public override string Message {
			get {
				return base.Message ?? "mauvais login / mot de passe";
			}
		}
	}
}

