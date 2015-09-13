using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;

namespace CopyResources
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			new CopyAssets (@"/Users/bja/Workspaces/Xamarin/mobilite/Mobilite.iOS/Resources/Images.xcassets", @"/Users/bja/Workspaces/Xamarin/mobilite/Mobilite.Android/Resources").Copy ();
		}
	}

	class CopyAssets
	{
		string sourcePath;
		string destinPath;

		public CopyAssets (string source, string dest)
		{
			this.sourcePath = source;
			this.destinPath = dest;
		}

		public void Copy()
		{
			Copy (GuessType (sourcePath), GuessType (destinPath));
		}
		public void Copy(IResProvider sourceType, IResProvider destType)
		{
			sourceType.GetImages ().ForEach (fg => fg.Name);
		}

		protected IResProvider GuessType(string path){
			if (path.EndsWith (".xcassets"))
				return new IOSXCAssets (path);
			if (path.EndsWith ("Resources"))
				return new AndroidRessources (path);
		}
	}

	interface IResProvider
	{
		List<FileGroup> GetImages();
		List<FileGroup> GetIcons();
	}

	class IOSXCAssets : IResProvider
	{
		DirectoryInfo dir;

		public IOSXCAssets (string root)
		{
			dir = new DirectoryInfo (root)
		}

		#region IResProvider implementation

		public List<FileGroup> GetFiles ()
		{
			return dir.GetDirectories ("*.appiconset").Select (p => new FileGroup (p.Name, p.FullName));
		}
		public List<FileGroup> GetIcons ()
		{
			return dir.GetDirectories ("*.imageset").Select (p => new FileGroup (p.Name, p.FullName));
		}
		#endregion
	}

	class AndroidRessources : IResProvider
	{
		DirectoryInfo dir;

		public AndroidRessources (string root)
		{
			dir = new DirectoryInfo (root);
		}

		#region IResProvider implementation

		public List<FileGroup> GetImages ()
		{
			dir.GetDirectories("drawable")
		}

		public List<FileGroup> GetIcons ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}

	abstract class FileGroup
	{
		public string Name { get; set; }
		public string Path { get; set; }

		public FileGroup (string name, string path)
		{
			this.Name = name;
			this.Path = path;
		}

		public virtual abstract GetFileForResolution(float multiplier);
	}
}
