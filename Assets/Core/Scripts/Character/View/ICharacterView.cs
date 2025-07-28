namespace Core.Scripts.Character.View
{
    public interface ICharacterView
    {
        CharacterComponentView Eyeshadow { get; }
        CharacterComponentView Lipstick { get; }
        CharacterComponentView Blush { get; }
        CharacterAcneView Acne { get; }
    }
}