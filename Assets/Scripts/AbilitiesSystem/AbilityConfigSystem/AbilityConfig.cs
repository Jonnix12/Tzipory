using System.Collections.Generic;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityConfigSystem
{
    [CreateAssetMenu(fileName = "NewAbilityConfig", menuName = "ScriptableObjects/EntitySystem/AbilitySystem/New ability config", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        [Header("Ability config")]
        [SerializeField,Tooltip("")] private int _abilityId;
        [SerializeField,Tooltip("")] private string _abilityName;
        [SerializeField,Tooltip("")] private AbilityType _abilityType;
        [SerializeField,Tooltip("")] private AbilityActionType _abilityActionType;
        [SerializeField,Tooltip("")] private TargetType _targetType;
        [SerializeField,Tooltip("")] private EffectType _effectType;
        [Header("Ability parameters")]
        [SerializeField,Tooltip("")] private StatConfig _cooldown;
        [SerializeField,Tooltip("")] private StatConfig _castTime;
        [SerializeField,Tooltip("")] private List<StatConfig> _statsConfig;
        [SerializeField,Tooltip("")] private List<StatusEffectConfig> _statusEffect;
        
        public List<StatConfig> StatsConfig => _statsConfig;
        
        public string AbilityName => _abilityName;

        public StatConfig Cooldown => _cooldown;

        public int AbilityId => _abilityId;

        public StatConfig CastTime => _castTime;

        public AbilityType AbilityType => _abilityType;

        public AbilityActionType AbilityActionType => _abilityActionType;

        public TargetType TargetType => _targetType;

        public EffectType EffectType => _effectType;
        
        public List<StatusEffectConfig> StatusEffect => _statusEffect;
    }

    public enum AbilityType
    {
        AOE,
        Single,
        Projectile
    }
}