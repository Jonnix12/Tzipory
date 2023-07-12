namespace Helpers.Consts
{
    public static class Constant
    {
        public static class EffectSequenceIds
        {
            public const int OnDeath = 1;
            public const int OnAttack = 2;
            public const int OnCritAttack = 3;
            public const int OnMove = 4;
            public const int OnGetHit = 5;
            public const int OnGetCritHit = 6;
        }

        public enum Stats
        {
            Health,
            AttackDamage,
            AttackRate,
            AttackRange,
            TargetingRange,
            MovementSpeed,
            CritDamage,
            CritChance,
            InvincibleTime,
            AbilityCooldown,
            AbilityCastTime,
            ProjectileSpeed,
            ProjectilePenetration,
            AoeRadius,
            AoeDuration,
            ChainRadius,
            ChainDuration,
            ChainAmount,
        }
    }

    
}