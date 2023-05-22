using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.AbilitiesSystem
{
    public class ProjectileAbility : BaseAbility
    {
        public Stat ProjectileSpeed { get;}
        
        public ProjectileAbility(IEntityTargetingComponent entityCasterTargetingComponent, AbilityConfig config) : base(entityCasterTargetingComponent,config)
        {
            if (AbilityParameter.TryGetValue("ProjectileSpeed",out Stat projectileSpeed))
                ProjectileSpeed = projectileSpeed;
            else
                throw new System.Exception($"{nameof(ProjectileAbility)} ProjectileSpeed not found");
        }

        protected override void Cast(IEntityTargetAbleComponent target)
        {
            throw new System.NotImplementedException();
        }
    }
}