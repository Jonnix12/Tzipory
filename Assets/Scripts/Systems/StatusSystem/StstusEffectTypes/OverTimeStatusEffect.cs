using Tzipory.BaseSystem.TimeSystem;

namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class OverTimeStatusEffect : BaseStatusEffect
    {
        private Stat _duration;
        private float _currentDuration;
        
        public OverTimeStatusEffect(StatusEffectConfigSo statusEffectConfigSo) : base(statusEffectConfigSo)
        {
            _duration = new Stat("Duration", statusEffectConfigSo.Duration, int.MaxValue,999 );//temp need to find what to do 
            _currentDuration = _duration.CurrentValue;
        }


        public override void StatusEffectStart()
        {
            foreach (var statModifier in modifiers)
                statModifier.ProcessStatModifier(currentStat);
            base.StatusEffectStart();
        }

        public override void ProcessStatusEffect()
        {
            _currentDuration -= GAME_TIME.GameDeltaTime;

            if (_currentDuration < 0)
                StatusEffectFinish();
        }

        protected override void StatusEffectFinish()
        {
            foreach (var statModifier in modifiers)
                statModifier.Undo(currentStat);
            base.StatusEffectFinish();
        }
    }
}