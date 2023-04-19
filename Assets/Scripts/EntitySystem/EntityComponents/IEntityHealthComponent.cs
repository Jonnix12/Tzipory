namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityHealthComponent : IEntityComponent
    {
        public float HP { get; }
        public float MaxHP { get; }
        public bool IsEntityDead { get; }
        
        public void Heal(float amount);
        
        public void TakeDamage(float damage);
    }
}