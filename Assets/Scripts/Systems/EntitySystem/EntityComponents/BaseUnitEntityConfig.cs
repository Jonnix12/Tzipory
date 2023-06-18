using System.Collections.Generic;
using Helpers.Consts;
using Sirenix.OdinInspector;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.StatusSystem.StatSystemConfig;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tzipory.EntitySystem.EntityConfigSystem
{

    [CreateAssetMenu(fileName = "NewEntityConfig", menuName = "ScriptableObjects/Entity/New Entity config", order = 0)]
    //Alon TBC - is this ok? is there a rule/convention that I maybe missed here?
    public class BaseUnitEntityConfig : ScriptableObject
    {
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _health;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _invincibleTime;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _attackDamage;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _AttackRate;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _attackRange;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _CritDamage;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _CritChance;
        [FormerlySerializedAs("_moveSpeed")] [SerializeField,Tooltip(""),TabGroup("Stats")] private Stat _movementSpeed;
        [SerializeField,Tooltip(""),TabGroup("Stats")] private List<Stat> _stats;
        [SerializeField,TabGroup("Abilities")] private List<AbilityConfig> _abilityConfigs;
        [SerializeField] private TargetingPriorityType _targetingPriority;
        
        public List<Stat> Stats => _stats;

        public Stat Health => _health;


        public Stat InvincibleTime => _invincibleTime;

        public Stat AttackDamage => _attackDamage;

        public Stat AttackRate => _AttackRate;

        public Stat AttackRange => _attackRange;

        public Stat CritDamage => _CritDamage;

        public Stat CritChance => _CritChance;

        public Stat MovementSpeed => _movementSpeed;

        public TargetingPriorityType TargetingPriority => _targetingPriority;

        public List<AbilityConfig> AbilityConfigs => _abilityConfigs;

        private void OnValidate()
        {
            _health.Name = Constant.StatNames.Health;
            _invincibleTime.Name = Constant.StatNames.InvincibleTime;
            _attackDamage.Name = Constant.StatNames.AttackDamage;
            _AttackRate.Name = Constant.StatNames.AttackRate;
            _attackRange.Name = Constant.StatNames.AttackRange;
            _CritDamage.Name = Constant.StatNames.CritDamage;
            _CritChance.Name = Constant.StatNames.CritChance;
            _movementSpeed.Name = Constant.StatNames.MovementSpeed;

            _health.Id = Constant.StatIds.Health;
            _invincibleTime.Id = Constant.StatIds.InvincibleTime;
            _attackDamage.Id = Constant.StatIds.AttackDamage;
            _AttackRate.Id = Constant.StatIds.AttackRate;
            _attackRange.Id = Constant.StatIds.AttackRange;
            _CritDamage.Id = Constant.StatIds.CritDamage;
            _CritChance.Id = Constant.StatIds.CritChance;
            _movementSpeed.Id = Constant.StatIds.MovementSpeed;
        }

        public void AddStat(StatConfig statConfig)
        {
            //Just adding the implied logic of this method

            //Check if this new stat is OK to add:
            //cases such as "a stat of the same type already existing on this unit. should it override or ignore?"
            //if this is relevant, my suggestion is to add a virtual (not an asbstact!) method to BaseUnitEntityConfig which looks like this:
            //_existingStatConfig.StackWithNewConfig(StatConfig toStack);
            //  1)Letting each Stat implement either the base virtual type of stacking
            //  2) 

            //_stats.Add(statConfig);
        }
    }
}