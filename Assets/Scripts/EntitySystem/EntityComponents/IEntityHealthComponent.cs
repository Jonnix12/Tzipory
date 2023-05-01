using Tzipory.StatusSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityHealthComponent : IEntityComponent
    {
        public Stat HP { get; }
        public bool IsEntityDead { get; }
        
        public void Heal(float amount);
        
        public void TakeDamage(float damage);
    }
}