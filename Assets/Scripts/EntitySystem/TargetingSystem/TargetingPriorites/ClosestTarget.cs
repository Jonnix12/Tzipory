using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem.TargetingPriorites
{
    public class ClosestTarget : BaseTargetingPriority
    {
        public ClosestTarget(IEntityTargetingComponent targetingComponent) : base(targetingComponent)
        {
        }

        public override IEntityTargetAbleComponent GetPriorityTarget(List<IEntityTargetAbleComponent> targets)
        {
            throw new System.NotImplementedException();
        }
    }
}