using Core.Scripts.Targets.Controller;
using Core.Scripts.Tools.Config;
using Core.Scripts.Tools.Controller;

namespace Core.Scripts.Tools.Factory
{
    public interface IBrushToolFactory
    {
        IToolController CreateBrushTool(string toolKey, BrushToolParams @params, TargetsController targetsController);
    }
}