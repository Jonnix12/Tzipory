using System;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class OverTimeStatusEffect : BaseStatusEffect
    {
        private const string DURATION_KEY = "Duration";
        private Stat _duration;
        private float _currentDuration;
        
        public OverTimeStatusEffect(StatusEffectConfig statusEffectConfig) : base(statusEffectConfig)
        {
            if (statusEffectConfig.TryGetParameter(DURATION_KEY,out var stat))
                _duration = new Stat(stat.Name,stat.BaseValue,stat.MaxValue,stat.Id);
            else
                throw new Exception($"{DURATION_KEY} was not found");

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
                StatusEffectFinish();
        }

        protected override void StatusEffectFinish()
        {
            foreach (var statModifier in modifiers)
                statModifier.UnDo(currentStat);
            base.StatusEffectFinish();
        }
    }
}