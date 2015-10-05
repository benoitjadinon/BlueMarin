using System;
using System.Threading.Tasks;

namespace BlueMarin.iOS
{
	//this fixes the following compilation issue
	// Error CS0012: The type `System.Threading.Tasks.Task' is defined in an assembly that is not referenced. Consider adding a reference to assembly `System.Threading.Tasks
	// when there's async Tasks stuff in the PCL lib but none yet in the project itself
	public class EmptyClass
	{
		#pragma warning disable 1998
		public async Task DoNotRemove ()
		{
		}
		#pragma warning restore 1998
	}
}

