using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityTargetingComponent : IEntityComponent
    {
        public IPriority DefaultPriority { get; }
        public ITargeting TargetingHandler { get; set; }
        public float GetDistanceToTarget(IEntityTargetAbleComponent targetAbleComponent);
        public void SetTargeting(ITargeting targeting);
    }
}