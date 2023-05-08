using System.Collections.Generic;

namespace Tzipory.AbilitiesSystem
{
    public interface IAbilityCaster
    {
        public List<BaseAbility> Abilities { get; }
    }
}