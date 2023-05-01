using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using UnityEngine;

namespace Tzipory.EntitySystem.Entitys
{
    public abstract class BaseUnitEntity : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent
    {

        #region UnityCallBacks

        protected override void Awake()
        {
            base.Awake();
            TargetingHandler = new TargetingHandler(this);
        }
        protected virtual void Update()
        {
            HealthComponentUpdate();
        }

        #endregion

        #region TargetingComponent

        public IPriority DefaultPriority { get; }
        public ITargeting TargetingHandler { get; set; }
        public void SetTargeting(ITargeting targeting) => 
            TargetingHandler = targeting;
        
        #endregion

        #region HealthComponent
        
        private float  _currentInvincibleTime;

        public float InvincibleTime { get; }
        public bool IsDamageable { get; private set; }
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
            if (IsDamageable)
            {
                HP.ReduceFromValue(damage);
                IsDamageable = false;
            }

            if (HP.CurrentValue < 0)
            {
                //dead                
            }
        }

        private void HealthComponentUpdate()
        {
            if (!IsDamageable)
            {
                _currentInvincibleTime -= Time.deltaTime;

                if (_currentInvincibleTime < 0)
                {
                    IsDamageable = true;
                    _currentInvincibleTime = InvincibleTime;
                }
            }
        }

        #endregion

        #region CombatComponent                                                                                                                                   
        
        private float _baseDamage;
        
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

        #region StstusEffcetComponent

        //stuff
        public StatusEffectHandler StatusEffectHandler { get; }

        #endregion

    }
}