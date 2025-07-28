using System;

namespace Core.Scripts.Tools.Model
{
    public interface ITool : IToolVisitor
    {
        event Action Enabled;
        event Action Disabled;
        event Action PutDownStarted;

        public bool IsSwitchable { get; }
        public string SwitchKey { get; }
        
        void Enable();
        void Disable();
        bool TryPickUp();
        void StartPutDown();
        void OnPutDownFinished();
        void SetPickUpBlocked(bool blocked);
        void Reset();
    }
}