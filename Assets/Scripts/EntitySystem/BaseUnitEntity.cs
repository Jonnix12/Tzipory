using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tzipory.AbilitiesSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.EntityConfigSystem;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.EntitySystem.TargetingSystem.TargetingPriorites;
using UnityEngine;

namespace Tzipory.EntitySystem.Entitys
{
    public abstract class BaseUnitEntity : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent , IEntityAbilitiesComponent
    {
        #region Fields

        [SerializeField] private EntityTeamType _entityTeam;//temp

#if UNITY_EDITOR
        [SerializeField, ReadOnly] private List<Stat> _stats;
#endif
        
        [SerializeField] private CircleCollider2D _bodyCollider;
        [SerializeField] private CircleCollider2D _rangeCollider;

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

            DefaultPriorityTargeting = new ClosestTarget(this);//temp
            
            TargetingHandler = new TargetingHandler(this);
            
            List<Stat> stats = new List<Stat>();

            if (_config.Stats != null && _config.Stats.Count > 0)
            {
                foreach (var stat in _config.Stats)
                    stats.Add(new Stat(stat.Name, stat.BaseValue, stat.MaxValue, stat.Id));
                _stats = stats;
            }
               
            StatusHandler = new StatusHandler(stats,this);

            AbilityHandler = new AbilityHandler(this, this, _config.AbilityConfigs);
        }
        
        protected virtual void Update()
        {
            HealthComponentUpdate();
            StatusHandler.UpdateStatusEffects();

            if (_target == null || _target.IsEntityDead)
                _target = TargetingHandler.GetPriorityTarget();
            
            
            if (_target != null)
                Attack();
            
          //  _rangeCollider.radius = StatusHandler.GetStatByName(ATTACK_RANGE).CurrentValue;
        }

        private void OnValidate()
        {
            if (_bodyCollider == null || _rangeCollider == null)
            {
                var collider = GetComponents<CircleCollider2D>();

                foreach (var collider2D in collider)
                {
                    if (collider2D.isTrigger)
                        _rangeCollider = collider2D;
                    else
                        _bodyCollider = collider2D;
                }
            }
        }

        #endregion

        #region TargetingComponent

        public EntityTeamType EntityTeamType => _entityTeam;
        
        public IPriorityTargeting DefaultPriorityTargeting { get; private set; }
        public ITargeting TargetingHandler { get; set; }
        public float GetDistanceToTarget(IEntityTargetAbleComponent targetAbleComponent)
        {
            return Vector2.Distance(transform.position, targetAbleComponent.EntityTransform.position);
        }

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

            // if (HP.CurrentValue > HP.BaseValue)
            //     HP.ResetValue();
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
        
        public IEntityTargetAbleComponent Target => _target;
        
        public Stat AttackDamage => StatusHandler.GetStatByName(ATTACK_DAMAGE);
        public Stat CritDamage => StatusHandler.GetStatByName(CRIT_DAMAGE);
        public Stat CritChance => StatusHandler.GetStatByName(CRIT_CHANCE);
        public Stat AttackRate => StatusHandler.GetStatByName(ATTACK_RATE);
        public Stat AttackRange => StatusHandler.GetStatByName(ATTACK_RANGE);
        
        public virtual void Attack()
        {
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
        
        public AbilityHandler AbilityHandler { get; private set; }

        #endregion

        #region Trriger

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<BaseUnitEntity>(out BaseUnitEntity unitEntity))
            {
                TargetingHandler.AddTarget(unitEntity);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
        }

        #endregion
    }
}