using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.EntitySystem.TargetingSystem.TargetingPriorites;

namespace Tzipory.AbilitiesSystem
{
    public abstract class BaseAbility
    {
        public string AbilityName { get; }
        public int AbilityId { get; }

        public TargetType TargetType { get; }//not in use

        public EffectType EffectType { get; }//not in use

        public bool IsCasting { get; private set; }

        protected Dictionary<string, Stat> AbilityParameter { get; }

        protected List<BaseStatusEffect> StatusEffects { get; }
        
        protected readonly IEntityTargetingComponent entityCasterTargetingComponent;

        protected IEntityTargetAbleComponent CurrentTarget;
        
        private Stat Cooldown { get; }

        private Stat CastTime { get; }
        
        private bool _isReady;
        
        private IPriorityTargeting _entityTargetingComponent;

        
        protected BaseAbility(IEntityTargetingComponent entityCasterTargetingComponent,AbilityConfig config)
        {
            this.entityCasterTargetingComponent = entityCasterTargetingComponent;
            
            AbilityName = config.AbilityName;
            AbilityId = config.AbilityId;
            
            EffectType = config.EffectType;

            AbilityParameter = new Dictionary<string, Stat>();

            foreach (var abilityParameter in config.AbilityParameter)
            {
                var stat = new Stat(abilityParameter.Name, abilityParameter.BaseValue, abilityParameter.MaxValue,abilityParameter.Id);
                AbilityParameter.Add(stat.Name,stat);
            }

            Cooldown = new Stat(config.Cooldown.Name, config.Cooldown.BaseValue, config.Cooldown.MaxValue,
                config.Cooldown.Id);
            CastTime = new Stat(config.CastTime.Name, config.CastTime.BaseValue, config.CastTime.MaxValue,
                config.CastTime.Id);

            StatusEffects = new List<BaseStatusEffect>();

            foreach (var statusEffectConfig in config.StatusEffectConfigs)
                StatusEffects.Add(StatusHandler.GetStatusEffect(statusEffectConfig));

            switch (config.TargetingPriority)
            {
                case TargetingPriority.ClosesToEntity:
                    _entityTargetingComponent = new ClosestTarget(entityCasterTargetingComponent);
                    break;
                default:
                    _entityTargetingComponent = entityCasterTargetingComponent.DefaultPriorityTargeting;
                    break;
            }
            
            _isReady = true;
        }

        public void Execute(IEnumerable<IEntityTargetAbleComponent> availableTarget)
        {
            if (!_isReady)
                return;
            
            _isReady = false;

            CurrentTarget = _entityTargetingComponent.GetPriorityTarget(availableTarget);
            IsCasting = true;
            entityCasterTargetingComponent.GameEntity.EntityTimer.StartNewTimer(CastTime.CurrentValue, ExecuteAbility);
        }

        protected abstract void ExecuteAbility();

        protected void StartCooldown()=>
            entityCasterTargetingComponent.GameEntity.EntityTimer.StartNewTimer(Cooldown.CurrentValue,
                () => { _isReady = true; IsCasting  = false; });
    }

    // public abstract class BaseAbilityCaster
    // {
    //     public abstract void Cast(IEntityTargetAbleComponent target);
    // }
    //
    // public class StatusEffectAbilityCaster : BaseAbilityCaster
    // {
    //     private BaseStatusEffect[] _statusEffects;
    //     
    //     public StatusEffectAbilityCaster(BaseStatusEffect[] statusEffects)
    //     {
    //         _statusEffects = statusEffects;
    //     }
    //
    //     public override void Cast(IEntityTargetAbleComponent target)
    //     {
    //         foreach (var statusEffect in _statusEffects)
    //             target.StatusHandler.AddStatusEffect(statusEffect);
    //     }
    // }
    //
    // public class ActionAbilityCaster : BaseAbilityCaster
    // {
    //     private AbilityActionType _abilityType;
    //     private float _amount;
    //     
    //     public ActionAbilityCaster(AbilityActionType abilityType,float amount)
    //     {
    //         _abilityType = abilityType;
    //         _amount = amount;
    //     }
    //
    //     public override void Cast(IEntityTargetAbleComponent target)
    //     {
    //         switch (_abilityType)
    //         {
    //             case AbilityActionType.Heal:
    //                 target.Heal(_amount);
    //                 break;
    //             case AbilityActionType.Damage:
    //                 target.TakeDamage(_amount);
    //                 break;
    //             default:
    //                 throw new ArgumentOutOfRangeException(nameof(_abilityType), _abilityType, null);
    //         }
    //     }
}