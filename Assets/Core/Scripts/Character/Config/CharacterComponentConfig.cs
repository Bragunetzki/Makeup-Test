using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Character.Config
{
    [CreateAssetMenu(fileName = "Character Component Config", menuName = "Makeup/CharacterComponentConfig")]
    public class CharacterComponentConfig : ScriptableObject
    {
        [SerializeField] private List<StringSpritePair> _sprites;
        [SerializeField] private bool _enabledByDefault;
        
        public List<StringSpritePair> Sprites => _sprites;
        public bool EnabledByDefault => _enabledByDefault;
    }
}