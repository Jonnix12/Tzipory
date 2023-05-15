
namespace Tzipory.EntitySystem.StatusSystem
{
    internal sealed class InstantStatusEffect : BaseStatusEffect
    {
        public InstantStatusEffect(Stat stat, StatModifier[] statModifiers) : base(stat,statModifiers)
        {
            StatusEffectStart();
        }

        public override void Execute()
        {
            foreach (var statModifier in modifiers)
                statModifier.Process(stat);

            StatusEffectFinish();
        }
    }
}