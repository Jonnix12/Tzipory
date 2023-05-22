﻿using System;
using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.Helpers;
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public class AbilityHandler : IAbilityCaster
    {
        public IEntityTargetingComponent Caster { get; }
        public Dictionary<string, BaseAbility> Abilities { get; }
        
        public AbilityHandler(IEntityTargetingComponent caster,IEntityTargetingComponent targetingComponent,IEnumerable<AbilityConfig> abilitiesConfig)//temp
        {
            Abilities = new Dictionary<string, BaseAbility>();
            Caster = caster;

            foreach (var baseAbilityConfig in abilitiesConfig)
            {
                switch (baseAbilityConfig.AbilityType)
                {
                    case AbilityType.AOE:
                        Abilities.Add(baseAbilityConfig.AbilityName,new AoeAbility(caster,baseAbilityConfig));
                        break;
                    case AbilityType.Single:
                        Abilities.Add(baseAbilityConfig.AbilityName,new SingleAbility(caster,baseAbilityConfig));
                        break;
                    case AbilityType.Projectile:
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
                CoroutineHelper.Instance.StartCoroutineHelper(ability.Execute(availableTarget));
            }
            else
                Debug.LogError($"{Caster.EntityTransform.name} cant find ability {abilityName}");
        }
    }
}