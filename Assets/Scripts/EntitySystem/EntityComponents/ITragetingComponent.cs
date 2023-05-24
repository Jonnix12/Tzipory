using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityTargetingComponent : IEntityComponent
    {
        public EntityTeamType EntityTeamType { get; }//temp
        public IPriorityTargeting DefaultPriorityTargeting { get; }
        public ITargeting TargetingHandler { get; set; }
        public float GetDistanceToTarget(IEntityTargetAbleComponent targetAbleComponent);
    }
}