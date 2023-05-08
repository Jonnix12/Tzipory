using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public class TargetingHandler : ITargeting
    {
        private IEntityTargetingComponent _entityTargetingComponent;
        private List<IEntityTargetAbleComponent> _availableTargets;
        

        public TargetingHandler(IEntityTargetingComponent targetingComponent)
        {
            _availableTargets = new List<IEntityTargetAbleComponent>();
            _entityTargetingComponent = targetingComponent;
        }

        public IEntityTargetAbleComponent GetPriorityTarget(IPriority priority = null)
        {
            if (priority == null)
                return _entityTargetingComponent.DefaultPriority.GetPriorityTarget(_availableTargets);
            
            return priority.GetPriorityTarget(_availableTargets);
        }

        public void UpdateTargets()
        {
            
        }
    }
}