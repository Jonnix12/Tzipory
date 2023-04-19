using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public interface ITargetingHandler
    {
        public List<ICombatTargetableComponent> AvailableTargets { get; }

        public ICombatTargetableComponent GetPriorityTarget();
        
        public void AddTarget(ICombatTargetableComponent target);
        
        public void RemoveTarget(ICombatTargetableComponent target);
    }
}