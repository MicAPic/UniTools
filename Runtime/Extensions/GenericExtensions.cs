namespace UniTools.Extensions
{
    public static class GenericExtensions
	{
		public static bool IsNull<T>(this T self)
		{
			return self == null;
		}

		public static bool IsNotNull<T>(this T self)
		{
			return self != null;
		}
	}
}
