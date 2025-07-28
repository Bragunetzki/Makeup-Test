using UnityEngine;

namespace Core.Scripts.Tools.View
{
    public class LipstickToolView : ToolView
    {
        [SerializeField] private string _lipstickKey;

        public string LipstickKey => _lipstickKey;
    }
}