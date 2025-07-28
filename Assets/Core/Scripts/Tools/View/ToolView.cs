using System;
using System.Collections.Generic;
using Core.Scripts.Targets.View;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Scripts.Tools.View
{
    public class ToolView :
        MonoBehaviour,
        IToolView,
        IPointerDownHandler,
        IDragHandler,
        IPointerUpHandler
    {
        public event Action UserPointerDown;
        public event Action<List<ToolTargetView>> DroppedOnTargets;

        private const float DRAG_SPEED = 10;
        private const string DEFAULT_ANIMATION_NAME = "Default";
        protected const float APPLY_ANIMATION_DURATION = 1.5f;
        protected const string APPLY_ANIMATION_NAME = "Apply";
        protected const float PICK_UP_DURATION = 0.3f;

        [SerializeField] private RectTransform _startPosition;
        [SerializeField] protected RectTransform _pickUpPosition;
        [SerializeField] protected Animator _animator;
        
        private readonly List<RaycastResult> _raycastBuffer = new(8);
        private readonly List<ToolTargetView> _targetsBuffer = new(8);
        protected RectTransform _transform;
        protected Tween _currentTween;
        protected bool _canBeDragged;
        protected Vector3 _dragTarget;
        
        public void Init()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void PlayPickUpAnimation(Action onComplete)
        {
            _canBeDragged = false;
            _dragTarget = _pickUpPosition.position;
            _currentTween?.Kill();
            _transform.SetAsLastSibling();
            _currentTween = _transform.DOMove(_pickUpPosition.position, PICK_UP_DURATION)
                .OnComplete(() =>
                {
                    _canBeDragged = true;
                    onComplete?.Invoke();
                });
        }

        public void PlayApplyAnimation(ToolTargetView targetView, Action<ToolTargetView> onComplete)
        {
            _canBeDragged = false;
            _dragTarget = targetView.ApplyPosition.position;
            _currentTween?.Kill();
            
            Sequence applySequence = DOTween.Sequence()
                .Append(_transform.DOMove(targetView.ApplyPosition.position, PICK_UP_DURATION))
                .AppendCallback(() => _animator.Play(APPLY_ANIMATION_NAME))
                .AppendInterval(APPLY_ANIMATION_DURATION)
                .OnComplete(() => onComplete?.Invoke(targetView));
            _currentTween = applySequence;
        }

        public void PlayPutDownAnimation(Action onComplete)
        {
            if (gameObject.activeInHierarchy)
            {
                _animator.Play(DEFAULT_ANIMATION_NAME);
            }

            _canBeDragged = false;
            _dragTarget = _startPosition.position;
            _currentTween?.Kill();
            _currentTween = _transform.DOMove(_startPosition.position, PICK_UP_DURATION)
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _animator.enabled = true;
            _animator.Play(DEFAULT_ANIMATION_NAME);
        }

        public void Hide()
        {
            _animator.enabled = false;
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            _currentTween?.Kill();
            _dragTarget = _startPosition.position;
            _transform.position = _startPosition.position;
            _transform.rotation = _startPosition.rotation;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UserPointerDown?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_canBeDragged)
            {
                return;
            }

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    _transform.parent as RectTransform, 
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector3 globalMousePos))
            {
                _dragTarget = globalMousePos;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_canBeDragged)
            {
                return;
            }

            _raycastBuffer.Clear();
            EventSystem.current.RaycastAll(eventData, _raycastBuffer);

            _targetsBuffer.Clear();
            foreach (RaycastResult result in _raycastBuffer)
            {
                var target = result.gameObject.GetComponent<ToolTargetView>();
                if (target == null)
                {
                    continue;
                }

                _targetsBuffer.Add(target);
            }

            if (_targetsBuffer.Count != 0)
            {
                DroppedOnTargets?.Invoke(_targetsBuffer);
            }
        }

        private void Update()
        {
            if (_canBeDragged)
            {
                float lerpFactor = 1f - Mathf.Exp(-DRAG_SPEED * Time.deltaTime);
                _transform.position = Vector3.Lerp(_transform.position, _dragTarget, lerpFactor);
            }
        }
    }
}