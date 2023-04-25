using Tzipory.StatusSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityCombatComponent : IEntityComponent
    {
        public IEntityHealthComponent Target { get; }
        
        public Status BaseAttackDamage { get; }
        public Status CritDamage { get; }
        public Status CritChance { get; }
        public Status AttackRate { get; }
        public Status AttackRange { get; }

        public void SetTarget(IEntityHealthComponent target);
    }
}