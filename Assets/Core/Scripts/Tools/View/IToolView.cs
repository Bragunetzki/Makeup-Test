using System;
using System.Collections.Generic;
using Core.Scripts.Targets.View;

namespace Core.Scripts.Tools.View
{
    public interface IToolView
    {
        event Action UserPointerDown;
        event Action<List<ToolTargetView>> DroppedOnTargets;

        void Init();
        void PlayPickUpAnimation(Action onComplete);
        void PlayApplyAnimation(ToolTargetView targetView, Action<ToolTargetView> onComplete);
        void PlayPutDownAnimation(Action onComplete);
        void Reset();
        void Show();
        void Hide();
    }
}