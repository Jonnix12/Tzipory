using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityConfigSystem
{
    public abstract class BaseAbilityConfig : ScriptableObject
    {
        [SerializeField,Tooltip("")] private string _abilityName;
        [SerializeField,Tooltip("")] private float _cooldown;
        [SerializeField,Tooltip("")] private AbilityType _abilityType;
        [SerializeField,Tooltip("")] private AbilityActionType _abilityActionType;
        [SerializeField,Tooltip("")] private TargetType _targetType;
        [SerializeField,Tooltip("")] private EffectType _effectType;

        public string AbilityName => _abilityName;

        public float Cooldown => _cooldown;

        public AbilityType AbilityType => _abilityType;

        public AbilityActionType AbilityActionType => _abilityActionType;

        public TargetType TargetType => _targetType;

        public EffectType EffectType => _effectType;
    }
}