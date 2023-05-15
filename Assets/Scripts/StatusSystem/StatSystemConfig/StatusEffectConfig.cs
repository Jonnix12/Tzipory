using System.Collections.Generic;
using StatusSystem.StatSystemConfig;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [CreateAssetMenu(fileName = "NewStatusEffectConfig", menuName = "ScriptableObjects/EntitySystem/StatusSystem/New status effect config", order = 0)]
    public class StatusEffectConfig : ScriptableObject
    {
        [Header("Stat config")]
        [SerializeField, Tooltip("")] private string _statName;
        [SerializeField, Tooltip("")] private int _statId;

        [Header("Stat Modifiers")] 
        [SerializeField, Tooltip("")] private StatusEffectType _statusEffectType;
        [SerializeField, Tooltip("")] private List<StatModifierConfig> _statModifier;
    }
}