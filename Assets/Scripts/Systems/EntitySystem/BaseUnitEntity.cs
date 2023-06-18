using System.Collections.Generic;
using Helpers.Consts;
using Sirenix.OdinInspector;
using Tzipory.AbilitiesSystem;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.EntityConfigSystem;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.EntitySystem.TargetingSystem.TargetingPriorites;
using Tzipory.Tools.Sound;
using Tzipory.VisualSystem.EffectSequence;
using UnityEngine;

namespace Tzipory.EntitySystem.Entitys
{
    public abstract class BaseUnitEntity : BaseGameEntity , IEntityTargetAbleComponent , IEntityCombatComponent , IEntityMovementComponent , IEntityTargetingComponent , IEntityAbilitiesComponent,IEntityVisualComponent
    {

        #region Fields
        
        [Header("Entity config")]
        [SerializeField] private BaseUnitEntityConfig _config;
      
        
#if UNITY_EDITOR
        [SerializeField, ReadOnly,TabGroup("Stats")] private List<Stat> _stats;
#endif
        [SerializeField,TabGroup("Component")] private CircleCollider2D _bodyCollider;
        [SerializeField,TabGroup("Component")] private CircleCollider2D _rangeCollider;
        [Header("Visual components")]
        [SerializeField,TabGroup("Component")] private SpriteRenderer _spriteRenderer;
        [SerializeField,TabGroup("Component")] private Transform _visualQueueEffectPosition;
        [SerializeField,TabGroup("Component")] private Transform _particleEffectPosition;
        [SerializeField,TabGroup("Component")] private SoundHandler _soundHandler;

        [SerializeField,TabGroup("Visual Events")] private EffectSequence _onDeath;
        [SerializeField,TabGroup("Visual Events")] private EffectSequence _onAttack;
        [SerializeField,TabGroup("Visual Events")] private EffectSequence _onCritAttack;
        [SerializeField,TabGroup("Visual Events")] private EffectSequence _onMove;
        [SerializeField,TabGroup("Visual Events")] private EffectSequence _onGetHit;
        [SerializeField,TabGroup("Visual Events")] private EffectSequence _onGetCritHit;

        #endregion

        #region UnityCallBacks

        protected override void Awake()//temp!!!
        {
            base.Awake();

            DefaultPriorityTargeting =
                Factory.TargetingPriorityFactory.GetTargetingPriority(this, _config.TargetingPriority);
            
            Targeting = GetComponentInChildren<TargetingHandler>();//temp
            Targeting.Init(this);
            
            List<Stat> stats = new List<Stat>();
            
            stats.Add(new Stat(Constant.StatNames.Health, _config.Health.BaseValue, _config.Health.MaxValue, Constant.StatIds.Health));
            stats.Add(new Stat(Constant.StatNames.InvincibleTime, _config.InvincibleTime.BaseValue, _config.InvincibleTime.MaxValue, Constant.StatIds.InvincibleTime));
            stats.Add(new Stat(Constant.StatNames.AttackDamage, _config.AttackDamage.BaseValue, _config.AttackDamage.MaxValue, Constant.StatIds.AttackDamage));
            stats.Add(new Stat(Constant.StatNames.CritDamage, _config.CritDamage.BaseValue, _config.CritDamage.MaxValue, Constant.StatIds.CritDamage));
            stats.Add(new Stat(Constant.StatNames.CritChance, _config.CritChance.BaseValue, _config.CritChance.MaxValue, Constant.StatIds.CritChance));
            stats.Add(new Stat(Constant.StatNames.AttackRate, _config.AttackRate.BaseValue, _config.AttackRate.MaxValue, Constant.StatIds.AttackRate));
            stats.Add(new Stat(Constant.StatNames.AttackRange, _config.AttackRange.BaseValue, _config.AttackRange.MaxValue, Constant.StatIds.AttackRange));
            stats.Add(new Stat(Constant.StatNames.MovementSpeed, _config.MovementSpeed.BaseValue, _config.MovementSpeed.MaxValue, Constant.StatIds.MovementSpeed));
            
            if (_config.Stats != null && _config.Stats.Count > 0)
            {
                foreach (var stat in _config.Stats)
                    stats.Add(new Stat(stat.Name, stat.BaseValue, stat.MaxValue, stat.Id));
#if UNITY_EDITOR
                _stats = stats;
#endif
            }
               
            StatusHandler = new StatusHandler(stats,this);

            _onDeath.ID = Constant.EffectSequenceIds.OnDeath;
            _onAttack.ID = Constant.EffectSequenceIds.OnAttack;
            _onCritAttack.ID = Constant.EffectSequenceIds.OnCritAttack;
            _onMove.ID = Constant.EffectSequenceIds.OnMove;
            _onGetHit.ID = Constant.EffectSequenceIds.OnGetHit;
            _onGetCritHit.ID = Constant.EffectSequenceIds.OnGetCritHit;
            
            _onDeath.SequenceName = "OnDeath";
            _onAttack.SequenceName = "OnAttack";
            _onCritAttack.SequenceName = "OnCritAttack";
            _onMove.SequenceName = "OnMove";
            _onGetHit.SequenceName = "OnGetHit";
            _onGetCritHit.SequenceName = "OnGetCritHit";
            
            var effectSequence = new EffectSequence[]
            {
                _onDeath,
                _onAttack,
                _onCritAttack,
                _onMove,
                _onGetHit,
                _onGetCritHit
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
                _target = Targeting.GetPriorityTarget();
            
            
            if (_target != null)//temp
                Attack();
            
            _rangeCollider.radius = StatusHandler.GetStatById(Constant.StatIds.AttackRange).CurrentValue;//temp
        }

        private void OnValidate()
        {
            _soundHandler ??= GetComponentInChildren<SoundHandler>();

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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StatusHandler.OnStatusEffectInterrupt -= EffectSequenceHandler.RemoveEffectSequence;
            StatusHandler.OnStatusEffectAdded -= AddStatusEffectVisual;
        }

        #endregion

        #region TargetingComponent

        public EntityTeamType EntityTeamType { get; protected set; }
        
        public IPriorityTargeting DefaultPriorityTargeting { get; private set; }
        public TargetingHandler Targeting { get; set; }
        public float GetDistanceToTarget(IEntityTargetAbleComponent targetAbleComponent)
        {
            return Vector2.Distance(transform.position, targetAbleComponent.EntityTransform.position);
        }

        #endregion

        #region HealthComponent
        
        private float  _currentInvincibleTime;

        public Stat HP => StatusHandler.GetStatById(Constant.StatIds.Health);
        
        public Stat InvincibleTime => StatusHandler.GetStatById(Constant.StatIds.InvincibleTime);
        public bool IsDamageable { get; private set; }
        public bool IsEntityDead => HP.CurrentValue <= 0;

        public void Heal(float amount)
        {
            HP.AddToValue(amount);

            // if (HP.CurrentValue > HP.BaseValue)
            //     HP.ResetValue();
        }

        public void TakeDamage(float damage,bool isCrit)
        {
            if (IsDamageable)
            {
                EffectSequenceHandler.PlaySequenceById(isCrit
                    ? Constant.EffectSequenceIds.OnGetCritHit
                    : Constant.EffectSequenceIds.OnGetHit);
                
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
        
        public Stat AttackDamage => StatusHandler.GetStatById(Constant.StatIds.AttackDamage);
        public Stat CritDamage => StatusHandler.GetStatById(Constant.StatIds.CritDamage);
        public Stat CritChance => StatusHandler.GetStatById(Constant.StatIds.CritChance);
        public Stat AttackRate => StatusHandler.GetStatById(Constant.StatIds.AttackRate);
        public Stat AttackRange => StatusHandler.GetStatById(Constant.StatIds.AttackRange);
        
        public virtual void Attack()
        {
        }

        #endregion

        #region MovementComponent


        public Stat MoveSpeed => StatusHandler.GetStatById(Constant.StatIds.MovementSpeed);
        
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
        public SoundHandler SoundHandler => _soundHandler;
        public Transform ParticleEffectPosition => _particleEffectPosition;
        public Transform VisualQueueEffectPosition => _visualQueueEffectPosition;

        private void AddStatusEffectVisual(BaseStatusEffect baseStatusEffect) =>
            EffectSequenceHandler.ActiveEffectSequence(baseStatusEffect.EffectSequence);

        #endregion
    }
}