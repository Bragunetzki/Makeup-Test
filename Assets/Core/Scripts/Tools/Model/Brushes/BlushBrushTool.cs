using Core.Scripts.Targets.Model;

namespace Core.Scripts.Tools.Model.Brushes
{
    public class BlushBrushTool : AbstractBrushTool
    {
        private const string KEY = "blush";
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
            target.SetBlush(CurrentPaletteKey);
            StartPutDown();
        }

        public override void ApplyTo(MouthTarget target)
        {
            // do nothing.
        }
    }
}