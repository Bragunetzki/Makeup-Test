using System;
using System.Collections.Generic;
using Core.Scripts.Palette.Config;
using Core.Scripts.Palette.View;
using Core.Scripts.Tools.View;
using UnityEngine;

namespace Core.Scripts.Tools.Config
{
    [Serializable]
    public class BrushToolParams
    {
        [SerializeField] private BrushToolView _toolView;
        [SerializeField] private PaletteConfig _paletteConfig;
        [SerializeField] private GameObject _palettesParent;
        [SerializeField] private List<BrushPaletteView> _paletteViews;

        public BrushToolView ToolView => _toolView;
        public PaletteConfig PaletteConfig => _paletteConfig;
        public GameObject PalettesParent => _palettesParent;
        public List<BrushPaletteView> PaletteViews => _paletteViews;
    }
}