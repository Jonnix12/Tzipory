using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public abstract class BaseTargetingPriority : IPriorityTargeting
    {
        protected IEntityTargetingComponent TargetingComponent;

        protected BaseTargetingPriority(IEntityTargetingComponent targetingComponent)
        {
            TargetingComponent  = targetingComponent;
        }
        
        public abstract IEntityTargetAbleComponent GetPriorityTarget(IEnumerable<IEntityTargetAbleComponent> targets);
    }
}