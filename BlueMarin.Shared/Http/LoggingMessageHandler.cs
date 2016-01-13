using System;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;

namespace BlueMarin
{
	// log showing twice in XS : https://forums.xamarin.com/discussion/32095/applications-output-shows-debug-writeline-twice
	public class LoggingMessageHandler : HttpClientHandler
	{
		const int MaxBodyLength = 150;

		protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
		{
			await PrintRequest (request);

			var response = await base.SendAsync (request, cancellationToken);

			await PrintResponse (response);

			return response;
		}

		public static async Task PrintRequest(HttpRequestMessage request){
			Debug ("REQUEST:");
			Debug (request.ToString ());
			if (request.Content != null) {
				Debug (await request.Content.ReadAsStringAsync ());
			}
		}

		public static async Task PrintResponse (HttpResponseMessage response)
		{
			Debug ("RESPONSE:");
			Debug (response.ToString ());
			if (response.Content != null) {
				var respBody = await response.Content.ReadAsStringAsync ();
				Debug (respBody.Substring (0, Math.Min (MaxBodyLength, respBody.Length)) + (respBody.Length >= MaxBodyLength ? "(...)" : ""));
			}
		}

		//https://bugzilla.xamarin.com/show_bug.cgi?id=13538
		[System.Diagnostics.Conditional("DEBUG")]
		public static void Debug(string text)
		{
			System.Diagnostics.Debug.WriteLine(text);
		}
	}
}
