using System;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private StatusModifierType _statusModifierType;
        [SerializeField] private Stat _modifier;

        public StatusModifierType ModifierType => _statusModifierType;
        public Stat Modifier => _modifier;
        
        private float _value;

        public StatModifier(Stat modifier,StatusModifierType statusModifierType)
        {
            _modifier = new Stat(modifier.Name,modifier.BaseValue,modifier.MaxValue,modifier.Id);
            _statusModifierType = statusModifierType;
        }

        public void Process(Stat stat)
        {
            _value = Modifier.CurrentValue;
            
            switch (ModifierType)
            {
                case StatusModifierType.Addition:
                    stat.AddToValue(_value);
                    break;
                case StatusModifierType.Multiplication:
                     stat.MultiplyValue(_value);
                    break;
                case StatusModifierType.Percentage:
                     stat.MultiplyValue(_value);//may meed to by change
                    break;
                case StatusModifierType.Set:
                    stat.SetValue(_value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ModifierType), ModifierType, null);
            }
        }

        public void UnDo(Stat stat)
        {
            switch (ModifierType)
            {
                case StatusModifierType.Addition:
                    stat.ReduceFromValue(_value);
                    break;
                case StatusModifierType.Multiplication:
                    stat.DivideValue(_value);
                    break;
                case StatusModifierType.Percentage:
                    stat.DivideValue(_value);//may meed to by change
                    break;
                case StatusModifierType.Set:
                    //set dos not have a undo
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ModifierType), ModifierType, null);
            }
        }
    }
        
    public enum StatusModifierType
    {
        Set,
        Addition,
        Multiplication,
        Percentage
    }
}