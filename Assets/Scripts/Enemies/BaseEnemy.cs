using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.StatusSystem;
using UnityEngine;

namespace Tzipory.Enemies
{
    public abstract class BaseEnemy : BaseGameEntity , IEntityHealthComponent , IEntityCombatComponent  ,IEntityMovementComponent
    {
        #region HealthComponent
        
        public Stat HP { get; }
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
        public Stat BaseAttackDamage { get; }
        public Stat CritDamage { get; }
        public Stat CritChance { get; }
        public Stat AttackRate { get; }
        public Stat AttackRange { get; }


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