using Core.Scripts.Character.Controller;
using Core.Scripts.Character.Model;
using UnityEngine;

namespace Core.Scripts.Sponge
{
    public class SpongeController
    {
        private readonly MakeUpCharacter _character;
        private readonly SpongeView _spongeView;

        public SpongeController(MakeUpCharacter character, SpongeView spongeView)
        {
            _spongeView = spongeView;
            _character = character;
            spongeView.Init(OnSpongeClick);
        }

        private void OnSpongeClick()
        {
            _character.Eyeshadow.Disable();
            _character.Blush.Disable();
            _character.Lipstick.Disable();
        }
    }
}