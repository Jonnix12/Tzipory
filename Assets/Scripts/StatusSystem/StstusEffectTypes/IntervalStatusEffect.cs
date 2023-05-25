using System;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class IntervalStatusEffect : BaseStatusEffect
    {
        private const string INTERVAL_KEY = "Interval";
        private const string DURATION_KEY = "Duration";
        
        private readonly Stat _interval;
        private readonly Stat _duration;
        
        private float _currentInterval;
        private float _currentDuration;
        
        public IntervalStatusEffect(StatusEffectConfig statusEffectConfig) : base(statusEffectConfig)
        {

            if (statusEffectConfig.TryGetParameter(INTERVAL_KEY, out var intervalStat))
                _interval = new Stat(intervalStat.Name, intervalStat.BaseValue, intervalStat.MaxValue, intervalStat.Id);
            else
                throw new Exception($"{INTERVAL_KEY} was not found");
            if (statusEffectConfig.TryGetParameter(DURATION_KEY, out var durationStat))
                _duration = new Stat(durationStat.Name, durationStat.BaseValue, durationStat.MaxValue, intervalStat.Id);
            else
                throw new Exception($"{DURATION_KEY} was not found");

            _currentInterval = _interval.CurrentValue;
            _currentDuration = _duration.CurrentValue;
        }

        public override void StatusEffectStart()
        {
            foreach (var statModifier in modifiers)
                statModifier.Process(currentStat);
            
            base.StatusEffectStart();
        }

        public override void Execute()
        {
            _currentDuration -= Time.deltaTime;

            if (_currentDuration < 0)
            {
                StatusEffectFinish();
                return;
            }

            _currentInterval -= Time.deltaTime;
            if (_currentInterval <= 0)
            {
                _currentInterval = _interval.CurrentValue;
                foreach (var statModifier in modifiers)
                {
#if UNITY_EDITOR
                   // Debug.Log($"Execute effect {currentStat.Name} by {statModifier.Modifier.CurrentValue}");    
#endif
                    statModifier.Process(currentStat);
                }
            }
        }
    }
}