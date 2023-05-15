using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class IntervalStatusEffect : BaseStatusEffect
    {
        private readonly float _interval;
        private float _duration;
        private float _currentInterval;
        
        public IntervalStatusEffect(float duration, float interval, Stat stat, StatModifier[] statModifiers) : base(stat,statModifiers)
        {
            _duration = duration;
            _interval  = interval;
            _currentInterval = interval;
        }

        protected override void StatusEffectStart()
        {
            foreach (var statModifier in modifiers)
                statModifier.Process(stat);
            
            base.StatusEffectStart();
        }

        public override void Execute()
        {
            _duration -= Time.deltaTime;

            if (_duration < 0)
            {
                StatusEffectFinish();
                return;
            }

            _currentInterval -= Time.deltaTime;
            if (_currentInterval <= 0)
            {
                _currentInterval = _interval;
                foreach (var statModifier in modifiers)
                    statModifier.Process(stat);
            }
        }
    }
}