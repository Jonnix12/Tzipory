using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;

namespace Tzipory.EntitySystem.EntityConfigSystem
{

    [CreateAssetMenu(fileName = "NewEntityConfig", menuName = "ScriptableObjects/Entity/New Entity config", order = 0)]
    //Alon TBC - is this ok? is there a rule/convention that I maybe missed here?
    public class BaseUnitEntityConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilityConfigs;
        [SerializeField] private List<Stat> _stats;

        public List<Stat> Stats => _stats;

        public List<AbilityConfig> AbilityConfigs => _abilityConfigs;

        public void AddStat(StatConfig statConfig)
        {
            //Just adding the implied logic of this method

            //Check if this new stat is OK to add:
            //cases such as "a stat of the same type already existing on this unit. should it override or ignore?"
            //if this is relevant, my suggestion is to add a virtual (not an asbstact!) method to BaseUnitEntityConfig which looks like this:
            //_existingStatConfig.StackWithNewConfig(StatConfig toStack);
            //  1)Letting each Stat implement either the base virtual type of stacking
            //  2) 

            _stats.Add(statConfig);
        }
    }
}