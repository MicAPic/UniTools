using System.Linq;
using UnityEngine;

namespace UniTools.Extensions
{
    public static class StringExtensions
    {
        public static string ToBold(this string self)
        {
            return WrapInTag(self.ToString(), "b", "b");
        }

        public static string ToItalic(this string self)
        {
            return WrapInTag(self.ToString(), "i", "i");
        }

        public static string Highlight(this string self, string xmlColor = "yellow", bool toBold = true)
        {
            return WrapInTag(toBold ? self.ToBold() : self, $"color={xmlColor}", "color");
        }


        public static string Highlight(this string self, Color color, bool toBold = true)
        {
            return self.Highlight("#" + ColorUtility.ToHtmlStringRGB(color), toBold);
        }

        private static string WrapInTag(string self, string openTag, string closeTag)
        {
            if (string.IsNullOrEmpty(self))
            {
                Debug.LogWarning($"String is not defined. Can't wrap in tags: {openTag} - {closeTag}");
                return self;
            }

            return $"<{openTag}>{self}</{closeTag}>";
        }


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