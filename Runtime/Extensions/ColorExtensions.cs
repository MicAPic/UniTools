using UnityEngine;

namespace UniTools.Extensions
{
    public static class ColorExtensions
    {
        public static Color Multiply(this Color color, float multiplier)
        {
            return new Color(color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a);
        }
    }
}