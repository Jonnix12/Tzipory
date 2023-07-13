using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityTargetingComponent : IEntityComponent
    {
        public Stat TargetingRange { get; }
        public EntityTeamType EntityTeamType { get; }//temp
        public IPriorityTargeting DefaultPriorityTargeting { get; }
        public TargetingHandler Targeting { get; set; }
        public float GetDistanceToTarget(IEntityTargetAbleComponent targetAbleComponent);
    }
}