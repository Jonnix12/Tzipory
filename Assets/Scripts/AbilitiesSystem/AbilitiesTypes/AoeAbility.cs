using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public class AoeAbility : BaseAbility
    {
        public Stat Radius { get; private set; }
        public Stat Duration { get; private set; }


        public AoeAbility(IEntityTargetAbleComponent entityCaster, AbilityConfig config) : base(entityCaster, config)
        {
            if(StatsValue.TryGetValue("Radius", out Stat radius))
                Radius = radius;
            else
                throw new System.Exception($"{nameof(AoeAbility)} Radius not found");
            
            if(StatsValue.TryGetValue("Duration", out Stat duration))
                Duration = duration;
            else
                throw new System.Exception($"{nameof(AoeAbility)} Duration not found");
        }

        protected override void Cast(IEntityTargetAbleComponent target)
        {
            var colliders = Physics.OverlapSphere(target.EntityTransform.position, Radius.CurrentValue);

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