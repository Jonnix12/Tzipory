using System.Collections.Generic;
using Tzipory.AbilitiesSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityAbilitiesComponent
    {
        public AbilityHandler AbilityHandler { get; }
    }
}