using System;
using Core.Scripts.Character.Config;
using Core.Scripts.Character.Model;
using Core.Scripts.Character.View;

namespace Core.Scripts.Character.Controller
{
    public class MakeupCharacterController : IDisposable
    {
        private readonly MakeUpCharacter _character = new();
        private ICharacterView _view;
        private bool _initialized;

        public MakeUpCharacter Character => _character;

        public void Init(ICharacterView view)
        {
            if (_initialized)
            {
                return;
            }
            
            _view = view;
            Subscribe();
            _initialized = true;
        }

        public void PostInit(CharacterConfig config)
        {
            _character.Init(config);
        }

        private void Subscribe()
        {
            _character.Acne.Erased += _view.Acne.Erase;
            _character.Acne.Activated += _view.Acne.ActivateInstant;
            _character.Acne.Deactivated += _view.Acne.DeactivateInstant;
            SubscribeComponent(_character.Blush, _view.Blush);
            SubscribeComponent(_character.Eyeshadow, _view.Eyeshadow);
            SubscribeComponent(_character.Lipstick, _view.Lipstick);
        }

        private void Unsubscribe()
        {
            _character.Acne.Erased -= _view.Acne.Erase;
            _character.Acne.Activated -= _view.Acne.ActivateInstant;
            _character.Acne.Deactivated -= _view.Acne.DeactivateInstant;
            UnsubscribeComponent(_character.Blush, _view.Blush);
            UnsubscribeComponent(_character.Eyeshadow, _view.Eyeshadow);
            UnsubscribeComponent(_character.Lipstick, _view.Lipstick);
        }

        private void SubscribeComponent(CharacterComponent component, ICharacterComponentView view)
        {
            component.SpriteChanged += view.SetSprite;
            component.Enabled += view.Show;
            component.Disabled += view.Hide;
        }

        private void UnsubscribeComponent(CharacterComponent component, ICharacterComponentView view)
        {
            component.SpriteChanged -= view.SetSprite;
            component.Enabled -= view.Show;
            component.Disabled -= view.Hide;
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}