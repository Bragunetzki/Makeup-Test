using System;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts.Tools.Switcher;
using Core.Scripts.Tools.View;

namespace Core.Scripts.Tools.Controller
{
    public class ToolsController : IDisposable
    {
        private const string DEFAULT_SWITCH_KEY = "blush";
        
        private readonly List<IToolController> _toolControllers;
        private readonly ToolSwitcher _toolSwitcher;

        public ToolsController(List<ToolSwitchButton> switchButtons, List<IToolController> toolControllers)
        {
            _toolControllers = toolControllers;
            _toolSwitcher = new ToolSwitcher(switchButtons,
                toolControllers
                    .Where(controller => controller.IsSwitchable).ToList());
            Subscribe();
        }

        public void Init()
        {
            foreach (IToolController controller in _toolControllers)
            {
                controller.Init();
            }
            
            _toolSwitcher.Init();
        }

        public void PostInit()
        {
            foreach (IToolController controller in _toolControllers)
            {
                controller.PostInit();
            }
            
            _toolSwitcher.PostInit(DEFAULT_SWITCH_KEY);
        }

        private void OnToolPickedUp(IToolController toolController)
        {
            foreach (IToolController controller in _toolControllers)
            {
                if (controller != toolController)
                {
                    controller.PutDownTool();
                }
            }
        }

        private void Subscribe()
        {
            foreach (IToolController toolController in _toolControllers)
            {
                toolController.ToolPickedUp += OnToolPickedUp;
            }
        }

        private void Unsubscribe()
        {
            foreach (IToolController toolController in _toolControllers)
            {
                toolController.ToolPickedUp -= OnToolPickedUp;
            }
        }

        public void Dispose()
        {
            Unsubscribe();

            foreach (IToolController toolController in _toolControllers)
            {
                toolController.Dispose();
            }
        }
    }
}