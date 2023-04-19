using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.TargetingSystem;
using UnityEngine;

namespace shamans
{
    public class Shaman : BaseGameEntity , ICombatTargetableComponent , IEntityCombatComponent , IEntityMovementComponent , ITargetingComponent
    {

        #region AbilitieComponent

        

        #endregion
        
        #region TargetingComponent

        private ITargetingHandler _targetingHandler;

        public ITargetingHandler TargetingHandler => _targetingHandler;
        
        private void OnCollisionEnter2D(Collision2D other)//need to be Trigger
        {
            if (other.gameObject.TryGetComponent(out ICombatTargetableComponent targetableComponent))
                _targetingHandler.AddTarget(targetableComponent);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out ICombatTargetableComponent targetableComponent))
                _targetingHandler.RemoveTarget(targetableComponent);
        }
        
        public void SetTargetingHandler(ITargetingHandler targetingHandler) =>
            _targetingHandler = targetingHandler;
        
        #endregion

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

        private float _DamagestatusEffactMuitiplier;
        
        private float _baseDamage;
        
        private IEntityHealthComponent _target;

        public IEntityHealthComponent Target => _target;

        public float BaseAttackDamage => _baseDamage * _DamagestatusEffactMuitiplier;
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