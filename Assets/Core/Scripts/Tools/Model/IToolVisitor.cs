using Core.Scripts.Targets.Model;

namespace Core.Scripts.Tools.Model
{
    public interface IToolVisitor
    {
        bool CanApplyTo(FaceTarget target);
        bool CanApplyTo(MouthTarget target);
        void ApplyTo(FaceTarget target);
        void ApplyTo(MouthTarget target);
    }
}