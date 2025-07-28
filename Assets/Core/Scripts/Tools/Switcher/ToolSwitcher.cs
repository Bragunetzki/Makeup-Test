using System.Collections.Generic;
using Core.Scripts.Tools.Controller;
using Core.Scripts.Tools.View;

namespace Core.Scripts.Tools.Switcher
{
    public class ToolSwitcher
    {
        private readonly List<ToolSwitchButton> _switchButtons;
        private readonly List<IToolController> _toggleGroup;

        public ToolSwitcher(List<ToolSwitchButton> switchButtons, List<IToolController> toggleGroup)
        {
            _switchButtons = switchButtons;
            _toggleGroup = toggleGroup;
        }

        public void Init()
        {
            foreach (ToolSwitchButton button in _switchButtons)
            {
                button.Init(OnSwitchButtonClicked);
            }
        }

        public void PostInit(string defaultSwitchKey)
        {
            OnSwitchButtonClicked(defaultSwitchKey);
        }

        private void OnSwitchButtonClicked(string key)
        {
            foreach (IToolController controller in _toggleGroup)
            {
                if (controller.Key == key)
                {
                    controller.Show();
                }
                else
                {
                    controller.Hide();
                }
            }

            foreach (ToolSwitchButton button in _switchButtons)
            {
                if (button.Key == key)
                {
                    button.Enable();
                }
                else
                {
                    button.Disable();
                }
            }
        }
    }
}