﻿using UnityEngine;

namespace UniTools.Extensions
{
    public static partial class StringExtensions
    {
        public static string ToBold(this string self)
        {
            return WrapInTag(self, "b", "b");
        }

        public static string ToItalic(this string self)
        {
            return WrapInTag(self, "i", "i");
        }
        
        public static string ToStrikethrough(this string self)
        {
            return WrapInTag(self, "strikethrough", "strikethrough");
        }
        
        public static string ToUnderlined(this string self)
        {
            return WrapInTag(self, "underline", "underline");
        }
        
        public static string ToSubscript(this string self)
        {
            return WrapInTag(self, "sub", "sub");
        }
        
        public static string ToSuperscript(this string self)
        {
            return WrapInTag(self, "sup", "sup");
        }

        public static string Highlight(this string self, string xmlColor = "yellow")
        {
            return WrapInTag(self, $"color={xmlColor}", "color");
        }
        
        public static string Highlight(this string self, Color color)
        {
            return self.Highlight("#" + ColorUtility.ToHtmlStringRGB(color));
        }

        private static string WrapInTag(string self, string openTag, string closeTag)
        {
            if (string.IsNullOrEmpty(self) is false) return $"<{openTag}>{self}</{closeTag}>";
            
            Debug.LogWarning($"String is not defined. Can't wrap in tags: {openTag} - {closeTag}");
            return self;
        }
    }
}