using System;

namespace Core.Scripts.Tools.Controller
{
    public interface IToolController : IDisposable
    {
        event Action<IToolController> ToolPickedUp;

        bool IsSwitchable { get; }
        string Key { get; }
        
        void Init();
        void PostInit();
        void PutDownTool();
        void Hide();
        void Show();
    }
}