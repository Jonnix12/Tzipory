using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public interface IPriority
    {
        public IEntityTargetAbleComponent GetPriorityTarget(List<IEntityTargetAbleComponent> targets);
    }
}