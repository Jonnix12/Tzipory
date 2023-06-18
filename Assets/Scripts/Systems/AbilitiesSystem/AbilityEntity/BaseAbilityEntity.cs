using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityEntity
{
    public abstract class BaseAbilityEntity : BaseGameEntity
    {
        [SerializeField] protected CircleCollider2D _collider2D;
        [SerializeField] protected Transform _sprite;
        
        private IAbilityExecutor _abilityExecutor;
        private IEntityTargetAbleComponent _target;

        public virtual void Init(IEntityTargetAbleComponent target, IAbilityExecutor abilityExecutor)
        {
            _target = target;
            _abilityExecutor = abilityExecutor;
        }

        protected virtual void Cast(IEntityTargetAbleComponent target)
        {
            _abilityExecutor.Execute(target);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IEntityTargetAbleComponent>(out var targetAbleComponent))
                _abilityExecutor.Execute(targetAbleComponent);
        }
    }
}