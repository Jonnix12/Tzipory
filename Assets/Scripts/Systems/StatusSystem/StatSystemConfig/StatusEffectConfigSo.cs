using System.Collections.Generic;
using Tzipory.VisualSystem.EffectSequence;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [CreateAssetMenu(fileName = "NewStatusEffectConfig", menuName = "ScriptableObjects/EntitySystem/StatusSystem/New status effect config", order = 0)]
    public class StatusEffectConfigSo : ScriptableObject
    {
        [Header("Status Effect Config")]
        [SerializeField, Tooltip("")] private string _statName;
        [SerializeField, Tooltip("")] private int _statusEffectId;
        [SerializeField, Tooltip("")] private List<Stat> _statusEffectParameter;
        [SerializeField, Tooltip("")] private List<StatusEffectConfigSo> _statusEffectToInterrupt;
        [Header("Stat Modifiers")] 
        [SerializeField, Tooltip("")] private StatusEffectType _statusEffectType;
        [SerializeField, Tooltip("")] private List<StatModifier> _statModifier;
        [Header("Status effect visual")]
        [SerializeField, Tooltip("")] private EffectSequence _effectSequence;

        public string StatName => _statName;

        public int StatusEffectId => _statusEffectId;

        public StatusEffectType StatusEffectType => _statusEffectType;

        public List<StatModifier> StatModifier => _statModifier;

        public List<StatusEffectConfigSo> StatusEffectToInterrupt => _statusEffectToInterrupt;

        public List<Stat> StatusEffectParameter => _statusEffectParameter;

        public EffectSequence EffectSequence => _effectSequence;

        public bool TryGetParameter(string name, out Stat stat)
        {
            stat = _statusEffectParameter.Find(x => x.Name == name);
            return stat != null;
        }

    }
}