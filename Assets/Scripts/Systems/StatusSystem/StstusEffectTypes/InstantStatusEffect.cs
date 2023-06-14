
namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class InstantStatusEffect : BaseStatusEffect
    {
        public InstantStatusEffect(StatusEffectConfigSo statusEffectConfigSo) : base(statusEffectConfigSo)
        {
        }

        public override void Execute()
        {
            foreach (var statModifier in modifiers)
                statModifier.Process(currentStat);

            StatusEffectFinish();
        }
    }
}