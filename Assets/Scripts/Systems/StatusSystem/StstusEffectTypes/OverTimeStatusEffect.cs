using System;
using Tzipory.BaseSystem.TimeSystem;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class OverTimeStatusEffect : BaseStatusEffect
    {
        private const string DURATION_KEY = "Duration";
        private Stat _duration;
        private float _currentDuration;
        
        public OverTimeStatusEffect(StatusEffectConfigSo statusEffectConfigSo) : base(statusEffectConfigSo)
        {
            if (statusEffectConfigSo.TryGetParameter(DURATION_KEY,out var stat))
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
            _currentDuration -= GAME_TIME.GameDeltaTime;

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