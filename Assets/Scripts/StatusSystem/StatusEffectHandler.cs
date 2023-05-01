using System.Collections.Generic;

namespace Tzipory.EntitySystem.StatusSystem
{
    public class StatusEffectHandler
    {
        private Dictionary<int, Stat> _stats;

        private List<BaseStatusEffect> _activeStatusEffects;

        public StatusEffectHandler(Stat[] stats)
        {
            _stats = new Dictionary<int, Stat>();
            
            foreach (var stat in stats)
                _stats.Add(stat.Id, stat);
            
            _activeStatusEffects = new List<BaseStatusEffect>();
        }

        public void UpdateStatusEffects()
        {
            foreach (var statusEffect in _activeStatusEffects)
                statusEffect.Execute();
        }
    }
}