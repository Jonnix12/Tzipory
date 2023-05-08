using System;
using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;

namespace Tzipory.AbilitiesSystem
{
    public class AbilityHandler : IAbilityCaster
    {
        public List<BaseAbility> Abilities { get; }
        
        public AbilityHandler(BaseAbilityConfig[] abilitiesConfig)
        {
            Abilities = new List<BaseAbility>();

            foreach (var baseAbilityConfig in abilitiesConfig)
            {
                AoeAbility aoeAbility = new AoeAbility();
                var ability = (BaseAbility) Activator.CreateInstance(baseAbilityConfig.Type);
                Abilities.Add(ability);
            }
            
        }
    }
}