using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.StatusSystem;
using UnityEngine;

namespace Tzipory.Enemies
{
    public abstract class BaseEnemy : BaseGameEntity , IEntityHealthComponent , IEntityCombatComponent  ,IEntityMovementComponent
    {
        #region HealthComponent
        
        public Status HP { get; }
        public bool IsEntityDead => HP.CurrentValue <= 0;

        public void Heal(float amount)
        {
            HP.AddToValue(amount);
            if (HP.CurrentValue > HP.BaseValue)
                HP.ResetValue();
        }

        public void TakeDamage(float damage)
        {
            HP.ReduceFromValue(damage);
            if (HP.CurrentValue < 0)
            {
                //dead                
            }
        }

        #endregion

        #region CombatComponent
        
        private IEntityHealthComponent _target;

        public IEntityHealthComponent Target => _target;
        public Status BaseAttackDamage { get; }
        public Status CritDamage { get; }
        public Status CritChance { get; }
        public Status AttackRate { get; }
        public Status AttackRange { get; }


        public void SetTarget(IEntityHealthComponent target)
        {
            _target = target;
        }

        #endregion
        
        #region MovementComponent

        public float MoveSpeed { get; }
        
        public void SetDestination(Vector2 destination, MoveType moveType)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}