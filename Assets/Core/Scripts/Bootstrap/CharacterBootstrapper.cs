using System;
using Core.Scripts.Character.Config;
using Core.Scripts.Character.Controller;
using Core.Scripts.Character.View;
using VContainer;
using VContainer.Unity;

namespace Core.Scripts.Bootstrap
{
    public class CharacterBootstrapper : IInitializable, IPostInitializable, IDisposable
    {
        private readonly MakeupCharacterController _characterController;
        private readonly CharacterView _characterView;
        private readonly CharacterConfig _characterConfig;

        [Inject]
        public CharacterBootstrapper(
            MakeupCharacterController characterController,
            CharacterView characterView,
            CharacterConfig characterConfig)
        {
            _characterController = characterController;
            _characterView = characterView;
            _characterConfig = characterConfig;
        }

        public void Initialize()
        {
            _characterController.Init(_characterView);
        }

        public void PostInitialize()
        {
            _characterController.PostInit(_characterConfig);
        }

        public void Dispose()
        {
            _characterController.Dispose();
        }
    }
}