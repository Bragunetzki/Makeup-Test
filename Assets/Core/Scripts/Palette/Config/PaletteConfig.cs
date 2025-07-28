using System.Collections.Generic;
using Core.Scripts.Tools.Config;
using UnityEngine;

namespace Core.Scripts.Palette.Config
{
    [CreateAssetMenu(fileName = "PaletteConfig", menuName = "Makeup/PaletteConfig")]
    public class PaletteConfig : ScriptableObject
    {
        [SerializeField] private List<StringColorPair> _colors;
        
        public List<StringColorPair> Colors => _colors;
    }
}