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
        
        public static class StatIds
        {
            public const int Health = 1;
            public const int AttackDamage = 2;
            public const int AttackRate = 3;
            public const int AttackRange = 4;
            public const int MovementSpeed = 5;
            public const int CritDamage = 6;
            public const int CritChance = 7;
            public const int InvincibleTime = 8;
            public const int AbilityCooldown = 9;
            public const int AbilityCastTime = 10;
            public const int ProjectileSpeed = 11;
            public const int ProjectilePenetration = 12;
            public const int AoeRadius = 13;
            public const int AoeDuration = 14;
            public const int ChainRadius = 15;
            public const int ChainDuration = 16;
            public const int ChainAmount = 17;
        }
        
        public enum Stats
        {
            Health,
            AttackDamage,
            AttackRate,
            AttackRange,
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
        
        public static class StatNames
        {
            public const string Health = "Health";
            public const string AttackDamage = "AttackDamage";
            public const string AttackRate = "AttackRate";
            public const string AttackRange = "AttackRange";
            public const string MovementSpeed = "MovementSpeed";
            public const string CritDamage = "CritDamage";
            public const string CritChance = "CritChance";
            public const string InvincibleTime = "InvincibleTime";
            public const string AbilityCooldown = "Cooldown";
            public const string AbilityCastTime = "CastTime";
        }
    }

    
}