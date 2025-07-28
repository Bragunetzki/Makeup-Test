using System;
using Core.Scripts.Character.Model;
using Core.Scripts.Tools.Model;

namespace Core.Scripts.Targets.Model
{
    public class FaceTarget : IToolTarget
    {
        private MakeUpCharacter _character;
        public Guid Id { get; private set; }

        public void Init(Guid id, MakeUpCharacter character)
        {
            Id = id;
            _character = character;
        }

        public void ClearAcne()
        {
            _character.Acne.Deactivate();
        }

        public void SetBlush(string key)
        {
            _character.Blush.UpdateSprite(key);
        }

        public void SetEyeShadow(string key)
        {
            _character.Eyeshadow.UpdateSprite(key);
        }

        public bool CanAccept(IToolVisitor tool)
        {
            return tool.CanApplyTo(this);
        }

        public void Accept(IToolVisitor tool)
        {
            tool.ApplyTo(this);
        }
    }
}