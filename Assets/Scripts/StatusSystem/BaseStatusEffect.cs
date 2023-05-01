using System;

namespace Tzipory.StatusSystem
{
    public abstract class BaseStatusEffect
    {
        public event Action OnStatusEffectStart;
        public event Action OnStatusEffectDone; 

        private RunTimeType _runTimeType;
        
        protected float duration;
        protected float interval;
        
        public virtual void StatusEffectStart()
        {
            OnStatusEffectStart?.Invoke();
        }

        public abstract void Execute();

        public virtual void StatusEffectFinish()
        {
            OnStatusEffectDone?.Invoke();
        }
    }
    
    public enum RunTimeType
    {
        OverTime,
        Instant,
        Interval
    }
}