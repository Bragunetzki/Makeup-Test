using System;
using System.Collections.Generic;
using Core.Scripts.Palette;
using Core.Scripts.Palette.View;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Targets.Model;
using Core.Scripts.Targets.View;
using Core.Scripts.Tools.Model;
using Core.Scripts.Tools.View;
using UnityEngine;

namespace Core.Scripts.Tools.Controller
{
    public class BrushToolController : IToolController
    {
        public event Action<IToolController> ToolPickedUp;

        private readonly IBrushToolView _view;
        private readonly IBrushTool _tool;
        private readonly TargetsController _targetsController;
        private readonly ToolPalette _palette;

        public bool IsSwitchable => _tool.IsSwitchable;
        public string Key => _tool.SwitchKey;

        public BrushToolController(
            IBrushTool tool, 
            IBrushToolView toolView, 
            TargetsController targetsController,
            ToolPalette palette)
        {
            _targetsController = targetsController;
            _tool = tool;
            _view = toolView;
            _palette = palette;
        }

        public void Init()
        {
            _view.Init();
            _palette.Init();
            Subscribe();
        }
        
        public void PostInit()
        {
            _tool.Enable();
            _tool.ClearColor();
        }

        public void PutDownTool()
        {
            _tool.StartPutDown();
        }

        public void Hide()
        {
            _palette.Hide();
            _view.Hide();
            _view.Reset();
            _tool.Reset();
        }

        public void Show()
        {
            _palette.Show();
            _view.Show();
        }

        private void OnPaletteClick(string key)
        {
            if (!_tool.TryPickUp())
            {
                return;
            }

            if (!_palette.TryGetView(key, out BrushPaletteView paletteView))
            {
                return;
            }
            
            _tool.CurrentPaletteKey = key;
            _tool.SetPickUpBlocked(true);
            _view.PlayPickUpAnimation(paletteView, OnPickUpAnimationComplete);
            ToolPickedUp?.Invoke(this);
        }

        private void OnPickUpAnimationComplete()
        {
            _tool.SetPickUpBlocked(false);
            
            if (_palette.TryGetValue(_tool.CurrentPaletteKey, out Color color))
            {
                _tool.CurrentColor = color;
            }
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
                    _tool.SetPickUpBlocked(true);
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
            _view.DroppedOnTargets += OnDropped;
            _tool.Enabled += _view.Show;
            _tool.Disabled += _view.Hide;
            _tool.PutDownStarted += OnPutDownStarted;
            _tool.ColorChanged += _view.SetColor;
            _tool.ColorCleared += _view.ClearColor;
            _palette.SubscribeOnClick(OnPaletteClick);
        }

        private void Unsubscribe()
        {
            _view.DroppedOnTargets -= OnDropped;
            _tool.Enabled -= _view.Show;
            _tool.Disabled -= _view.Hide;
            _tool.PutDownStarted -= OnPutDownStarted;
            _tool.ColorChanged -= _view.SetColor;
            _tool.ColorCleared -= _view.ClearColor;
            _palette.UnsubscribeFromClick(OnPaletteClick);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}