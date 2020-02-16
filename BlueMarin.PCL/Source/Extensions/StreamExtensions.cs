using System.IO;

namespace BlueMarin
{
	public static class StreamExtensions
	{
		public static byte[] ReadFully(this Stream input)
		{
			byte[] buffer = new byte[16*1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public static byte[] AsBytes (this Stream stream)
		{
			using (MemoryStream ms = new MemoryStream ()) {
				stream.CopyTo (ms);
				return ms.ToArray ();
			}
		}
	}
}

