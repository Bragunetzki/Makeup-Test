using System;
using System.Collections.Generic;
using Core.Scripts.Character.Config;
using Core.Scripts.Character.Controller;
using Core.Scripts.Character.View;
using Core.Scripts.Sponge;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Targets.View;
using Core.Scripts.Tools.Config;
using Core.Scripts.Tools.Controller;
using Core.Scripts.Tools.Factory;
using Core.Scripts.Tools.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Scripts.Bootstrap
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [Header("Character Setup")]
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private ToolTargetView _faceTargetView;
        [SerializeField] private ToolTargetView _mouthTargetView;
        
        [Header("Basic Tools")]
        [SerializeField] private ToolView _creamView;
        [SerializeField] private SpongeView _spongeView;
        [SerializeField] private List<LipstickToolView> _lipstickViews;

        [Header("Brush Tools")]
        [SerializeField] private BrushToolParams _blushToolParams;
        [SerializeField] private BrushToolParams _eyeshadowToolParams;
    
        [Header("UI")]
        [SerializeField] private List<ToolSwitchButton> _switchButtons;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_characterView);
            builder.RegisterInstance(_characterConfig);
            builder.RegisterInstance(_creamView);
            builder.RegisterInstance(_spongeView);
            builder.RegisterInstance(_lipstickViews);
            builder.RegisterInstance(_switchButtons);
            
            builder.Register<MakeupCharacterController>(Lifetime.Singleton);
            builder.Register<TargetsController>(Lifetime.Singleton);
        
            builder.Register<IToolFactory, ToolFactory>(Lifetime.Singleton);
            builder.Register<IBrushToolFactory, BrushToolFactory>(Lifetime.Singleton);
            builder.Register<SpongeController>(Lifetime.Singleton);
            builder.Register<ToolsController>(Lifetime.Singleton);

            builder.RegisterEntryPoint<CharacterBootstrapper>();
            builder.RegisterEntryPoint<ToolSystemBootstrapper>()
                .WithParameter("blushToolParams", _blushToolParams)
                .WithParameter("eyeshadowToolParams", _eyeshadowToolParams)
                .WithParameter("faceTargetView", _faceTargetView)
                .WithParameter("mouthTargetView", _mouthTargetView);
        }
    }
}