using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Character.View
{
    public class CharacterComponentView : MonoBehaviour, ICharacterComponentView
    {
        [SerializeField] private Image _image;
        
        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.enabled = true;
        }

        public void Show()
        {
            _image.enabled = true;
        }

        public void Hide()
        {
            _image.enabled = false;
        }
    }
}