using System;
using Core.Scripts.Targets.Model;

namespace Core.Scripts.Tools.Model
{
    public class CreamTool : ITool
    {
        public event Action Enabled;
        public event Action Disabled;
        public event Action PutDownStarted;
        
        private const string KEY = "cream";

        private bool _pickedUp;
        private bool _pickUpBlocked;

        public bool IsSwitchable => false;
        public string SwitchKey => KEY;

        public bool CanApplyTo(FaceTarget target)
        {
            return true;
        }

        public bool CanApplyTo(MouthTarget target)
        {
            return false;
        }

        public void ApplyTo(FaceTarget target)
        {
            target.ClearAcne();
            StartPutDown();
        }

        public void ApplyTo(MouthTarget target)
        {
            // do nothing.
        }

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
            if (_pickedUp || _pickUpBlocked)
            {
                return false;
            }
            
            _pickedUp = true;
            return true;
        }

        public void StartPutDown()
        {
            PutDownStarted?.Invoke();
        }

        public void OnPutDownFinished()
        {
            _pickedUp = false;
        }

        public void SetPickUpBlocked(bool blocked)
        {
            _pickUpBlocked = blocked;
        }
        
        public void Reset()
        {
            _pickedUp = false;
            _pickUpBlocked = false;
        }
    }
}