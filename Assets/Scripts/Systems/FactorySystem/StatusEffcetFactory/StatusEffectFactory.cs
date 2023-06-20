using System;
using Tzipory.EntitySystem.StatusSystem;

namespace Factory
{
    public class StatusEffectFactory
    {
        public static BaseStatusEffect GetStatusEffect(StatusEffectConfigSo statusEffectConfigSo)
        {
            return statusEffectConfigSo.StatusEffectType switch
            {
                StatusEffectType.OverTime => new OverTimeStatusEffect(statusEffectConfigSo),
                StatusEffectType.Instant => new InstantStatusEffect(statusEffectConfigSo),
                StatusEffectType.Interval => new IntervalStatusEffect(statusEffectConfigSo),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}