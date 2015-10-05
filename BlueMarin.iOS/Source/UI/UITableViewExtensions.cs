using System;
using UIKit;
using Foundation;

namespace BlueMarin.iOS
{
	public static class UITableViewExtensions
	{
		//https://gist.github.com/nirinchev/d9db62291e2d0812d995#file-uitableviewextensions-L7
		public static T DequeueReusableCell<T> (this UITableView tableView) where T : UITableViewCell
		{
			var identifier = typeof(T).Name;
			var cell = tableView.DequeueReusableCell (identifier);
			if (cell == null)
			{
				// Nib with the class name MUST exist in the name bundle
				var nib = UINib.FromName (identifier, NSBundle.MainBundle);
				tableView.RegisterNibForCellReuse (nib, identifier);

				cell = tableView.DequeueReusableCell (identifier);
			}

			return (T)cell;
		}
	}
}

