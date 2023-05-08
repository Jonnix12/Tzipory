using System.Collections.Generic;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;

namespace EntitySystem.EntityConfigSystem
{
    //Alon TBC - is this ok? is there a rule/convention that I maybe missed here?
    [CreateAssetMenu(fileName = "Base Unit Entity Config", menuName = "ScriptableObjects/EntitySystem/Config/", order = 0)]
    public class BaseUnitEntityConfig : ScriptableObject
    {
        [SerializeField] private List<StatConfig> _stats;

        public List<StatConfig> Stats => _stats;

        public void AddStat(StatConfig statConfig)
        {
            //Just adding the implied logic of this method

            //Check if this new stat is OK to add:
            //cases such as "a stat of the same type already existing on this unit. should it override or ignore?"
            //if this is relevant, my suggestion is to add a virtual (not an asbstact!) method to BaseUnitEntityConfig which looks like this:
            //_existingStatConfig.StackWithNewConfig(StatConfig toStack);
            //letting each Stat implement either a base 

            _stats.Add(statConfig);
        }
    }
}