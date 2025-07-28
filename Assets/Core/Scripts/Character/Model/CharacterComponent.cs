using System;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts.Character.Config;
using UnityEngine;

namespace Core.Scripts.Character.Model
{
    public class CharacterComponent
    {
        public event Action<Sprite> SpriteChanged;
        public event Action Enabled;
        public event Action Disabled;

        private Dictionary<string, Sprite> _sprites;
        private string _currentSpriteKey;
        private bool _initialized;

        public void Init(CharacterComponentConfig config)
        {
            _sprites = config.Sprites.ToDictionary(pair => pair.Key, pair => pair.Sprite);
            if (config.EnabledByDefault)
            {
                Enable();
            }
            else
            {
                Disable();
            }
            
            _initialized = true;
        }

        public void UpdateSprite(string spriteKey)
        {
            if (!_initialized || spriteKey == _currentSpriteKey)
            {
                return;
            }
            
            _currentSpriteKey = spriteKey;
            if (_sprites.TryGetValue(spriteKey, out Sprite sprite))
            {
                SpriteChanged?.Invoke(sprite);
                Enable();
            }
        }

        public void Enable()
        {
            if (!_initialized)
            {
                return;
            }
            
            Enabled?.Invoke();
        }

        public void Disable()
        {
            if (!_initialized)
            {
                return;
            }
            
            Disabled?.Invoke();
        }
    }
}