using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.AbilitiesSystem
{
    public interface IAbilityCaster
    {
        public AbilityCastType AbilityCastType  { get; }

        public void Cast(IEntityTargetAbleComponent target,IAbilityExecutor abilityExecutor);
    }
}