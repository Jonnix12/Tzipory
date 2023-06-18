﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public class AbilityHandler
    {
        public IEntityTargetingComponent Caster { get; }
        public Dictionary<string, BaseAbility> Abilities { get; }
        
        public bool IsCasting => Abilities.Any(ability => ability.Value.IsCasting);
        
        public AbilityHandler(IEntityTargetingComponent caster,IEnumerable<AbilityConfig> abilitiesConfig)//temp
        {
            Abilities = new Dictionary<string, BaseAbility>();
            Caster = caster;

            foreach (var baseAbilityConfig in abilitiesConfig)//need to add factory
            {
                switch (baseAbilityConfig.AbilityExecuteType)
                {
                    case AbilityExecuteType.AOE:
                        Abilities.Add(baseAbilityConfig.AbilityName,new SelfAbility(caster,baseAbilityConfig));
                        break;
                    case AbilityExecuteType.Single:
                        Abilities.Add(baseAbilityConfig.AbilityName,new SingleAbility(caster,baseAbilityConfig));
                        break;
                    case AbilityExecuteType.Chain:
                        Abilities.Add(baseAbilityConfig.AbilityName,new ProjectileAbility(caster,baseAbilityConfig));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void CastAbilityByName(string abilityName,IEnumerable<IEntityTargetAbleComponent> availableTarget)
        {
            if (Abilities.TryGetValue(abilityName, out var ability))
            {
                ability.Cast(availableTarget);
            }
            else
                Debug.LogError($"{Caster.EntityTransform.name} cant find ability {abilityName}");
        }
    }
}