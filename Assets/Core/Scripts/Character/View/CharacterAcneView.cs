using Core.Scripts.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Character.View
{
    public class CharacterAcneView : MonoBehaviour, ICharacterAcneView
    {
        private const float FADE_DURATION = 0.5f;
        
        [SerializeField] private Image _image;

        private Tween _currentTween;
        
        public void Show()
        {
            _currentTween?.Kill();
            _image.DOFade(1, FADE_DURATION);
        }

        public void ActivateInstant()
        {
            _currentTween?.Kill();
            _image.color = ColorUtils.MakeOpaque(_image.color);
        }

        public void Erase()
        {
            _currentTween?.Kill();
            _image.DOFade(0, FADE_DURATION);
        }

        public void DeactivateInstant()
        {
            _currentTween?.Kill();
            _image.color = ColorUtils.MakeTransparent(_image.color);
        }
    }
}