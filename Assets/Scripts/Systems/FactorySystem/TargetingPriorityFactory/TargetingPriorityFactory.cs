using System;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.EntitySystem.TargetingSystem.TargetingPriorites;

namespace Factory
{
    public class TargetingPriorityFactory
    {
        public static  IPriorityTargeting GetTargetingPriority(IEntityTargetingComponent entityTargetingComponent,TargetingPriorityType targetingPriorityType)
        {
            switch (targetingPriorityType)
            {
                case TargetingPriorityType.Random:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.ClosesToEntity:
                    return new ClosestTarget(entityTargetingComponent);
                case TargetingPriorityType.FarthestFromEntity:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.ClosesToCore:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.FarthestFromCore:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.LowestHpEntity:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.HighestHpEntity:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.LowestHpPercentageEntity:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.HighestHpPercentageEntity:
                    throw  new ArgumentOutOfRangeException(nameof(targetingPriorityType), targetingPriorityType, null);
                case TargetingPriorityType.Default:
                    return entityTargetingComponent.DefaultPriorityTargeting ?? new ClosestTarget(entityTargetingComponent);
                default:
                    throw  new ArgumentOutOfRangeException();
            }
        }
    }
}