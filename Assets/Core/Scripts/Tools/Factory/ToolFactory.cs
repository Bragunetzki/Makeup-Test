using System.Collections.Generic;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Tools.Controller;
using Core.Scripts.Tools.Model;
using Core.Scripts.Tools.View;
using VContainer;

namespace Core.Scripts.Tools.Factory
{
    public class ToolFactory : IToolFactory
    {
        private readonly ToolView _creamView;
        private readonly List<LipstickToolView> _lipstickViews;

        [Inject]
        public ToolFactory(ToolView creamView, List<LipstickToolView> lipstickViews)
        {
            _creamView = creamView;
            _lipstickViews = lipstickViews;
        }

        public IToolController CreateCreamTool(TargetsController targetsController)
        {
            return new ToolController(new CreamTool(), _creamView, targetsController);
        }
        
        public List<IToolController> CreateLipstickTools(TargetsController targetsController)
        {
            var controllers = new List<IToolController>();
            foreach (LipstickToolView lipstickView in _lipstickViews)
            {
                controllers.Add(new ToolController(
                    new LipstickTool(lipstickView.LipstickKey),
                    lipstickView,
                    targetsController));
            }

            return controllers;
        }
    }
}