using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public abstract class BaseTargetingPriority : IPriority
    {
        protected IEntityTargetingComponent TargetingComponent;

        protected BaseTargetingPriority(IEntityTargetingComponent targetingComponent)
        {
            TargetingComponent  = targetingComponent;
        }
        
        public abstract IEntityTargetAbleComponent GetPriorityTarget(List<IEntityTargetAbleComponent> targets);
    }
}