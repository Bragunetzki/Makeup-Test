using System.Collections.Generic;
using Core.Scripts.Targets.Controller;
using Core.Scripts.Tools.Controller;

namespace Core.Scripts.Tools.Factory
{
    public interface IToolFactory
    {
        IToolController CreateCreamTool(TargetsController targetsController);
        List<IToolController> CreateLipstickTools(TargetsController targetsController);
    }
}