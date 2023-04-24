using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public class TargetingHandler : ITargeting
    {
        private IEntityTargetingComponent _entityTargetingComponent;
        private List<IEntityTargetAbleComponent> _availableTargets;
        
        private IPriority DefaultPriority { get; }

        protected TargetingHandler(IEntityTargetingComponent targetingComponent)
        {
            _availableTargets = new List<IEntityTargetAbleComponent>();
            DefaultPriority = targetingComponent.DefaultPriority;
            _entityTargetingComponent = targetingComponent;
        }

        public IEntityTargetAbleComponent GetPriorityTarget(IPriority priority = null)
        {
            if (priority == null)
                return DefaultPriority.GetPriorityTarget(_availableTargets);
            
            return priority.GetPriorityTarget(_availableTargets);
        }

        public void UpdateTargets()
        {
            
        }
    }
}