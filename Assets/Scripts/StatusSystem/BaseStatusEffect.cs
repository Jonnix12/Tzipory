using System;

namespace Tzipory.StatusSystem
{
    public abstract class BaseStatusEffect
    {
        public event Action OnStatusEffectDone; 

        private RunTimeType _runTimeType;
        
        private float _duration;
        private float _interval;

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