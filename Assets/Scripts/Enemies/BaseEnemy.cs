using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace Tzipory.Enemies
{
    public abstract class BaseEnemy : BaseGameEntity , IEntityHealthComponent , IEntityCombatComponent  ,IEntityMovementComponent
    {
        #region HealthComponent
        
        private float _hp;
        private float _maxHp;

        public float HP => _hp;
        public float MaxHP => _maxHp;

        public bool IsEntityDead => HP <= 0;

        public void Heal(float amount)
        {
            _hp += amount;
            if (_hp > MaxHP)
                _hp = MaxHP;
        }

        public void TakeDamage(float damage)
        {
            _hp  -= damage;
            if (_hp < 0)
            {
                //dead                
            }
        }

        #endregion

        #region CombatComponent
        
        private IEntityHealthComponent _target;

        public IEntityHealthComponent Target => _target;
        
        public float BaseAttackDamage { get; }
        public float AttackDamageMultiplier { get; }
        public float CritDamage { get; }
        public float CritChance { get; }
        public float AttackRate { get; }
        public float AttackRange { get; }
        
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