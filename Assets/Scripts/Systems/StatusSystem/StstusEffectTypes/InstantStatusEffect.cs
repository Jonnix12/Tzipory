
namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class InstantStatusEffect : BaseStatusEffect
    {
        public InstantStatusEffect(StatusEffectConfigSo statusEffectConfigSo,Stat statToEffectToEffect) : base(statusEffectConfigSo,statToEffectToEffect)
        {
        }

        public override void ProcessStatusEffect()
        {
            foreach (var statModifier in modifiers)
                statModifier.ProcessStatModifier(StatToEffect);

            StatusEffectFinish();
        }

        public override void Dispose()
        {
            foreach (var statModifier in modifiers)
                statModifier.Undo(StatToEffect);
        }
    }
}