using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public class TargetingHandler : MonoBehaviour
    {
        private IEntityTargetingComponent _entityTargetingComponent;
        private List<IEntityTargetAbleComponent> _availableTargets;
        
        public IEntityTargetAbleComponent CurrentTarget { get; private set; }
        
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

        public void SetAttackTarget(IEntityTargetAbleComponent target)
        {
            CurrentTarget = target;
        }

        public void GetPriorityTarget(IPriorityTargeting priorityTargeting = null)
        {
            if (priorityTargeting == null)
            {
                CurrentTarget = _entityTargetingComponent.DefaultPriorityTargeting.GetPriorityTarget(_availableTargets);
                return;
            }
            
            CurrentTarget = priorityTargeting.GetPriorityTarget(_availableTargets);
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
            if (other.gameObject.TryGetComponent(out IEntityTargetAbleComponent targetAbleComponent) && targetAbleComponent.EntityTeamType != _entityTargetingComponent.EntityTeamType) //Removing friendly fire!
                AddTarget(targetAbleComponent);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IEntityTargetAbleComponent targetAbleComponent))
            {
                RemoveTarget(targetAbleComponent);
                
                if (CurrentTarget == null)
                    return;
                
                if (targetAbleComponent.EntityInstanceID == CurrentTarget.EntityInstanceID)
                    GetPriorityTarget();
            }
        }
    }
}