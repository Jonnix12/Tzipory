﻿using System;

namespace Tzipory.StatusSystem
{
    public class StatModifier
    {
        public StatusModifierType ModifierType { get; private set; }    
        public float Modifier { get; private set; }

        public StatModifier(float modifier,StatusModifierType statusModifierType)
        {
            Modifier = modifier;
            ModifierType = statusModifierType;
        }

        public void Process(Stat stat)
        {
            switch (ModifierType)
            {
                case StatusModifierType.Addition:
                     stat.AddToValue(Modifier);
                    break;
                case StatusModifierType.Multiplication:
                     stat.MultiplyValue(Modifier);
                    break;
                case StatusModifierType.Percentage:
                     stat.MultiplyValue(Modifier);//may meed to by change
                    break;
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