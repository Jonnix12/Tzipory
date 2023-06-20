using System;
using System.Collections.Generic;
using System.Linq;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    public class StatusHandler
    {
        public event Action<BaseStatusEffect> OnStatusEffectAdded; 
        public event Action<int> OnStatusEffectRemoved; 
        public event Action<int> OnStatusEffectInterrupt; 

        private IEntityStatusEffectComponent _entity;
        private  readonly Dictionary<int, IStatHolder> _stats;
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
                statusEffect.ProcessStatusEffect();
            }
        }
        
        public void AddStatusEffect(BaseStatusEffect baseStatusEffect)
        {
            if (_activeStatusEffects.ContainsKey(baseStatusEffect.StatusEffectId))
                return;

            baseStatusEffect.SetStat(GetStatByName(baseStatusEffect.AffectedStatName));
            
            baseStatusEffect.StatusEffectStart();
            
            InterruptStatusEffects(baseStatusEffect.StatusEffectToInterrupt);
            
            _activeStatusEffects.Add(baseStatusEffect.StatusEffectId, baseStatusEffect);
            OnStatusEffectAdded?.Invoke(baseStatusEffect);
            baseStatusEffect.OnStatusEffectDone += RemoveStatusEffect;
            
#if UNITY_EDITOR
            Debug.Log($"Add Statues Effect {baseStatusEffect.StatusEffectName} on {_entity.EntityTransform.name}, Affected stat is {baseStatusEffect.AffectedStatName}");
#endif
        }

        private void InterruptStatusEffects(IEnumerable<StatusEffectConfigSo> effectConfigSos)
        {
            foreach (var effectConfigSo in effectConfigSos)
            {
                if (_activeStatusEffects.TryGetValue(effectConfigSo.StatusEffectId,out var statusEffect))
                {
                    statusEffect.StatusEffectInterrupt();
                    _activeStatusEffects.Remove(effectConfigSo.StatusEffectId);
                    OnStatusEffectInterrupt?.Invoke(statusEffect.StatusEffectId);
                }
            }
        }

        private void RemoveStatusEffect(int id)
        {
            if(_activeStatusEffects.TryGetValue(id, out BaseStatusEffect baseStatusEffect))
            {
                baseStatusEffect.OnStatusEffectDone -= RemoveStatusEffect;
                OnStatusEffectRemoved?.Invoke(baseStatusEffect.StatusEffectId);
                _activeStatusEffects.Remove(id);
            }
        }
    }
}