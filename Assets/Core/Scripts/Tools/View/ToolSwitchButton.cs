using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Tools.View
{
    public class ToolSwitchButton : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _enabledState;
        [SerializeField] private GameObject _disabledState;
        
        private Action<string> _onClicked;

        public string Key => _key;

        public void Init(Action<string> onClicked)
        {
            _onClicked = onClicked;
            _button.onClick.RemoveListener(OnClick);
            _button.onClick.AddListener(OnClick);
        }

        public void Enable()
        {
            _enabledState.SetActive(true);
            _disabledState.SetActive(false);
        }

        public void Disable()
        {
            _enabledState.SetActive(false);
            _disabledState.SetActive(true);
        }

        private void OnClick()
        {
            _onClicked?.Invoke(Key);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}