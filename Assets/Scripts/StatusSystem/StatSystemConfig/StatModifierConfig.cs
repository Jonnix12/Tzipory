using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace StatusSystem.StatSystemConfig
{
    [CreateAssetMenu(fileName = "NewStatModifierConfig", menuName = "ScriptableObjects/EntitySystem/StatusSystem/New stat modifier config", order = 0)]
    public class StatModifierConfig : ScriptableObject
    {
        [SerializeField, Tooltip("")] private StatusModifierType _statusModifierType;
        [SerializeField, Tooltip("")] private Stat _modifier;

        public StatusModifierType StatusModifierType => _statusModifierType;

        public Stat Modifier => _modifier;
    }
}