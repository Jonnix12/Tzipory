using System;
using System.Collections.Generic;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tzipory.AbilitiesSystem.AbilityConfigSystem
{
    public abstract class BaseAbilityConfig : ScriptableObject
    {
        [SerializeField,Tooltip("")] private string _abilityName;
        [SerializeField,Tooltip("")] private AbilityType _abilityType;
        [SerializeField,Tooltip("")] private AbilityActionType _abilityActionType;
        [SerializeField,Tooltip("")] private TargetType _targetType;
        [SerializeField,Tooltip("")] private EffectType _effectType;
        [SerializeField,Tooltip("")] private StatConfig _cooldown;
        [FormerlySerializedAs("_stats")] [SerializeField,Tooltip("")] private List<StatConfig> statsConfig;

        public abstract Type Type { get; }

        public List<StatConfig> StatsConfig => statsConfig;
        
        public string AbilityName => _abilityName;

        public StatConfig Cooldown => _cooldown;

        public AbilityType AbilityType => _abilityType;

        public AbilityActionType AbilityActionType => _abilityActionType;

        public TargetType TargetType => _targetType;

        public EffectType EffectType => _effectType;
    }
}