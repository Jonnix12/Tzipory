using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;

namespace Tzipory.EntitySystem.EntityConfigSystem
{
    [CreateAssetMenu(fileName = "NewEntityConfig", menuName = "ScriptableObjects/Entity/New Entity config", order = 0)]
    public class BaseUnitEntityConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilityConfigs;
        [SerializeField] private List<StatConfig> _stats;

        public List<StatConfig> Stats => _stats;

        public List<AbilityConfig> AbilityConfigs => _abilityConfigs;

        public void AddStat(StatConfig statConfig)
        {
            
        }
    }
}