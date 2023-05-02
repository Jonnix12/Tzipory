using System.Collections.Generic;
using System.Linq;

namespace Tzipory.EntitySystem.StatusSystem
{
    public class StatusEffectHandler
    {
        private Dictionary<int, Stat> _stats;

        private Dictionary<int, BaseStatusEffect> _activeStatusEffects;

        public StatusEffectHandler(Stat[] stats)
        {
            _stats = new Dictionary<int, Stat>();
            
            foreach (var stat in stats)
                _stats.Add(stat.Id, stat);

            _activeStatusEffects = new Dictionary<int, BaseStatusEffect>();
        }

        public void UpdateStatusEffects()
        {
            for (int index = 0; index < _activeStatusEffects.Count; index++)
            {
                KeyValuePair<int, BaseStatusEffect> statusEffect = _activeStatusEffects.ElementAt(index);
                statusEffect.Value.Execute();
            }
        }
        
        private void RemoveStatusEffect(int id)
        {
            if(_activeStatusEffects.TryGetValue(id, out BaseStatusEffect baseStatusEffect))
            {
                baseStatusEffect.OnStatusEffectDone -= RemoveStatusEffect;
                _activeStatusEffects.Remove(id);
            }
        }
        
        public void AddStatusEffect(StatusEffectType statusEffectType,float duration,int statId,StatModifier[] statModifiers)=>
            AddStatusEffect(statusEffectType,duration,0,statId,statModifiers);
        
        
        public void AddStatusEffect(StatusEffectType statusEffectType,int statId,StatModifier[] statModifiers)=>
            AddStatusEffect(statusEffectType,0,0,statId,statModifiers);
        

        private void AddStatusEffect(StatusEffectType statusEffectType,float duration,float interval,int statId,StatModifier[] statModifiers)
        {
            BaseStatusEffect baseStatusEffect = statusEffectType switch
            {
                StatusEffectType.OverTime => new OverTimeStatusEffect(duration, _stats[statId], statModifiers),
                StatusEffectType.Instant => new InstantStatusEffect(_stats[statId], statModifiers),
                StatusEffectType.Interval => new IntervalStatusEffect(duration, interval, _stats[statId], statModifiers),
                _ => null
            };

            if (baseStatusEffect != null)
            {
                baseStatusEffect.OnStatusEffectDone += RemoveStatusEffect;

                _activeStatusEffects.Add(baseStatusEffect.StatusEffectId, baseStatusEffect);
            }
        }

    }

    public enum StatusEffectType
    {
        OverTime,
        Instant,
        Interval
    }
}