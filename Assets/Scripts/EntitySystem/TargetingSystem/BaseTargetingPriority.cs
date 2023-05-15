using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public abstract class BaseTargetingPriority : IPriority
    {
        public BaseTargetingPriority(IEntityTargetingComponent targetingComponent)
        {
            
        }
        
        public abstract IEntityTargetAbleComponent GetPriorityTarget(List<IEntityTargetAbleComponent> targets);
    }
}