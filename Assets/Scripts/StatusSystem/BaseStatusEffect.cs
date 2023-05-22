using System;
using System.Collections.Generic;

namespace Tzipory.EntitySystem.StatusSystem
{
    public abstract class BaseStatusEffect
    {
        public event Action<int> OnStatusEffectStart;
        public event Action<int> OnStatusEffectDone;

        protected List<StatModifier> modifiers;

        public string StatName { get; }
        public int StatusEffectId { get; }

        protected Stat currentStat;

        protected BaseStatusEffect(StatusEffectConfig statusEffectConfig)
        {
            StatName = statusEffectConfig.StatName;
            StatusEffectId = statusEffectConfig.StatId;
            modifiers = new List<StatModifier>();

            foreach (var modifier in statusEffectConfig.StatModifier)
            {
                modifiers.Add(new StatModifier(modifier.Modifier, modifier.ModifierType));
            }
            //need to add 
        }

        public virtual void StatusEffectStart()
        {
            OnStatusEffectStart?.Invoke(StatusEffectId);
        }

        protected virtual void StatusEffectFinish()
        {
            OnStatusEffectDone?.Invoke(StatusEffectId);
        }

        public void SetStat(Stat stat)
        {
            currentStat = stat;
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