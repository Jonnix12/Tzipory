using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class OverTimeStatusEffect : BaseStatusEffect
    {
        private float _duration;
        
        public OverTimeStatusEffect(float duration,Stat stat, StatModifier[] modifiers) : base(stat, modifiers)
        {
            _duration = duration;
            StatusEffectStart();
        }

        protected sealed override void StatusEffectStart()
        {
            foreach (var statModifier in modifiers)
                statModifier.Process(stat);
            base.StatusEffectStart();
        }

        public override void Execute()
        {
            _duration -= Time.deltaTime;

            if (_duration < 0)
                StatusEffectFinish();
        }

        protected override void StatusEffectFinish()
        {
            foreach (var statModifier in modifiers)
                statModifier.UnDo(stat);
            base.StatusEffectFinish();
        }
    }
}