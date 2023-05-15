using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;

namespace Tzipory.EntitySystem.EntityConfigSystem
{
    [CreateAssetMenu(fileName = "NewEntityConfig", menuName = "ScriptableObjects/Entity/New Entity config", order = 0)]
    public class BaseUnitEntityConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilityConfigs;
        [SerializeField] private List<Stat> _stats;

        public List<Stat> Stats => _stats;

        public List<AbilityConfig> AbilityConfigs => _abilityConfigs;

        public void AddStat(StatConfig statConfig)
        {
            
        }
    }
}