using System.Collections.Generic;
using Helpers.Consts;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityExecuteTypes
{
    public class AoeAbilityExecuter :  IAbilityExecutor , IStatHolder
    {
        private const string  AoePrefabPath = "Prefabs/Ability/AoeAbilityEntity";
        
        private GameObject _aoePrefab;
        
        public Dictionary<int, Stat> Stats { get; }

        public Stat Radius { get; private set; }
        public Stat Duration { get; private set; }
        
        private List<BaseStatusEffect> _statusEffects;
        public AbilityExecuteType AbilityExecuteType => AbilityExecuteType.AOE;
        public IEntityTargetAbleComponent Caster { get; }
        public List<BaseStatusEffect> StatusEffects { get; }


        public AoeAbilityExecuter(IEntityTargetAbleComponent caster,AbilityConfig abilityConfig)
        {
            Caster = caster;
            StatusEffects = new List<BaseStatusEffect>();

            foreach (var effectConfigSo in abilityConfig.StatusEffectConfigs)
                StatusEffects.Add(Factory.StatusEffectFactory.GetStatusEffect(effectConfigSo));

            Radius = new Stat("AoeRadius", abilityConfig.AoeRadius, int.MaxValue, Constant.StatIds.AoeRadius);
            Duration = new Stat("AoeDuration", abilityConfig.AoeDuration, int.MaxValue, Constant.StatIds.AoeDuration);

            _aoePrefab = Resources.Load<GameObject>(AoePrefabPath);
        }

        public void Execute(IEntityTargetAbleComponent target)
        {
            var aoeGameobject = Object.Instantiate(_aoePrefab).GetComponent<AoeAbilityEntity>();
            aoeGameobject.Init(target,Radius.CurrentValue,Duration.CurrentValue,this);
        }

    }
}