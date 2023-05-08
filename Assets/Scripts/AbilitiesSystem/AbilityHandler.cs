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
                
            }
            
        }
    }
}