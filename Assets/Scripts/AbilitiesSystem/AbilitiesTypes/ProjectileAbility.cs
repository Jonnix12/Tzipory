using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.AbilitiesSystem.AbilityEntity;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using Unity.Mathematics;
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public class ProjectileAbility : BaseAbility
    {
        private const string ProjectilePrefabPath = "Prefabs/Ability/ProjectileAbilityEntity";
        
        public Stat ProjectileSpeed { get;}

        private GameObject _projectilePrefab;
        
        public ProjectileAbility(IEntityTargetingComponent entityCasterTargetingComponent, AbilityConfig config) : base(entityCasterTargetingComponent,config)
        {
            if (AbilityParameter.TryGetValue("ProjectileSpeed",out Stat projectileSpeed))
                ProjectileSpeed = projectileSpeed;
            else
                throw new System.Exception($"{nameof(ProjectileAbility)} ProjectileSpeed not found");

            _projectilePrefab = Resources.Load<GameObject>(ProjectilePrefabPath);
        }

        protected override void Cast(IEntityTargetAbleComponent target)
        {
            var casterPosition = entityCasterTargetingComponent.EntityTransform.position;
            var dir = target.EntityTransform.position - casterPosition;
            var projectilePrefab = GameObject.Instantiate(_projectilePrefab,casterPosition,Quaternion.Euler(dir));
            projectilePrefab.GetComponent<ProjectileAbilityEntity>().Init(ProjectileSpeed.CurrentValue,5,StatusEffects);

        }
    }
}