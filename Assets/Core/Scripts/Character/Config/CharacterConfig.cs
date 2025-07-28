using UnityEngine;

namespace Core.Scripts.Character.Config
{
    [CreateAssetMenu(fileName = "Character Config", menuName = "Makeup/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private CharacterComponentConfig _eyeshadowConfig; 
        [SerializeField] private CharacterComponentConfig _lipstickConfig;
        [SerializeField] private CharacterComponentConfig _blushConfig;
        [SerializeField] private bool _hasAcne;
        
        public CharacterComponentConfig EyeshadowConfig => _eyeshadowConfig;
        public CharacterComponentConfig LipstickConfig => _lipstickConfig;
        public CharacterComponentConfig BlushConfig => _blushConfig;
        public bool HasAcne => _hasAcne;
    }
}