using System.Collections.Generic;
using Helpers.Consts;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.AbilitiesSystem
{
    public class Ability
    {
        private readonly IEntityTargetingComponent _entityTargetingComponent;
        private IPriorityTargeting _priorityTargeting;
        private bool _isReady;

        public string AbilityName { get; }
        public int AbilityId { get; }
        public bool IsCasting { get; private set; }

        private Stat Cooldown { get; }
        private Stat CastTime { get; }
        
        private IAbilityExecutor AbilityExecutor { get; }
        private IAbilityCaster AbilityCaster { get; }


        public Ability(IEntityTargetAbleComponent caster,IEntityTargetingComponent entityTargetingComponent, AbilityConfig config)
        {
            _entityTargetingComponent = entityTargetingComponent;

            AbilityName = config.AbilityName;
            AbilityId = config.AbilityId;

            Cooldown = new Stat(Constant.Stats.AbilityCooldown.ToString(), config.Cooldown, int.MaxValue,
                (int)Constant.Stats.AbilityCooldown);
            CastTime = new Stat(Constant.Stats.AbilityCastTime.ToString(), config.CastTime, int.MaxValue,
                (int)Constant.Stats.AbilityCastTime);

            AbilityCaster = Factory.AbilityFactory.GetAbilityCaster(entityTargetingComponent,config);
            AbilityExecutor = Factory.AbilityFactory.GetAbilityExecutor(caster,config);

            AbilityCaster.OnCast += StartCooldown;
            
            _priorityTargeting =
                Factory.TargetingPriorityFactory.GetTargetingPriority(entityTargetingComponent,
                    config.TargetingPriorityType);

            _isReady = true;
        }

        public void ExecuteAbility(IEnumerable<IEntityTargetAbleComponent> availableTarget)
        {
            if (!_isReady)
                return;

            _isReady = false;
            IsCasting = true;
            _entityTargetingComponent.GameEntity.EntityTimer.StartNewTimer(CastTime.CurrentValue, Cast,ref availableTarget);
        }

        private void Cast(IEnumerable<IEntityTargetAbleComponent> availableTarget)
        {
            var currentTarget = _priorityTargeting.GetPriorityTarget(availableTarget);
            
            if (currentTarget == null)
                return;
            
            AbilityCaster.Cast(currentTarget,AbilityExecutor);
        }

        private void StartCooldown()
        {
            IsCasting  = false;
            _entityTargetingComponent.GameEntity.EntityTimer.StartNewTimer(Cooldown.CurrentValue,ResetAbility);
        }

        private void ResetAbility() =>
            _isReady = true;

        ~Ability()
        {
            AbilityCaster.OnCast -= StartCooldown;
        }

    }
}