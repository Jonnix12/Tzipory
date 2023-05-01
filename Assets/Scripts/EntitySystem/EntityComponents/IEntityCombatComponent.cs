using Tzipory.EntitySystem.StatusSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityCombatComponent : IEntityComponent
    {
        public IEntityHealthComponent Target { get; }
        
        public Stat BaseAttackDamage { get; }
        public Stat CritDamage { get; }
        public Stat CritChance { get; }
        public Stat AttackRate { get; }
        public Stat AttackRange { get; }

        public void SetTarget(IEntityHealthComponent target);
    }
}