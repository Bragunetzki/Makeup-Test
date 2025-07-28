using UnityEngine;

namespace Core.Scripts.Utils
{
    public static class ColorUtils
    {
        public static Color MakeTransparent(Color color)
        {
            color.a = 0;
            return color;
        }
        
        public static Color MakeOpaque(Color color)
        {
            color.a = 1;
            return color;
        }
    }
}