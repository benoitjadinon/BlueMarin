using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android.Content;
using Android.App;

namespace BlueMarin.Android
{
	//https://gist.github.com/AArnott/8937612
	public abstract class BaseResultActivity : Activity
	{
		private int activityResultRegistrationCounter = 10000;

		private Dictionary<int, TaskCompletionSource<Tuple<Result, Intent>>> activityResultRegistrations = new Dictionary<int, TaskCompletionSource<Tuple<Result, Intent>>>();

		/// <summary>
		/// Starts another activity and allows awaiting on its return.
		/// </summary>
		/// <param name="intent">The intent that should be used to launch the new activity.</param>
		/// <returns>A task that completes with the finishing of the activity, providing the result of the activity.</returns>
		/// <example>
		/// var result = await this.StartActivityForResultAsync(Intent.CreateChooser(intent, "Select picture"));
		/// if (result.Item1 == Result.Ok) {
		///     // User chose a picture. Get the Uri
		///     Android.Net.Uri imageSource = result.Item2.Data;
		/// }
		/// </example>
		public Task<Tuple<Result, Intent>> StartActivityForResultAsync(Intent intent) {
			int requestCode = activityResultRegistrationCounter++;
			var completionSource = new TaskCompletionSource<Tuple<Result, Intent>>();
			this.activityResultRegistrations[requestCode] = completionSource;
			this.StartActivityForResult(intent, requestCode);
			return completionSource.Task;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
			base.OnActivityResult(requestCode, resultCode, data);

			TaskCompletionSource<Tuple<Result, Intent>> completionSource;
			if (this.activityResultRegistrations.TryGetValue(requestCode, out completionSource)) {
				this.activityResultRegistrations.Remove(requestCode);
				completionSource.SetResult(Tuple.Create(resultCode, data));
			}
		}
	}
}

