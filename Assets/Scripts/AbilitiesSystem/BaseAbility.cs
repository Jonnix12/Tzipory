using System;
using System.Collections;
using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.Helpers;
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public abstract class BaseAbility
    {
        public string AbilityName { get; }
        public int AbilityId { get; }

        public TargetType TargetType { get; }//not in use

        public EffectType EffectType { get; }//not in use
        
        protected Stat Cooldown { get; }

        protected Stat CastTime { get; }

        protected bool IsReady { get; private set; }

        protected Dictionary<string, Stat> StatsValue { get; }

        protected List<StatusEffectConfig> StatusEffect { get; }
        

        protected readonly BaseAbilityCaster abilityCaster;//may need to be remove
        
        protected IEntityTargetAbleComponent entityCaster;

        protected BaseAbility(IEntityTargetAbleComponent entityCaster,AbilityConfig config)
        {
            this.entityCaster = entityCaster;
            
            AbilityName = config.AbilityName;
            AbilityId = config.AbilityId;
            
            TargetType = config.TargetType;
            EffectType = config.EffectType;

            StatsValue = new Dictionary<string, Stat>();

            foreach (var statConfig in config.StatsConfig)
            {
                var stat = new Stat(statConfig.StatName, statConfig.BaseValue, statConfig.MaxValue,statConfig.ID);
                StatsValue.Add(stat.Name,stat);
            }

            Cooldown = new Stat(config.Cooldown.StatName, config.Cooldown.BaseValue, config.Cooldown.MaxValue,
                config.Cooldown.ID);
            CastTime = new Stat(config.CastTime.StatName, config.CastTime.BaseValue, config.CastTime.MaxValue,
                config.CastTime.ID);

            StatusEffect = config.StatusEffect;
        }

        public IEnumerator Execute(IEntityTargetAbleComponent target)
        {
            if (!IsReady)
                yield break;

            yield return new WaitForSeconds(CastTime.CurrentValue);//may need to by set in tick
            
            Cast(target);
            
            CoroutineHelper.Instance.StartCoroutine(StartCooldown());
        }

        protected abstract void Cast(IEntityTargetAbleComponent target);

        private IEnumerator StartCooldown()//may need to by set in tick
        {
            IsReady = false;

            yield return new WaitForSeconds(Cooldown.CurrentValue);

            IsReady = true;
        }
    }

    public abstract class BaseAbilityCaster
    {
        public abstract void Cast(IEntityTargetAbleComponent target);
    }

    public class StatusEffectAbilityCaster : BaseAbilityCaster
    {
        private BaseStatusEffect[] _statusEffects;
        
        public StatusEffectAbilityCaster(BaseStatusEffect[] statusEffects)
        {
            _statusEffects = statusEffects;
        }

        public override void Cast(IEntityTargetAbleComponent target)
        {
            foreach (var statusEffect in _statusEffects)
                target.StatusHandler.AddStatusEffect(statusEffect);
        }
    }

    public class ActionAbilityCaster : BaseAbilityCaster
    {
        private AbilityActionType _abilityType;
        private float _amount;
        
        public ActionAbilityCaster(AbilityActionType abilityType,float amount)
        {
            _abilityType = abilityType;
            _amount = amount;
        }

        public override void Cast(IEntityTargetAbleComponent target)
        {
            switch (_abilityType)
            {
                case AbilityActionType.Heal:
                    target.Heal(_amount);
                    break;
                case AbilityActionType.Damage:
                    target.TakeDamage(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_abilityType), _abilityType, null);
            }
        }
    }

    public enum AbilityActionType
    {
        Heal,
        Damage
    }

    public enum TargetType
    {
        Self,
        Enemy,
        Ally,
    }
    
    public enum EffectType
    {
        Positive,
        Negative,
    }
}