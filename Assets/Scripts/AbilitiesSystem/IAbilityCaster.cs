using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.AbilitiesSystem
{
    public interface IAbilityCaster
    {
        public IEntityTargetAbleComponent Caster { get; }
        public Dictionary<string, BaseAbility> Abilities { get; }
    }
}