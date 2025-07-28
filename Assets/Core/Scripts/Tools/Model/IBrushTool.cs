using System;
using UnityEngine;

namespace Core.Scripts.Tools.Model
{
    public interface IBrushTool : ITool
    {
        event Action<Color> ColorChanged;
        event Action ColorCleared;
        
        public string CurrentPaletteKey { get; set; }
        public Color CurrentColor { get; set; }

        public void ClearColor();
    }
}