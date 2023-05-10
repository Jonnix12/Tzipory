using System;

namespace Tzipory.EntitySystem.StatusSystem
{
    public class StatModifier
    {
        public StatusModifierType ModifierType { get; private set; }    
        public Stat Modifier { get; private set; }
        
        private float  _value;

        public StatModifier(Stat modifier,StatusModifierType statusModifierType)
        {
            Modifier = modifier;
            ModifierType = statusModifierType;
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