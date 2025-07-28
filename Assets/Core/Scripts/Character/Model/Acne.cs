using System;

namespace Core.Scripts.Character.Model
{
    public class Acne
    {
        public event Action Activated;
        public event Action Deactivated;
        public event Action Erased;
        
        private bool _active;

        public bool Active => _active;
        
        public void Activate()
        {
            _active = true;
            Activated?.Invoke();
        }

        public void Deactivate()
        {
            _active = false;
            Deactivated?.Invoke();
        }

        public void Erase()
        {
            if (!_active)
            {
                return;
            }
            
            _active = false;
            Erased?.Invoke();
        }
    }
}