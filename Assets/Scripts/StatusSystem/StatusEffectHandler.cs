using System.Collections.Generic;

namespace Tzipory.StatusSystem
{
    public class StatusEffectHandler
    {
        private List<BaseStatusEffect> _activeStatusEffects;

        public void UpdateStatusEffects()
        {
            foreach (var statusEffect in _activeStatusEffects)
            {
                statusEffect.Execute();
            }
        }
    }
}