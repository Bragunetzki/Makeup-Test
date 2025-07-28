using UnityEngine;

namespace Core.Scripts.Character.View
{
    public class CharacterView : MonoBehaviour, ICharacterView
    {
        [SerializeField] private CharacterComponentView _eyeshadow;
        [SerializeField] private CharacterComponentView _lipstick;
        [SerializeField] private CharacterComponentView _blush;
        [SerializeField] private CharacterAcneView _acne;

        public CharacterComponentView Eyeshadow => _eyeshadow;
        public CharacterComponentView Lipstick => _lipstick;
        public CharacterComponentView Blush => _blush;
        public CharacterAcneView Acne => _acne;
    }
}