using System.Collections.Generic;
using AbilitiesSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityAbilitiesComponent
    {
        public List<BaseEntityAbility> _entityAbilities { get; }
        
        
    }
}