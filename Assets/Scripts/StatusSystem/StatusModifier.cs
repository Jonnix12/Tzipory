using System;

namespace Tzipory.StatusSystem
{
    public class StatusModifier
    {
        public StatusModifierType ModifierType { get; private set; }    
        public float Modifier { get; private set; }

        public StatusModifier(float modifier,StatusModifierType statusModifierType)
        {
            Modifier = modifier;
            ModifierType = statusModifierType;
        }

        public float Modifie(float value)
        {
            switch (ModifierType)
            {
                case StatusModifierType.Addition:
                    return value + Modifier;
                case StatusModifierType.Multiplication:
                    return value * Modifier;
                case StatusModifierType.Percentage:
                    return value * Modifier;//may meed to by change
                default:
                    throw new ArgumentOutOfRangeException(nameof(ModifierType), ModifierType, null);
            }
        }
    }
        
    public enum StatusModifierType
    {
        Addition,
        Multiplication,
        Percentage
    }
}