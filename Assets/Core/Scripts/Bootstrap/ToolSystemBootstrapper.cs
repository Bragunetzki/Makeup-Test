using System;
using System.Collections.Generic;
using Core.Scripts.Character.Controller;
using Core.Scripts.Character.Model;
using Core.Scripts.Sponge;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Targets.View;
using Core.Scripts.Tools.Config;
using Core.Scripts.Tools.Controller;
using Core.Scripts.Tools.Factory;
using Core.Scripts.Tools.Model.Brushes;
using Core.Scripts.Tools.View;
using VContainer;
using VContainer.Unity;

namespace Core.Scripts.Bootstrap
{
    public class ToolSystemBootstrapper : IInitializable, IPostInitializable, IDisposable
    {
        private readonly MakeUpCharacter _character;
        private readonly ToolTargetView _faceTargetView;
        private readonly ToolTargetView _mouthTargetView;
        private readonly SpongeView _spongeView;
        private readonly List<ToolSwitchButton> _switchButtons;
        private readonly BrushToolParams _blushToolParams;
        private readonly BrushToolParams _eyeshadowToolParams;
        private readonly IToolFactory _toolFactory;
        private readonly IBrushToolFactory _brushToolFactory;

        private ToolsController _toolsController;
        private SpongeController _spongeController;
        private TargetsController _targetsController;

        [Inject]
        public ToolSystemBootstrapper(
            MakeupCharacterController characterController,
            ToolTargetView faceTargetView,
            ToolTargetView mouthTargetView,
            SpongeView spongeView,
            List<ToolSwitchButton> switchButtons,
            BrushToolParams blushToolParams,
            BrushToolParams eyeshadowToolParams,
            IToolFactory toolFactory,
            IBrushToolFactory brushToolFactory)
        {
            _character = characterController.Character;
            _faceTargetView = faceTargetView;
            _mouthTargetView = mouthTargetView;
            _spongeView = spongeView;
            _switchButtons = switchButtons;
            _blushToolParams = blushToolParams;
            _eyeshadowToolParams = eyeshadowToolParams;
            _toolFactory = toolFactory;
            _brushToolFactory = brushToolFactory;
        }

        public void Initialize()
        {
            _targetsController = new TargetsController(_character, _faceTargetView, _mouthTargetView);
            InitializeTools();
            InitializeSponge();
        }

        public void PostInitialize()
        {
            _toolsController.PostInit();
        }

        private void InitializeTools()
        {
            var toolControllers = new List<IToolController>();

            toolControllers.AddRange(_toolFactory.CreateLipstickTools(_targetsController));
            toolControllers.Add(_toolFactory.CreateCreamTool(_targetsController));
            
            toolControllers.Add(_brushToolFactory.CreateBrushTool(
                BrushConstants.BlushName, _blushToolParams, _targetsController));
            toolControllers.Add(_brushToolFactory.CreateBrushTool(
                BrushConstants.EyeshadowName, _eyeshadowToolParams, _targetsController));

            _toolsController = new ToolsController(_switchButtons, toolControllers);
            _toolsController.Init();
        }

        private void InitializeSponge()
        {
            _spongeController = new SpongeController(_character, _spongeView);
        }

        public void Dispose()
        {
            _toolsController?.Dispose();
        }
    }
}