using Core.Scripts.Targets.Model;

namespace Core.Scripts.Tools.Model.Brushes
{
    public class EyeshadowBrushTool : AbstractBrushTool
    {
        private const string KEY = "eyeshadow";
        public override string SwitchKey => KEY;

        public override bool CanApplyTo(FaceTarget target)
        {
            return true;
        }

        public override bool CanApplyTo(MouthTarget target)
        {
            return false;
        }

        public override void ApplyTo(FaceTarget target)
        {
            target.SetEyeShadow(CurrentPaletteKey);
            StartPutDown();
        }

        public override void ApplyTo(MouthTarget target)
        {
            // do nothing.
        }
    }
}