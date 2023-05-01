using Tzipory.StatusSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityStatusEffectComponent
    {
        public StatusEffectHandler StatusEffectHandler { get; }
    }
}