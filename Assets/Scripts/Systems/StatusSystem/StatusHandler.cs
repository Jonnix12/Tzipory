﻿using System;
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

        private readonly IEntityStatusEffectComponent _entity;
        
        private  readonly Dictionary<int, IStatHolder> _stats;//need to make a big refactor! for this to work
        
        private readonly Dictionary<int, Stat> _statsById;

        private readonly Dictionary<int, BaseStatusEffect> _activeStatusEffects;

        public StatusHandler(IEnumerable<Stat> stats,IEntityStatusEffectComponent entity)
        {
            _entity = entity;
            
            _statsById = new Dictionary<int, Stat>();

            foreach (var stat in stats)
                _statsById.Add(stat.Id, stat);

            _activeStatusEffects = new Dictionary<int, BaseStatusEffect>();
        }

        public Stat GetStatById(int id)
        {
            if (_statsById.TryGetValue(id, out Stat stat))
            {
                return stat;
            }

            Debug.LogError($"Stat ID: {id} not found in StatusHandler");
            return default;
        }

        public void UpdateStatusEffects()
        {
            for (int index = 0; index < _activeStatusEffects.Count; index++)
            {
                var statusEffect = _activeStatusEffects.ElementAt(index).Value;

                if (statusEffect.IsDone)
                {
                    RemoveStatusEffect(statusEffect.AffectedStatId);
                    continue;
                }
                
                statusEffect.ProcessStatusEffect();
            }
        }
        
        public IDisposable AddStatusEffect(StatusEffectConfigSo statusEffectConfigSo)
        {
            if (_activeStatusEffects.ContainsKey(statusEffectConfigSo.AffectedStatId))
                return null;

            var statToEffect = GetStatById(statusEffectConfigSo.AffectedStatId);
            
            var statusEffect = Factory.StatusEffectFactory.GetStatusEffect(statusEffectConfigSo,statToEffect);
            
            statusEffect.StatusEffectStart();
            
            InterruptStatusEffects(statusEffectConfigSo.StatusEffectToInterrupt);
            
            _activeStatusEffects.Add(statusEffectConfigSo.AffectedStatId, statusEffect);
            OnStatusEffectAdded?.Invoke(statusEffect);
            
            //statusEffect.OnStatusEffectDone += RemoveStatusEffect;
#if UNITY_EDITOR
            Debug.Log($"Add Statues Effect {statusEffectConfigSo.StatusEffectName} on {_entity.EntityTransform.name}, Affected stat is {statusEffectConfigSo.AffectedStatName}");
#endif
            return statusEffect;
        }

        private void InterruptStatusEffects(IEnumerable<StatusEffectConfigSo> effectConfigSos)
        {
            foreach (var effectConfigSo in effectConfigSos)
            {
                if (_activeStatusEffects.TryGetValue(effectConfigSo.AffectedStatId,out var statusEffect))
                {
                    statusEffect.StatusEffectInterrupt();
                    _activeStatusEffects.Remove(effectConfigSo.AffectedStatId);
                    OnStatusEffectInterrupt?.Invoke(statusEffect.AffectedStatId);
                }
            }
        }

        private void RemoveStatusEffect(int id)
        {
            if(_activeStatusEffects.TryGetValue(id, out BaseStatusEffect baseStatusEffect))
            {
                //statusEffectConfigSo.OnStatusEffectDone -= RemoveStatusEffect;
                OnStatusEffectRemoved?.Invoke(baseStatusEffect.AffectedStatId);
                _activeStatusEffects.Remove(id);
            }
        }
    }
}