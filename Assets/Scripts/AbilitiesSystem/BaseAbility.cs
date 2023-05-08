using System;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;

namespace Tzipory.AbilitiesSystem
{
    public abstract class BaseAbility
    {
        protected TargetType _targetType;
        protected EffectType _effectType;
        protected Stat _cooldown;

        private string _abilityName;

        public string AbilityName => _abilityName;

        public Stat Cooldown => _cooldown;

        protected readonly BaseAbilityCaster abilityCaster;
        
        protected IEntityTargetAbleComponent entityCaster;

        protected BaseAbility(IEntityTargetAbleComponent entityCaster, float cooldown)
        {
            this.entityCaster = entityCaster;
            _cooldown = new Stat("Ability cooldown",cooldown,7);
        }

        protected BaseAbility(IEntityTargetAbleComponent entityCaster,float cooldown,BaseStatusEffect[] statusEffects) : this(entityCaster,cooldown)=>
            abilityCaster = new StatusEffectAbilityCaster(statusEffects);
        
        protected BaseAbility(IEntityTargetAbleComponent entityCaster,float cooldown,AbilityActionType abilityType,float amount) : this(entityCaster,cooldown)=>
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