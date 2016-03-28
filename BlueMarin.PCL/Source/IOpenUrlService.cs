using System;
using System.Threading.Tasks;

namespace BlueMarin
{
	public interface IOpenUrlService
	{
		void OpenUrl (string url, string mime = null, bool extra = false);
	}
}

