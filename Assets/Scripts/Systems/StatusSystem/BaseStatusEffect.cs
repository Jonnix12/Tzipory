using System;
using System.Collections.Generic;
using Tzipory.VisualSystem.EffectSequence;

namespace Tzipory.EntitySystem.StatusSystem
{
    public abstract class BaseStatusEffect
    {
        public event Action<int> OnStatusEffectStart;
        public event Action<int> OnStatusEffectDone;
        public event  Action<int> OnStatusEffectInterrupt;
        
        protected List<StatModifier> modifiers;
        
        protected Stat currentStat;

        public string StatName { get; }
        public int StatusEffectId { get; }

        public EffectSequence EffectSequence { get; }

        public List<StatusEffectConfigSo> StatusEffectToInterrupt { get; }
        
        protected BaseStatusEffect(StatusEffectConfigSo statusEffectConfigSo)
        {
            StatName = statusEffectConfigSo.AffectedStatName;
            StatusEffectId = statusEffectConfigSo.StatusEffectId;
            StatusEffectToInterrupt = statusEffectConfigSo.StatusEffectToInterrupt;
            EffectSequence = statusEffectConfigSo.EffectSequence;

            modifiers = new List<StatModifier>();

            foreach (var modifier in statusEffectConfigSo.StatModifier)
            {
                modifiers.Add(new StatModifier(modifier.Modifier, modifier.ModifierType));
            }
            //need to add 
        }

        public virtual void StatusEffectStart()
        {
            OnStatusEffectStart?.Invoke(StatusEffectId);
        }

        public virtual void StatusEffectInterrupt()
        {
            OnStatusEffectInterrupt?.Invoke(StatusEffectId);
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