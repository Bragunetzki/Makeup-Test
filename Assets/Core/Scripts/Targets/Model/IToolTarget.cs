using System;
using Core.Scripts.Character.Model;
using Core.Scripts.Tools.Model;

namespace Core.Scripts.Targets.Model
{
    public interface IToolTarget
    {
        void Init(Guid id, MakeUpCharacter character);
        Guid Id { get; }
        bool CanAccept(IToolVisitor tool);
        void Accept(IToolVisitor tool);
    }
}