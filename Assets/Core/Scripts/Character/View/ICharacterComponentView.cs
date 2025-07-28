using UnityEngine;

namespace Core.Scripts.Character.View
{
    public interface ICharacterComponentView
    {
        void SetSprite(Sprite sprite);
        void Show();
        void Hide();
    }
}