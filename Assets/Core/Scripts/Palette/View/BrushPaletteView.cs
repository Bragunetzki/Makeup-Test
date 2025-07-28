using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Scripts.Palette.View
{
    public class BrushPaletteView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<string> Clicked;
        
        private const float SCALE_VALUE = 0.8f;
        private const float SCALE_DURATION = 0.25f;

        [SerializeField] private string _key;
        
        private RectTransform _transform;
        private Tween _currentTween;
        
        public string Key => _key;
        public RectTransform Transform => _transform;

        public void Init()
        {
            _transform = GetComponent<RectTransform>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_currentTween == null || !_currentTween.active)
            {
                _currentTween = _transform.DOScale(SCALE_VALUE, SCALE_DURATION)
                    .SetLoops(2, LoopType.Yoyo);
            }

            Clicked?.Invoke(_key);
        }
    }
}