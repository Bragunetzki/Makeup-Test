using System;
using UnityEngine;

namespace Core.Scripts.Tools.Config
{
    [Serializable]
    public class StringColorPair
    {
        [SerializeField] private string _key;
        [SerializeField] private Color _color;

        public string Key => _key;
        public Color Color => _color;
    }
}