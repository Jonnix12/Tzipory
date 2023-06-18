using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.AbilitiesSystem
{
    public class SingleAbility : BaseAbility
    {
        public SingleAbility(IEntityTargetingComponent entityCasterTargetingComponent,AbilityConfig config) : base(entityCasterTargetingComponent ,config)
        {
        }

        protected override void ExecuteAbility()
        {
            StartCooldown();//temp!!! need to start cooldown after done execute
        }
    }
}