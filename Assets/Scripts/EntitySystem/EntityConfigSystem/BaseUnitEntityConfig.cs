using System.Collections.Generic;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;

namespace EntitySystem.EntityConfigSystem
{
    public class BaseUnitEntityConfig : ScriptableObject
    {
        [SerializeField] private List<StatConfig> _stats;

        public List<StatConfig> Stats => _stats;

        public void AddStat(StatConfig statConfig)
        {
            
        }
    }
}