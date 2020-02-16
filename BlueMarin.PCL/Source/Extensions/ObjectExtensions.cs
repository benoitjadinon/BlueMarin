namespace BlueMarin
{
	public static class ObjectExtensions
	{
		//https://gist.github.com/kpespisa/21c31c712ace6d7c19e0
		public static string ToSafeString(this object obj)
		{
			if (obj == null)
				return string.Empty;
			
			return obj.ToString();
		}
	}
}

