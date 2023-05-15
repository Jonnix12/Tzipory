using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;

namespace Tzipory.AbilitiesSystem
{
    public class ProjectileAbility : BaseAbility
    {
        public Stat ProjectileSpeed { get;}
        
        public ProjectileAbility(IEntityTargetAbleComponent entityCaster, AbilityConfig config) : base(entityCaster, config)
        {
            if (StatsValue.TryGetValue("ProjectileSpeed",out Stat projectileSpeed))
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