using System;
using System.Collections.Generic;
using Core.Scripts.Character.Controller;
using Core.Scripts.Character.Model;
using Core.Scripts.Targets.Model;
using Core.Scripts.Targets.View;

namespace Core.Scripts.Targets.Controller
{
    public class TargetsController
    {
        private readonly Dictionary<Guid, IToolTarget> _targetsDictionary = new();
        private readonly Dictionary<Guid, ToolTargetView> _viewsDictionary = new();

        public TargetsController(
            MakeUpCharacter character, 
            ToolTargetView faceTargetView,
            ToolTargetView mouthTargetView)
        {
            AddTarget<FaceTarget>(faceTargetView, character);
            AddTarget<MouthTarget>(mouthTargetView, character);
        }

        private void AddTarget<T>(ToolTargetView view, MakeUpCharacter character) where T : IToolTarget, new()
        {
            Guid id = GetNewGuid();
            var target = new T();
            target.Init(id, character);
            view.Init(id);
            _targetsDictionary.Add(id, target);
            _viewsDictionary.Add(id, view);
        }

        public bool TryGetTarget(Guid id, out IToolTarget target)
        {
            return _targetsDictionary.TryGetValue(id, out target);
        }

        public bool TryGetView(Guid id, out ToolTargetView target)
        {
            return _viewsDictionary.TryGetValue(id, out target);
        }

        private Guid GetNewGuid()
        {
            var guid = Guid.NewGuid();

            // very unlikely to happen
            while (_viewsDictionary.ContainsKey(guid) || _targetsDictionary.ContainsKey(guid))
            {
                guid = Guid.NewGuid();
            }

            return guid;
        }
    }
}