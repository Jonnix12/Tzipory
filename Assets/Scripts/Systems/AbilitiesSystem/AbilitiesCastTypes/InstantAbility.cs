using System;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.AbilitiesSystem
{
    public class InstantAbility : IAbilityCaster
    {
        public event Action OnCast;
        public AbilityCastType AbilityCastType => AbilityCastType.Instant;
        
        public InstantAbility(IEntityTargetingComponent entityCasterTargetingComponent,AbilityConfig config)
        {
        }

        
        public void Cast(IEntityTargetAbleComponent target, IAbilityExecutor abilityExecutor)
        {
            OnCast?.Invoke();
            abilityExecutor.Init(target);
        }
    }
}