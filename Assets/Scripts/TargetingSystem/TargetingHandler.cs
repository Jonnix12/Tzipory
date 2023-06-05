using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public class TargetingHandler : ITargeting
    {
        private IEntityTargetingComponent _entityTargetingComponent;
        private List<IEntityTargetAbleComponent> _availableTargets;

        public List<IEntityTargetAbleComponent> AvailableTargets => _availableTargets;

        public TargetingHandler(IEntityTargetingComponent targetingComponent)
        {
            _availableTargets = new List<IEntityTargetAbleComponent>();
            _entityTargetingComponent = targetingComponent;
        }

        public IEntityTargetAbleComponent GetPriorityTarget(IPriorityTargeting priorityTargeting = null)
        {
            if (priorityTargeting == null)
                return _entityTargetingComponent.DefaultPriorityTargeting.GetPriorityTarget(_availableTargets);
            
            return priorityTargeting.GetPriorityTarget(_availableTargets);
        }

        public void AddTarget(IEntityTargetAbleComponent targetAbleComponent)
        {
            _availableTargets.Add(targetAbleComponent);
        }

        public void RemoveTarget(IEntityTargetAbleComponent targetAbleComponent)
        {
            _availableTargets.Remove(targetAbleComponent);
        }
    }
}