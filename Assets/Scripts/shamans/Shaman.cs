using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.StatusSystem;
using UnityEngine;

namespace Tzipory.shamans
{
    public class Shaman : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent
    {

        #region UnityCallBacks

        protected override void Awake()
        {
            base.Awake();
            TargetingHandler = new TargetingHandler(this);
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

        private float _damageStatusEffactMuitiplier;
        
        private float _baseDamage;
        
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