using System.Collections.Generic;
using Helpers.Consts;
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
            
            AbilityParameter = new Dictionary<string, Stat>();

            Cooldown = new Stat(Constant.StatNames.AbilityCooldown, config.Cooldown, int.MaxValue,Constant.StatIds.AbilityCooldown);
            CastTime = new Stat(Constant.StatNames.AbilityCastTime, config.CastTime, int.MaxValue,Constant.StatIds.AbilityCastTime);

            StatusEffects = new List<BaseStatusEffect>();

            foreach (var statusEffectConfig in config.StatusEffectConfigs)
                StatusEffects.Add(StatusHandler.GetStatusEffect(statusEffectConfig));

            _entityTargetingComponent = Factory.TargetingPriorityFactory.GetTargetingPriority(entityCasterTargetingComponent,config.TargetingPriorityType);

            _isReady = true;
        }

        public void Cast(IEnumerable<IEntityTargetAbleComponent> availableTarget)
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
}