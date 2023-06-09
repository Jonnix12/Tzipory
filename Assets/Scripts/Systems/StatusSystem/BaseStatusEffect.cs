﻿using System;
using System.Collections.Generic;
using SerializeData.VisualSystemSerializeData;

namespace Tzipory.EntitySystem.StatusSystem
{
    public abstract class BaseStatusEffect : IDisposable
    {
        #region Events

        public event Action<int> OnStatusEffectStart;
        public event Action<int> OnStatusEffectDone;
        public event  Action<int> OnStatusEffectInterrupt;

        #endregion

        #region Fields

        protected readonly Stat StatToEffect;
        
        protected readonly List<StatModifier> modifiers;

        #endregion

        #region Property

        public string StatusEffectName { get; }

        public string AffectedStatName => StatToEffect.Name;
        public int AffectedStatId => StatToEffect.Id;

        public bool IsDone { get; private set; }

        public EffectSequenceData EffectSequence { get; }

        public List<StatusEffectConfigSo> StatusEffectToInterrupt { get; }

        #endregion
       
        
        protected BaseStatusEffect(StatusEffectConfigSo statusEffectConfigSo,Stat statToEffectToEffect)
        {
            StatusEffectName = statusEffectConfigSo.StatusEffectName;
            StatusEffectToInterrupt = statusEffectConfigSo.StatusEffectToInterrupt;
            EffectSequence = statusEffectConfigSo.EffectSequence;

            StatToEffect = statToEffectToEffect;

            modifiers = new List<StatModifier>();

            foreach (var modifier in statusEffectConfigSo.StatModifier)
            {
                modifiers.Add(new StatModifier(modifier.Modifier, modifier.StatusModifierType));
            }
            //need to add 
        }

        public virtual void StatusEffectStart()
        {
            OnStatusEffectStart?.Invoke(AffectedStatId);
            IsDone  = false;
        }

        public virtual void StatusEffectInterrupt()
        {
            OnStatusEffectInterrupt?.Invoke(AffectedStatId);
        }

        protected virtual void StatusEffectFinish()
        {
            OnStatusEffectDone?.Invoke(AffectedStatId);
            IsDone = true;
        }

        public abstract void ProcessStatusEffect();

        public abstract void Dispose();
    }
    
    public enum StatusEffectType
    {
        OverTime,
        Instant,
        Interval
    }
}