
namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class InstantStatusEffect : BaseStatusEffect
    {
        public InstantStatusEffect(StatusEffectConfigSo statusEffectConfigSo) : base(statusEffectConfigSo)
        {
        }

        public override void ProcessStatusEffect()
        {
            foreach (var statModifier in modifiers)
                statModifier.ProcessStatModifier(currentStat);

            StatusEffectFinish();
        }
    }
}