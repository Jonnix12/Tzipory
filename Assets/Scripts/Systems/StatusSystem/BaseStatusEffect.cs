using System;
using System.Collections.Generic;
using SerializeData.VisualSystemSerializeData;

namespace Tzipory.EntitySystem.StatusSystem
{
    public abstract class BaseStatusEffect
    {
        public event Action<int> OnStatusEffectStart;
        public event Action<int> OnStatusEffectDone;
        public event  Action<int> OnStatusEffectInterrupt;
        
        protected List<StatModifier> modifiers;
        
        protected Stat currentStat;

        public string StatusEffectName { get; }
        public string AffectedStatName { get; }
        public int StatusEffectId { get; }

        public EffectSequenceData EffectSequence { get; }

        public List<StatusEffectConfigSo> StatusEffectToInterrupt { get; }
        
        protected BaseStatusEffect(StatusEffectConfigSo statusEffectConfigSo)
        {
            StatusEffectName = statusEffectConfigSo.StatusEffectName;
            AffectedStatName = statusEffectConfigSo.AffectedStatName;
            StatusEffectId = statusEffectConfigSo.StatusEffectId;
            StatusEffectToInterrupt = statusEffectConfigSo.StatusEffectToInterrupt;
            EffectSequence = statusEffectConfigSo.EffectSequence;

            modifiers = new List<StatModifier>();

            foreach (var modifier in statusEffectConfigSo.StatModifier)
            {
                modifiers.Add(new StatModifier(modifier.Modifier, modifier.StatusModifierType));
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
        
        public abstract void ProcessStatusEffect();
    }
    
    public enum StatusEffectType
    {
        OverTime,
        Instant,
        Interval
    }
}