using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public abstract class BaseTargetingPriorityTargeting : IPriorityTargeting
    {
        protected IEntityTargetingComponent TargetingComponent;

        protected BaseTargetingPriorityTargeting(IEntityTargetingComponent targetingComponent)
        {
            TargetingComponent  = targetingComponent;
        }
        
        public abstract IEntityTargetAbleComponent GetPriorityTarget(IEnumerable<IEntityTargetAbleComponent> targets);
    }
}