using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tzipory.AbilitiesSystem;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.EntityConfigSystem;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.EntitySystem.TargetingSystem.TargetingPriorites;
using Tzipory.VisualSystem.EffectSequence;
using UnityEngine;

namespace Tzipory.EntitySystem.Entitys
{
    public abstract class BaseUnitEntity : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent , IEntityAbilitiesComponent,IEntityVisualComponent
    {
        #region Const

        private const string HEALTH = "Hp";
        private const string INVINCIBLE_TIME = "Invincible Time";
        private const string ATTACK_DAMAGE = "Attack Damage";
        private const string CRIT_DAMAGE = "Crit Damage";
        private const string CRIT_CHANCE = "Crit Chance";
        private const string ATTACK_RATE = "Attack Rate";
        private const string ATTACK_RANGE = "Attack Range";
        private const string MOVE_SPEED = "Move Speed";

        #endregion
        
        #region Fields
        
#if UNITY_EDITOR
        [SerializeField, ReadOnly] private List<Stat> _stats;
#endif
        [Header("Component")]
        [SerializeField] private CircleCollider2D _bodyCollider;
        [SerializeField] private CircleCollider2D _rangeCollider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _visualQueueEffectPosition;
        [SerializeField] private Transform _particleEffectPosition;

        [Header("Visual Events")] 
        [SerializeField] private EffectSequence _onDeath;
        [SerializeField] private EffectSequence _onAttack;
        [SerializeField] private EffectSequence _onMove;
        [SerializeField] private EffectSequence _onGetHit;
        
        [Header("Entity config")]
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
#if UNITY_EDITOR
                _stats = stats;
#endif
            }
               
            StatusHandler = new StatusHandler(stats,this);
            
            var effectSequence = new EffectSequence[]
            {
                _onDeath,
                _onAttack,
                _onMove,
                _onGetHit
            };

            EffectSequenceHandler = new EffectSequenceHandler(this,effectSequence);

            StatusHandler.OnStatusEffectInterrupt += EffectSequenceHandler.RemoveEffectSequence;
            StatusHandler.OnStatusEffectAdded += AddStatusEffectVisual;
            
            
            AbilityHandler = new AbilityHandler(this, _config.AbilityConfigs);

            _rangeCollider.isTrigger = true;
        }
        
        protected override void Update()
        {
            base.Update();
            
            HealthComponentUpdate();
            StatusHandler.UpdateStatusEffects();

            if (_target == null || _target.IsEntityDead)
                _target = TargetingHandler.GetPriorityTarget();
            
            
            if (_target != null)
                Attack();
            
            _rangeCollider.radius = StatusHandler.GetStatByName(ATTACK_RANGE).CurrentValue;//temp
        }

        private void OnValidate()
        {
            if (_bodyCollider == null || _rangeCollider == null)
            {
                var colliders = GetComponents<CircleCollider2D>();

                foreach (var collider in colliders)
                {
                    if (collider.isTrigger)
                        _rangeCollider = collider;
                    else
                        _bodyCollider = collider;
                }
            }
        }

        private void OnDestroy()
        {
            StatusHandler.OnStatusEffectInterrupt -= EffectSequenceHandler.RemoveEffectSequence;
            StatusHandler.OnStatusEffectAdded -= AddStatusEffectVisual;
        }

        #endregion

        #region TargetingComponent

        public EntityTeamType EntityTeamType { get; protected set; }
        
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
                EffectSequenceHandler.PlaySequenceById(111);
                HP.ReduceFromValue(damage);
                IsDamageable = false;
            }
        }

        private void HealthComponentUpdate()
        {
            if (!IsDamageable)
            {
                _currentInvincibleTime -= GAME_TIME.GameDeltaTime;

                if (_currentInvincibleTime < 0)
                {
                    IsDamageable = true;
                    _currentInvincibleTime = InvincibleTime.CurrentValue;
                }
            }
            
            if (HP.CurrentValue < 0)
            {
                gameObject.SetActive(false);
                EntityTimer.StartNewTimer(2f, () => { Destroy(gameObject); });
                //effectplay
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
        
        #region VisualComponent
        
        public EffectSequenceHandler EffectSequenceHandler { get; private set; }
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Transform ParticleEffectPosition => _particleEffectPosition;
        public Transform VisualQueueEffectPosition => _visualQueueEffectPosition;

        private void AddStatusEffectVisual(BaseStatusEffect baseStatusEffect) =>
            EffectSequenceHandler.ActiveEffectSequence(baseStatusEffect.EffectSequence);

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
            if (other.gameObject.TryGetComponent<BaseUnitEntity>(out BaseUnitEntity unitEntity))
            {
                TargetingHandler.RemoveTarget(unitEntity);
            }
        }

        #endregion
    }
}