using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    public class StatusHandler
    {
        private readonly Dictionary<int, Stat> _statsById;
        private readonly Dictionary<string, Stat> _statsByName;

        private readonly Dictionary<int, BaseStatusEffect> _activeStatusEffects;

        public StatusHandler(IEnumerable<Stat> stats)
        {
            _statsById = new Dictionary<int, Stat>();
            _statsByName = new Dictionary<string, Stat>();

            foreach (var stat in stats)
            {
                _statsByName.Add(stat.Name, stat);
                _statsById.Add(stat.Id, stat);
            }

            _activeStatusEffects = new Dictionary<int, BaseStatusEffect>();
        }

        public Stat GetStatById(int id)
        {
            if (_statsById.TryGetValue(id, out Stat stat))
            {
                return stat;
            }

            Debug.LogError($"Stat ID: {id} not found in StatusHandler");
            return  null;
        }

        public Stat GetStatByName(string statName)
        {
            if (_statsByName.TryGetValue(statName, out Stat stat))
            {
                return stat;
            }

            Debug.LogError($"Stat Name: {statName} not found in StatusHandler");
            return  null;
        }

        public void UpdateStatusEffects()
        {
            for (int index = 0; index < _activeStatusEffects.Count; index++)
            {
                var statusEffect = _activeStatusEffects.ElementAt(index).Value;
                statusEffect.Execute();
            }
        }
        
        public void AddStatusEffect(BaseStatusEffect baseStatusEffect)
        {
            baseStatusEffect.OnStatusEffectDone += RemoveStatusEffect;
            _activeStatusEffects.Add(baseStatusEffect.StatusEffectId, baseStatusEffect);
        }
        
        public void AddStatusEffect(StatusEffectType statusEffectType,float duration,int statId,StatModifier[] statModifiers)=>
            AddStatusEffect(statusEffectType,duration,0,statId,statModifiers);
        
        
        public void AddStatusEffect(StatusEffectType statusEffectType,int statId,StatModifier[] statModifiers)=>
            AddStatusEffect(statusEffectType,0,0,statId,statModifiers);
        
        private void RemoveStatusEffect(int id)
        {
            if(_activeStatusEffects.TryGetValue(id, out BaseStatusEffect baseStatusEffect))
            {
                baseStatusEffect.OnStatusEffectDone -= RemoveStatusEffect;
                _activeStatusEffects.Remove(id);
            }
        }
        
        private void AddStatusEffect(StatusEffectType statusEffectType,float duration,float interval,int statId,StatModifier[] statModifiers)
        {
            BaseStatusEffect baseStatusEffect = statusEffectType switch
            {
                StatusEffectType.OverTime => new OverTimeStatusEffect(duration, _statsById[statId], statModifiers),
                StatusEffectType.Instant => new InstantStatusEffect(_statsById[statId], statModifiers),
                StatusEffectType.Interval => new IntervalStatusEffect(duration, interval, _statsById[statId], statModifiers),
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