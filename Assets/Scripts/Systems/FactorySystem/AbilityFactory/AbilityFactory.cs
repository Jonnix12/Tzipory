using System;
using Tzipory.AbilitiesSystem;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;

namespace Factory
{
    public class AbilityFactory
    {
        public static BaseAbility GetAbility(AbilityConfig abilityConfig)
        {
            switch (abilityConfig.AbilityCastType)
            {
                case AbilityCastType.Projectile:
                    break;
                case AbilityCastType.Instant:
                    break;
                case AbilityCastType.Self:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return  null;//tempppp
        }
    }
}