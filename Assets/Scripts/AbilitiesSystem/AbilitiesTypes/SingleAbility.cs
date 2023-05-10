using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.AbilitiesSystem
{
    public class SingleAbility : BaseAbility
    {
        public SingleAbility(IEntityTargetAbleComponent entityCaster, AbilityConfig config) : base(entityCaster, config)
        {
        }

        protected override void Cast(IEntityTargetAbleComponent target)
        {
            abilityCaster.Cast(target);
        }
    }
}