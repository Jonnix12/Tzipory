using System.Collections.Generic;
using System.Linq;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    public class StatusHandler
    {
        private IEntityStatusEffectComponent _entity;
        
        private readonly Dictionary<int, Stat> _statsById;
        private readonly Dictionary<string, Stat> _statsByName;

        private readonly Dictionary<int, BaseStatusEffect> _activeStatusEffects;

        public StatusHandler(IEnumerable<Stat> stats,IEntityStatusEffectComponent entity)
        {
            _entity = entity;
            
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
            if (_activeStatusEffects.ContainsKey(baseStatusEffect.StatusEffectId))
                return;

            baseStatusEffect.SetStat(GetStatByName(baseStatusEffect.StatName));
            
            baseStatusEffect.StatusEffectStart();
#if UNITY_EDITOR
            Debug.Log($"Add Statues Effect on {_entity.EntityTransform.name}");
#endif
            _activeStatusEffects.Add(baseStatusEffect.StatusEffectId, baseStatusEffect);
            baseStatusEffect.OnStatusEffectDone += RemoveStatusEffect;
        }

        private void RemoveStatusEffect(int id)
        {
            if(_activeStatusEffects.TryGetValue(id, out BaseStatusEffect baseStatusEffect))
            {
                baseStatusEffect.OnStatusEffectDone -= RemoveStatusEffect;
                _activeStatusEffects.Remove(id);
            }
        }
        
        public static BaseStatusEffect GetStatusEffect(StatusEffectConfig statusEffectConfig)
        {
            BaseStatusEffect baseStatusEffect = statusEffectConfig.StatusEffectType switch
            {
                StatusEffectType.OverTime => new OverTimeStatusEffect(statusEffectConfig),
                StatusEffectType.Instant => new InstantStatusEffect(statusEffectConfig),
                StatusEffectType.Interval => new IntervalStatusEffect(statusEffectConfig),
                _ => null
            };
            
            return  baseStatusEffect;
        }
    }
}