using System.Collections.Generic;
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
        [SerializeField,Tooltip("")] private AbilityType _abilityType;
        [SerializeField,Tooltip("")] private CastType _castType;
        [Header("Targeting")]
        [SerializeField,Tooltip("")] private TargetingPriority _targetingPriority;
        [SerializeField,Tooltip("")] private EffectType _effectType;
        [Header("Ability parameters")]
        [SerializeField,Tooltip("")] private Stat _cooldown;
        [SerializeField,Tooltip("")] private Stat _castTime;
        [FormerlySerializedAs("_statsConfig")] [SerializeField,Tooltip("")] private List<Stat> abilityParameter;
        [FormerlySerializedAs("_statusEffect")] [SerializeField,Tooltip("")] private List<StatusEffectConfigSo> _statusEffectConfigs;
        
        public string AbilityName => _abilityName;
        public int AbilityId => _abilityId;
        public AbilityType AbilityType => _abilityType;
        public TargetingPriority TargetingPriority => _targetingPriority;
        public EffectType EffectType => _effectType;
        public Stat Cooldown => _cooldown;
        public Stat CastTime => _castTime;
        public List<Stat> AbilityParameter => abilityParameter;
        public List<StatusEffectConfigSo> StatusEffectConfigs => _statusEffectConfigs;
    }

    public enum AbilityType
    {
        AOE,
        Single,
        Chain
    }
    
    public enum CastType
    {
        Projectile,
        Instant,
        Self
    }

    public enum TargetingPriority
    {
        ClosesToEntity
    }
    
    public enum AbilityActionType
    {
        Heal,
        Damage
    }

    public enum TargetType
    {
        Self,
        Enemy,
        Ally,
    }
    
    public enum EffectType
    {
        Positive,
        Negative,
    }
}