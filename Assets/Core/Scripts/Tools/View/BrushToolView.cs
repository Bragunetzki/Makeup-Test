using System;
using Core.Scripts.Palette.View;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Tools.View
{
    public class BrushToolView : ToolView, IBrushToolView
    {
        [SerializeField] private Image _brushColorMask;
        
        public void SetColor(Color color)
        {
            _brushColorMask.color = color;    
        }

        public void ClearColor()
        {
            _brushColorMask.color = Color.clear;
        }

        public void PlayPickUpAnimation(BrushPaletteView paletteView, Action onComplete)
        {
            _canBeDragged = false;
            _dragTarget = _pickUpPosition.position;
            _transform.SetAsLastSibling();
            _currentTween?.Kill();
            _currentTween = DOTween.Sequence()
                .Append(_transform.DOMove(paletteView.Transform.position, PICK_UP_DURATION))
                .AppendCallback(() => _animator.Play(APPLY_ANIMATION_NAME))
                .AppendInterval(APPLY_ANIMATION_DURATION)
                .Append(_transform.DOMove(_pickUpPosition.position, PICK_UP_DURATION))
                .OnComplete(() =>
                {
                    _canBeDragged = true;
                    onComplete?.Invoke();
                });
        }
    }
}