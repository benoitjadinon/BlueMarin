using System;
using System.Threading.Tasks;
using System.Diagnostics;
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
			Debug.WriteLine ("REQUEST:");
			Debug.WriteLine (request.ToString ());
			if (request.Content != null) {
				Debug.WriteLine (await request.Content.ReadAsStringAsync ());
			}

			var response = await base.SendAsync (request, cancellationToken);

			Debug.WriteLine ("RESPONSE:");
			Debug.WriteLine (response.ToString ());
			if (response.Content != null) {
				var respBody = await response.Content.ReadAsStringAsync ();
				Debug.WriteLine (respBody.Substring (0, Math.Min (MaxBodyLength, respBody.Length)) + (respBody.Length >= MaxBodyLength ? "(...)" : ""));
			}

			return response;
		}
	}
}

