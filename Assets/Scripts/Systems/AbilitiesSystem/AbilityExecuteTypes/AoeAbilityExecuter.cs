using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.AbilitiesSystem.AbilityEntity;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityExecuteTypes
{
    public class AoeAbilityExecuter : BaseAbilityEntity , IAbilityExecutor
    {
        private const string  AoePrefabPath = "Prefabs/Ability/AoeAbilityEntity";
        
        private GameObject _aoePrefab;
        
        public Stat Radius { get; private set; }
        public Stat Duration { get; private set; }
        
        private List<BaseStatusEffect> _statusEffects;
        public AbilityExecuteType AbilityExecuteType => AbilityExecuteType.AOE;
        public IEntityTargetingComponent Caster { get; }

        List<BaseStatusEffect> IAbilityExecutor.StatusEffects => _statusEffects;

        public AoeAbilityExecuter(IEnumerator<BaseStatusEffect> statusEffects, IEntityTargetingComponent caster)
        {
            
            
            _aoePrefab = Resources.Load<GameObject>(AoePrefabPath);
        }

        public void Execute(IEntityTargetAbleComponent target)
        {
            throw new System.NotImplementedException();
        }
    }
}