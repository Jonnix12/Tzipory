using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public interface ITargeting
    {
        public IEntityTargetAbleComponent GetPriorityTarget(IPriority priority = null);
    }
}