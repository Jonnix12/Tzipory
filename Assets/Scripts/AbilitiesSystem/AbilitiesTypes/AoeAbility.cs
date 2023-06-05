using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public class AoeAbility : BaseAbility
    {
        private const string  AoePrefabPath = "Prefabs/Ability/AoeAbilityEntity";
        
        public Stat Radius { get; private set; }
        public Stat Duration { get; private set; }
        
        private GameObject _aoePrefab;
        
        public AoeAbility(IEntityTargetingComponent entityCasterTargetingComponent ,AbilityConfig config) : base(entityCasterTargetingComponent ,config)
        {
            if(AbilityParameter.TryGetValue("Radius", out Stat radius))
                Radius = radius;
            else
                throw new System.Exception($"{nameof(AoeAbility)} Radius not found");
            
            if(AbilityParameter.TryGetValue("Duration", out Stat duration))
                Duration = duration;
            else
                throw new System.Exception($"{nameof(AoeAbility)} Duration not found");
            
            _aoePrefab = Resources.Load<GameObject>(AoePrefabPath);
        }

        protected override void ExecuteAbility()
        {
            var aoeEntity = GameObject.Instantiate(_aoePrefab,CurrentTarget.EntityTransform.position,Quaternion.identity);
            aoeEntity.GetComponent<AoeAbilityEntity>().Init(Radius.CurrentValue,Duration.CurrentValue,StatusEffects);
        }
    }
}