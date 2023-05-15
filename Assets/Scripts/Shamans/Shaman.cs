using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Shamans
{
    public class Shaman : BaseUnitEntity
    {
        private float _currentAttackRate;

        public override void Attack()
        {
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
            
            //AbilityHandler.CastAbilityByName("AoeFire",TargetingHandler.AvailableTargets);
        }
    }
}