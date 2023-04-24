using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.TargetingSystem;
using UnityEngine;

namespace shamans
{
    public class Shaman : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent
    {

        #region UnityCallBacks

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion

        #region AbilitieComponent

        

        #endregion
        
        #region TargetingComponent

        public IPriority DefaultPriority { get; }
        public ITargeting TargetingHandler { get; set; }
        public void SetTargeting(ITargeting targeting) => 
            TargetingHandler = targeting;
        
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

        private float _damageStatusEffactMuitiplier;
        
        private float _baseDamage;
        
        private IEntityHealthComponent _target;

        public IEntityHealthComponent Target => _target;

        public float BaseAttackDamage => _baseDamage * _damageStatusEffactMuitiplier;
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