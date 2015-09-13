using System;
using System.Threading.Tasks;

namespace BlueMarin
{
	public interface IInitializable
	{
		Task InitAsync ();
	}
}

