using System;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts.Palette.Config;
using Core.Scripts.Palette.View;
using UnityEngine;

namespace Core.Scripts.Palette
{
    public class ToolPalette
    {
        private readonly Dictionary<string, BrushPaletteView> _paletteViews;
        private readonly Dictionary<string, Color> _paletteColors;
        private readonly GameObject _palettesParent;

        public ToolPalette(
            PaletteConfig paletteConfig,
            GameObject palettesParent,
            List<BrushPaletteView> paletteViews)
        {
            _palettesParent = palettesParent;
            _paletteViews = paletteViews.ToDictionary(view => view.Key);
            _paletteColors = paletteConfig.Colors.ToDictionary(pair => pair.Key, pair => pair.Color);
        }

        public void Init()
        {
            foreach (BrushPaletteView paletteView in _paletteViews.Values)
            {
                paletteView.Init();
            }
        }

        public void SubscribeOnClick(Action<string> listener)
        {
            foreach (BrushPaletteView paletteView in _paletteViews.Values)
            {
                paletteView.Clicked += listener;
            }
        }

        public void UnsubscribeFromClick(Action<string> listener)
        {
            foreach (BrushPaletteView paletteView in _paletteViews.Values)
            {
                paletteView.Clicked -= listener;
            }
        }

        public void Show()
        {
            _palettesParent.SetActive(true);
        }

        public void Hide()
        {
            _palettesParent.SetActive(false);
        }

        public bool TryGetView(string key, out BrushPaletteView paletteView)
        {
            return _paletteViews.TryGetValue(key, out paletteView);
        }

        public bool TryGetValue(string key, out Color color)
        {
            return _paletteColors.TryGetValue(key, out color);
        }
    }
}