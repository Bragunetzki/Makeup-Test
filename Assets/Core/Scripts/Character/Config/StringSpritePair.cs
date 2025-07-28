using System;
using UnityEngine;

namespace Core.Scripts.Character.Config
{
    [Serializable]
    public class StringSpritePair
    {
        [SerializeField] private string _key;
        [SerializeField] private Sprite _sprite;

        public string Key => _key;
        public Sprite Sprite => _sprite;
    }
}