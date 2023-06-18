

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityTargetAbleComponent : IEntityHealthComponent , IEntityStatusEffectComponent
    {
        public EntityTeamType EntityTeamType { get; }
    }

    public enum EntityTeamType
    {
        Hero,
        Enemy
    }
}