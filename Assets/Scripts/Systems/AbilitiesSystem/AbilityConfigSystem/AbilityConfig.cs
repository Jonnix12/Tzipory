using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tzipory.AbilitiesSystem.AbilityConfigSystem
{
    [CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "ScriptableObjects/EntitySystem/AbilitySystem/New ability config", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField,Tooltip("")] private int _abilityId;
        [SerializeField,Tooltip("")] private string _abilityName;
        [Header("Ability config")]
        [SerializeField,Tooltip("")] private AbilityCastType abilityCastType;
        [SerializeField,ShowIf("abilityCastType",AbilityCastType.Projectile)] private float _projectileSpeed;
        [SerializeField,ShowIf("abilityCastType",AbilityCastType.Projectile)] private float _projectilePenetration;
        
        [SerializeField,Tooltip("")] private AbilityExecuteType abilityExecuteType;
        [SerializeField,ShowIf("abilityExecuteType",AbilityExecuteType.AOE)] private float _aoeRadius;
        [SerializeField,ShowIf("abilityExecuteType",AbilityExecuteType.AOE)] private float _aoeDuration;
        [SerializeField,ShowIf("abilityExecuteType",AbilityExecuteType.Chain)] private float _chainRadius;
        [SerializeField,ShowIf("abilityExecuteType",AbilityExecuteType.Chain)] private float _chainDuration;
        [SerializeField,ShowIf("abilityExecuteType",AbilityExecuteType.Chain)] private float _chainAmount;
        
        [Header("Targeting")]
        [SerializeField,Tooltip("")] private TargetingPriorityType targetingPriorityType;
        [Header("Ability parameters")]
        [SerializeField,Tooltip("")] private float _cooldown;
        [SerializeField,Tooltip("")] private float _castTime;
        [SerializeField,Tooltip("")] private List<StatusEffectConfigSo> _statusEffectConfigs;
        
        public string AbilityName => _abilityName;
        public int AbilityId => _abilityId;
        public AbilityExecuteType AbilityExecuteType => abilityExecuteType;
        public AbilityCastType AbilityCastType => abilityCastType;

        public TargetingPriorityType TargetingPriorityType => targetingPriorityType;
        
        public float Cooldown => _cooldown;
        public float CastTime => _castTime;
        public List<StatusEffectConfigSo> StatusEffectConfigs => _statusEffectConfigs;
    }

    public enum AbilityExecuteType
    {
        AOE,
        Single,
        Chain
    }
    
    public enum AbilityCastType
    {
        Projectile,
        Instant,
        Self
    }

    public enum TargetingPriorityType
    {
        Default,
        Random,
        ClosesToEntity,
        FarthestFromEntity,
        ClosesToCore,
        FarthestFromCore,
        LowestHpEntity,
        HighestHpEntity,
        LowestHpPercentageEntity,
        HighestHpPercentageEntity,
    }

    public enum EffectType
    {
        Positive,
        Negative,
    }
}