using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public class TargetingHandler : MonoBehaviour , ITargeting
    {
        private IEntityTargetingComponent _entityTargetingComponent;
        private List<IEntityTargetAbleComponent> _availableTargets;

        

        public List<IEntityTargetAbleComponent> AvailableTargets => _availableTargets;

        public TargetingHandler(IEntityTargetingComponent targetingComponent)//may whnt to not be a monobehavior
        {
            _availableTargets = new List<IEntityTargetAbleComponent>();
            _entityTargetingComponent = targetingComponent;
            
        }

        public void Init(IEntityTargetingComponent targetingComponent)
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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<BaseUnitEntity>(out BaseUnitEntity unitEntity) && unitEntity.EntityTeamType != _entityTargetingComponent.EntityTeamType) //Removing friendly fire!
                AddTarget(unitEntity);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<BaseUnitEntity>(out BaseUnitEntity unitEntity))
                RemoveTarget(unitEntity);
        }
    }
}