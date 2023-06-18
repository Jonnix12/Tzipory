using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;

namespace Tzipory.AbilitiesSystem
{
    public interface IAbilityExecutor
    {
        public AbilityExecuteType AbilityExecuteType { get; }
        public IEntityTargetingComponent Caster { get; }
        
        protected List<BaseStatusEffect> StatusEffects { get; }
        
        public void Execute(IEntityTargetAbleComponent target);
    }
}