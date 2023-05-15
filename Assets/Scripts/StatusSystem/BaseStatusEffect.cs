using System;

namespace Tzipory.EntitySystem.StatusSystem
{
    public abstract class BaseStatusEffect
    {
        public event Action<int> OnStatusEffectStart;
        public event Action<int> OnStatusEffectDone;

        protected Stat stat;
        protected StatModifier[] modifiers;

        public int StatusEffectId => stat.Id;

        protected BaseStatusEffect(Stat stat,StatModifier[] modifiers)
        {
            this.stat = stat;
            this.modifiers = modifiers;
        }

        protected virtual void StatusEffectStart()
        {
            OnStatusEffectStart?.Invoke(StatusEffectId);
        }
        
        protected virtual void StatusEffectFinish()
        {
            OnStatusEffectDone?.Invoke(StatusEffectId);
        }
        
        public abstract void Execute();
    }
    
    public enum StatusEffectType
    {
        OverTime,
        Instant,
        Interval
    }
}