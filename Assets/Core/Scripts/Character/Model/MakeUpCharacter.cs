using Core.Scripts.Character.Config;

namespace Core.Scripts.Character.Model
{
    public class MakeUpCharacter
    {
        public CharacterComponent Eyeshadow { get; } = new();
        public CharacterComponent Lipstick { get; } = new();
        public CharacterComponent Blush { get; } = new();
        public Acne Acne { get; } = new();

        public void Init(CharacterConfig config)
        {
            Eyeshadow.Init(config.EyeshadowConfig);
            Lipstick.Init(config.LipstickConfig);
            Blush.Init(config.BlushConfig);
            
            if (config.HasAcne)
            {
                Acne.Activate();
            }
            else
            {
                Acne.Deactivate();
            }
        }
    }
}