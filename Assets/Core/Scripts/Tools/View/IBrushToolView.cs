using System;
using Core.Scripts.Palette.View;
using UnityEngine;

namespace Core.Scripts.Tools.View
{
    public interface IBrushToolView : IToolView
    {
        void SetColor(Color color);
        void ClearColor();
        void PlayPickUpAnimation(BrushPaletteView paletteView, Action onComplete);
    }
}