
namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class InstantStatusEffect : BaseStatusEffect
    {
        public InstantStatusEffect(StatusEffectConfig statusEffectConfig) : base(statusEffectConfig)
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