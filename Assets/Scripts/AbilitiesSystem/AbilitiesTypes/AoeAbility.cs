using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilitiesTypes
{
    public class AoeAbility : BaseAbility
    {
        private readonly float _radius;
        private float _duration;
        
        public AoeAbility(float radius,float duration,IEntityTargetAbleComponent entityCaster,float cooldown) : base(entityCaster,cooldown)
        {
            _radius = radius;
            _duration = duration;
        }

        public AoeAbility(float radius,float duration,IEntityTargetAbleComponent entityCaster,float cooldown, BaseStatusEffect[] statusEffects) : base(entityCaster,cooldown,statusEffects)
        {
            _radius = radius;
            _duration = duration;
        }

        public AoeAbility(float radius,float duration,IEntityTargetAbleComponent entityCaster,float cooldown, AbilityActionType abilityType, float amount) : base(entityCaster,cooldown,abilityType,amount)
        {
            _radius = radius;
            _duration = duration;
        }
        
        public override void Cast(IEntityTargetAbleComponent target)
        {
            var colliders = Physics.OverlapSphere(target.EntityTransform.position, _radius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IEntityTargetAbleComponent entityTargetAbleComponent))
                {
                    abilityCaster.Cast(target);
                }
            }
        }
    }
}