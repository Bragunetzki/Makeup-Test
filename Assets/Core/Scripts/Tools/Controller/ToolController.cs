using System;
using System.Collections.Generic;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Targets.Model;
using Core.Scripts.Targets.View;
using Core.Scripts.Tools.Model;
using Core.Scripts.Tools.View;

namespace Core.Scripts.Tools.Controller
{
    public class ToolController : IToolController
    {
        public event Action<IToolController> ToolPickedUp;

        private readonly IToolView _view;
        private readonly ITool _tool;
        private readonly TargetsController _targetsController;

        public bool IsSwitchable => _tool.IsSwitchable;
        public string Key => _tool.SwitchKey;

        public ToolController(ITool tool, IToolView view, TargetsController targetsController)
        {
            _targetsController = targetsController;
            _tool = tool;
            _view = view;
        }

        public void Init()
        {
            _view.Init();
            Subscribe();
        }

        public void PostInit()
        {
            _tool.Enable();
        }
        
        public void PutDownTool()
        {
            _tool.StartPutDown();
        }

        public void Hide()
        {
            _view.Hide();
            _view.Reset();
            _tool.Reset();
        }

        public void Show()
        {
            _view.Show();
        }

        private void OnUserPointerDown()
        {
            if (!_tool.TryPickUp())
            {
                return;
            }

            ToolPickedUp?.Invoke(this);
            _view.PlayPickUpAnimation(null);
        }

        private void OnDropped(List<ToolTargetView> targetViews)
        {
            foreach (ToolTargetView targetView in targetViews)
            {
                if (!_targetsController.TryGetTarget(targetView.Id, out IToolTarget target))
                {
                    return;
                }

                if (target.CanAccept(_tool))
                {
                    _view.PlayApplyAnimation(targetView, OnApplyAnimationFinished);
                    break;
                }
            }
        }

        private void OnApplyAnimationFinished(ToolTargetView targetView)
        {
            if (!_targetsController.TryGetTarget(targetView.Id, out IToolTarget target))
            {
                return;
            }
            
            target.Accept(_tool);
        }

        private void OnPutDownStarted()
        {
            _view.PlayPutDownAnimation(_tool.OnPutDownFinished);
        }

        private void Subscribe()
        {
            _view.UserPointerDown += OnUserPointerDown;
            _view.DroppedOnTargets += OnDropped;
            _tool.Enabled += _view.Show;
            _tool.Disabled += _view.Hide;
            _tool.PutDownStarted += OnPutDownStarted;
        }

        private void Unsubscribe()
        {
            _view.UserPointerDown -= OnUserPointerDown;
            _view.DroppedOnTargets -= OnDropped;
            _tool.Enabled -= _view.Show;
            _tool.Disabled -= _view.Hide;
            _tool.PutDownStarted -= OnPutDownStarted;
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}