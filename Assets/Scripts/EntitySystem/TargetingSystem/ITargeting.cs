﻿using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.EntitySystem.TargetingSystem
{
    public interface ITargeting
    {
        public List<IEntityTargetAbleComponent> AvailableTargets { get; }
        
        public IEntityTargetAbleComponent GetPriorityTarget(IPriorityTargeting priorityTargeting = null);

        public void AddTarget(IEntityTargetAbleComponent targetAbleComponent);
        public void RemoveTarget();
    }
}