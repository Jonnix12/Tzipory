namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityCombatComponent : IEntityComponent
    {
        public IEntityHealthComponent Target { get; }
        
        public float BaseAttackDamage { get; }
        public float AttackDamageMultiplier { get; }
        public float CritDamage { get; }
        public float CritChance { get; }
        public float AttackRate { get; }
        public float AttackRange { get; }

        public void SetTarget(IEntityHealthComponent target);
    }
}