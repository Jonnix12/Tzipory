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

            Cooldown = new Stat(Constant.StatNames.AbilityCooldown, config.Cooldown, int.MaxValue,
                Constant.StatIds.AbilityCooldown);
            CastTime = new Stat(Constant.StatNames.AbilityCastTime, config.CastTime, int.MaxValue,
                Constant.StatIds.AbilityCastTime);

            AbilityCaster = Factory.AbilityFactory.GetAbilityCaster(entityTargetingComponent,config);
            AbilityExecutor = Factory.AbilityFactory.GetAbilityExecutor(caster,config);

            AbilityCaster.OnCast += StartCooldown;
            
            _priorityTargeting =
                Factory.TargetingPriorityFactory.GetTargetingPriority(entityTargetingComponent,
                    config.TargetingPriorityType);

            _isReady = true;
        }

        public void Cast(IEnumerable<IEntityTargetAbleComponent> availableTarget)
        {
            if (!_isReady)
                return;

            _isReady = false;

            var currentTarget = _priorityTargeting.GetPriorityTarget(availableTarget);
            IAbilityExecutor abilityExecutor = AbilityExecutor;
            IsCasting = true;
            _entityTargetingComponent.GameEntity.EntityTimer.StartNewTimer(CastTime.CurrentValue,
                AbilityCaster.Cast, ref currentTarget, ref abilityExecutor);

        }

        private void StartCooldown() =>
            _entityTargetingComponent.GameEntity.EntityTimer.StartNewTimer(Cooldown.CurrentValue,
                () =>
                {
                    _isReady = true;
                    IsCasting = false;
                });

        ~Ability()
        {
            AbilityCaster.OnCast -= StartCooldown;
        }

    }
}