using System.Linq;

namespace UniTools.Extensions
{
    public static partial class StringExtensions
    {
        public static string Remove(this string self, params string[] remove)
        {
            return remove.Aggregate(self, (current, t) => current.Replace(t, string.Empty));
        }

        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        public static bool IsNotNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self) is false;
        }
    }
}