using System.Collections.Generic;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [CreateAssetMenu(fileName = "NewStatusEffectConfig", menuName = "ScriptableObjects/EntitySystem/StatusSystem/New status effect config", order = 0)]
    public class StatusEffectConfig : ScriptableObject
    {
        [Header("Status Effect Config")]
        [SerializeField, Tooltip("")] private string _statName;
        [SerializeField, Tooltip("")] private int _statId;
        [SerializeField, Tooltip("")] private List<Stat> _statusEffectParameter;
        [Header("Stat Modifiers")] 
        [SerializeField, Tooltip("")] private StatusEffectType _statusEffectType;
        [SerializeField, Tooltip("")] private List<StatModifier> _statModifier;

        public string StatName => _statName;

        public int StatId => _statId;

        public StatusEffectType StatusEffectType => _statusEffectType;

        public List<StatModifier> StatModifier => _statModifier;

        public bool TryGetParameter(string name, out Stat stat)
        {
            stat = _statusEffectParameter.Find(x => x.Name == name);
            return stat != null;
        }

    }
}