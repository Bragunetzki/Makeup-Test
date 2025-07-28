using System;
using Core.Scripts.Targets.Model;

namespace Core.Scripts.Tools.Model
{
    public class LipstickTool : ITool
    {
        public event Action Enabled;
        public event Action Disabled;
        public event Action PutDownStarted;

        private const string SWITCH_KEY = "lipstick";
        private readonly string _lipstickKey;

        private bool _pickedUp;
        private bool _pickUpBlocked;

        public bool IsSwitchable => true;
        public string SwitchKey => SWITCH_KEY;

        public LipstickTool(string lipstickKey)
        {
            _lipstickKey = lipstickKey;
        }

        public bool CanApplyTo(FaceTarget target)
        {
            return false;
        }

        public bool CanApplyTo(MouthTarget target)
        {
            return true;
        }

        public void ApplyTo(FaceTarget target)
        {
            // do nothing.
        }

        public void ApplyTo(MouthTarget target)
        {
            target.SetLipstick(_lipstickKey);
            StartPutDown();
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