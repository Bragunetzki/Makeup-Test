using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Scripts.Sponge
{
    public class SpongeView : MonoBehaviour, IPointerClickHandler
    {
        private Action _onClick;
        
        private const float SCALE_VALUE = 0.8f;
        private const float SCALE_DURATION = 0.25f;
        
        private RectTransform _transform;
        private Tween _currentTween;
        
        public void Init(Action onClick)
        {
            _transform = GetComponent<RectTransform>();
            _onClick = onClick;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_currentTween == null || !_currentTween.active)
            {
                _currentTween = _transform.DOScale(SCALE_VALUE, SCALE_DURATION)
                    .SetLoops(2, LoopType.Yoyo);
            }
            
            _onClick?.Invoke();
        }
    }
}