using Tzipory.EntitySystem.EntityComponents;

namespace AbilitiesSystem
{
    public abstract class BaseEntityAbility
    {
        protected TargetType _targetType;
        protected EffectType _effectType;

        protected IEntityTargetAbleComponent _target;

        protected BaseEntityAbility(IEntityTargetAbleComponent target)
        {
            
        }
        
        public abstract void Cast();
    }

    public enum TargetType
    {
        Self,
        Enemy,
        Ally,
    }
    
    public enum EffectType
    {
        Positive,
        Negative,
    }
}