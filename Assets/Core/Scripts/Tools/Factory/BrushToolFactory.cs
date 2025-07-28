using System;
using Core.Scripts.Palette;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Tools.Config;
using Core.Scripts.Tools.Controller;
using Core.Scripts.Tools.Model;
using Core.Scripts.Tools.Model.Brushes;

namespace Core.Scripts.Tools.Factory
{
    public class BrushToolFactory : IBrushToolFactory
    {
        public IToolController CreateBrushTool(string toolKey, BrushToolParams @params, TargetsController targetsController)
        {
            IBrushTool brushTool = toolKey switch
            {
                BrushConstants.BlushName => new BlushBrushTool(),
                BrushConstants.EyeshadowName => new EyeshadowBrushTool(),
                _ => throw new ArgumentException($"Unknown brush tool type: {toolKey}")
            };

            var palette = new ToolPalette(
                @params.PaletteConfig,
                @params.PalettesParent,
                @params.PaletteViews);

            return new BrushToolController(
                brushTool,
                @params.ToolView,
                targetsController,
                palette);
        }
    }
}