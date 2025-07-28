using System;
using Core.Scripts.Targets.Model;
using UnityEngine;

namespace Core.Scripts.Tools.Model.Brushes
{
    public abstract class AbstractBrushTool : IBrushTool
    {
        public event Action Enabled;
        public event Action Disabled;
        public event Action PutDownStarted;
        public event Action<Color> ColorChanged;
        public event Action ColorCleared;
        
        private bool _pickUpBlocked;
        private Color _currentColor;

        public string CurrentPaletteKey { get; set; }
        public bool IsSwitchable => true;
        public abstract string SwitchKey { get; }

        public Color CurrentColor
        {
            get => _currentColor;
            set
            {
                _currentColor = value;
                ColorChanged?.Invoke(_currentColor);
            }
        }

        public void ClearColor()
        {
            _currentColor = default;
            CurrentPaletteKey = null;
            ColorCleared?.Invoke();
        }

        public abstract bool CanApplyTo(FaceTarget target);
        public abstract bool CanApplyTo(MouthTarget target);

        public abstract void ApplyTo(FaceTarget target);
        public abstract void ApplyTo(MouthTarget target);

        public void Enable()
        {
            Enabled?.Invoke();
        }

        public void Disable()
        {
            Disabled?.Invoke();
        }

        public bool TryPickUp()
        {
            return !_pickUpBlocked;
        }

        public void StartPutDown()
        {
            PutDownStarted?.Invoke();
        }

        public void OnPutDownFinished()
        {
            SetPickUpBlocked(false);
        }

        public void SetPickUpBlocked(bool blocked)
        {
            _pickUpBlocked = blocked;
        }

        public void Reset()
        {
            _pickUpBlocked = false;
            ClearColor();
        }
    }
}