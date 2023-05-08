using System;
using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;

namespace Tzipory.AbilitiesSystem
{
    public abstract class BaseAbility
    {
        private const string COOLDOWN_KEY = "Cooldown";
        
        protected TargetType _targetType;
        protected EffectType _effectType;
        protected Stat _cooldown;
        protected Dictionary<string,Stat> _statsValue;

        private string _abilityName;

        public string AbilityName => _abilityName;

        public Stat Cooldown => _cooldown;

        public TargetType TargetType => _targetType;

        public EffectType EffectType => _effectType;

        public Dictionary<string, Stat> StatsValue => _statsValue;

        protected readonly BaseAbilityCaster abilityCaster;
        
        protected IEntityTargetAbleComponent entityCaster;

        private BaseAbility(IEntityTargetAbleComponent entityCaster,BaseAbilityConfig config)
        {
            this.entityCaster = entityCaster;
            
            _abilityName = config.AbilityName;
            _targetType = config.TargetType;
            _effectType = config.EffectType;
            
            _statsValue = new Dictionary<string, Stat>();

            foreach (var statConfig in config.StatsConfig)
            {
                var stat = new Stat(statConfig.StatName, statConfig.BaseValue, statConfig.MaxValue,statConfig.ID);
                _statsValue.Add(stat.Name,stat);
            }

            _cooldown = new Stat(config.Cooldown.StatName, config.Cooldown.BaseValue, config.Cooldown.MaxValue,
                config.Cooldown.ID);
        }
        
        protected BaseAbility(IEntityTargetAbleComponent entityCaster,BaseAbilityConfig config,BaseStatusEffect[] statusEffects) : this(entityCaster,config)=>
            abilityCaster = new StatusEffectAbilityCaster(statusEffects);
        
        protected BaseAbility(IEntityTargetAbleComponent entityCaster,BaseAbilityConfig config,AbilityActionType abilityType,float amount) : this(entityCaster,config)=>
            abilityCaster = new ActionAbilityCaster(abilityType,amount);

        public abstract void Cast(IEntityTargetAbleComponent target);
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
    
    public enum AbilityType
    {
        StatusEffectAbility,
        ActionAbility
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