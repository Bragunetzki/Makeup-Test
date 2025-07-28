using System;
using Core.Scripts.Character.Model;
using Core.Scripts.Tools.Model;

namespace Core.Scripts.Targets.Model
{
    public class MouthTarget : IToolTarget
    {
        private MakeUpCharacter _character;
        public Guid Id { get; private set; }

        public void Init(Guid id, MakeUpCharacter character)
        {
            Id = id;
            _character = character;
        }

        public void SetLipstick(string key)
        {
            _character.Lipstick.UpdateSprite(key);
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