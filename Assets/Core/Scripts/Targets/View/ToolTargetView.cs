using System;
using UnityEngine;

namespace Core.Scripts.Targets.View
{
    public class ToolTargetView : MonoBehaviour
    {
        [SerializeField] private RectTransform _applyPosition;

        private Guid _id;
        
        private RectTransform _rectTransform;
        public RectTransform ApplyPosition => _applyPosition;
        public RectTransform RectTransform => _rectTransform;
        public Guid Id => _id;

        public void Init(Guid id)
        {
            _id = id;
            _rectTransform = GetComponent<RectTransform>();
        }
    }
}