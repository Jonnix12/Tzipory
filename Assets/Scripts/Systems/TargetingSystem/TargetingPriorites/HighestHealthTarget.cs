using System.Collections;
using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;
namespace Tzipory.EntitySystem.TargetingSystem.TargetingPriorites
{

    public class HighestHealthTarget : BaseTargetingPriority
    {
        public HighestHealthTarget(IEntityTargetingComponent targetingComponent) : base(targetingComponent)
        {
        }

        public override IEntityTargetAbleComponent GetPriorityTarget(IEnumerable<IEntityTargetAbleComponent> targets)
        {
            IEntityTargetAbleComponent currentHighestTarget = null;

            float currentLowestHP = float.MinValue;

            foreach (var target in targets)
            {
                if (target.HP.CurrentValue > currentLowestHP)
                {
                    currentHighestTarget = target;
                    currentLowestHP = target.HP.CurrentValue;
                }
            }

            return currentHighestTarget;
        }
    }
}
