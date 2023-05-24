using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Shamans
{
    public class Shaman : BaseUnitEntity
    {
        private float _currentAttackRate;

        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Hero;
        }

        public override void Attack()
        {
            AbilityHandler.CastAbilityByName("AoeFire",TargetingHandler.AvailableTargets);
            
            bool canAttack = false;
            
            _currentAttackRate -= Time.deltaTime;
            
            if (_currentAttackRate < 0)
            {
                _currentAttackRate = AttackRate.CurrentValue;
                canAttack = true;
            }
            
            if(!canAttack)
                return;
            
            if (CritChance.CurrentValue > Random.Range(0, 100))
            {
                Target.TakeDamage(CritDamage.CurrentValue);
                return;
            }
            
            Target.TakeDamage(AttackDamage.CurrentValue);
            
            Debug.Log(
                $"{Target.EntityTransform.name} been attacked by {gameObject.name}, hp left {Target.HP.CurrentValue}");
            
        }
    }
}