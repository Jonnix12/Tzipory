using System.Collections.Generic;
using EntitySystem.EntityConfigSystem;
using Tzipory.AbilitiesSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using UnityEngine;

namespace Tzipory.EntitySystem.Entitys
{
    public abstract class BaseUnitEntity : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent , IEntityAbilitiesComponent
    {
        #region Fields

        private const string HEALTH = "Hp";
        private const string INVINCIBLE_TIME = "Invincible Time";
        private const string ATTACK_DAMAGE = "Attack Damage";
        private const string CRIT_DAMAGE = "Crit Damage";
        private const string CRIT_CHANCE = "Crit Chance";
        private const string ATTACK_RATE = "Attack Rate";
        private const string ATTACK_RANGE = "Attack Range";
        private const string MOVE_SPEED = "Move Speed";
        
        [SerializeField] private BaseUnitEntityConfig _config;
        
        #endregion

        #region UnityCallBacks

        protected override void Awake()//temp!!!
        {
            base.Awake();
            
            TargetingHandler = new TargetingHandler(this);
            
            List<Stat> stats = new List<Stat>();

            foreach (var stat in _config.Stats)
                stats.Add(new Stat(stat.StatName,stat.BaseValue,stat.ID));

            StatusHandler = new StatusHandler(stats.ToArray());
        }
        protected virtual void Update()
        {
            HealthComponentUpdate();
            StatusHandler.UpdateStatusEffects();

            if (_target == null || _target.IsEntityDead)
                _target = TargetingHandler.GetPriorityTarget();
        }

        #endregion

        #region TargetingComponent

        public IPriority DefaultPriority { get; private set; }
        public ITargeting TargetingHandler { get; set; }
        public void SetTargeting(ITargeting targeting) => 
            TargetingHandler = targeting;
        
        #endregion

        #region HealthComponent
        
        private float  _currentInvincibleTime;

        public Stat HP => StatusHandler.GetStatByName(HEALTH);
        public Stat InvincibleTime => StatusHandler.GetStatByName(INVINCIBLE_TIME);
        public bool IsDamageable { get; private set; }
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
                    _currentInvincibleTime = InvincibleTime.CurrentValue;
                }
            }
        }

        #endregion

        #region CombatComponent                                                                                                                                   
        
        private IEntityTargetAbleComponent _target;
        private float _currentAttackRate;
        
        public IEntityTargetAbleComponent Target => _target;
        
        public Stat AttackDamage => StatusHandler.GetStatByName(ATTACK_DAMAGE);
        public Stat CritDamage => StatusHandler.GetStatByName(CRIT_DAMAGE);
        public Stat CritChance => StatusHandler.GetStatByName(CRIT_CHANCE);
        public Stat AttackRate => StatusHandler.GetStatByName(ATTACK_RATE);
        public Stat AttackRange => StatusHandler.GetStatByName(ATTACK_RANGE);
        
        public void Attack()
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
                _target.TakeDamage(CritDamage.CurrentValue);
                return;
            }
            
            _target.TakeDamage(AttackDamage.CurrentValue);
        }

        #endregion

        #region MovementComponent


        public Stat MoveSpeed => StatusHandler.GetStatByName(MOVE_SPEED); 
        
        //Dont we prefer a vector3? unless we're planning on making some form of "data/info map" that would be the 2D logic of the battle field,
        //then translate that to a 3D representation of the battle -> seperatly.
        public void SetDestination(Vector2 destination, MoveType moveType) 
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region StstusEffcetComponent

        //stuff
        public StatusHandler StatusHandler { get; private set; }

        #endregion

        #region AbilityComponent
        
        public AbilityHandler AbilityHandler { get; }

        #endregion
    }
}