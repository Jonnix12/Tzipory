using System;
using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence;
using Tzipory.VisualSystem.EffectSequence.EffectType;
using Tzipory.VisualSystem.EffectSequence.EffectType.Sound;

namespace Factory.EffectActionFactory
{
    public class EffectActionFactory
    {
        private static Dictionary<EffectActionType, Type> _effectActionType = new()
        {
            { EffectActionType.Transform, typeof(TransformEffectAction) },
            { EffectActionType.Color, typeof(ColorEffectAction) },
        };

        public static BaseEffectAction GetEffectAction(BaseEffectActionSO effectActionSO,IEntityVisualComponent visualComponent)//need to change happens in update 
        {
            // if (!_effectActionType.TryGetValue(effectActionSO.ActionType, out var actionType))
            //     throw new Exception("No such effect action type");
            //
            // Type[] parameters = new[]
            // {
            //     typeof(BaseEffectActionSO),
            //     typeof(IEntityVisualComponent)
            // };
            //     
            // var constructorInfo = actionType.GetConstructor(parameters);
            //
            // if (constructorInfo is null)
            //     throw  new Exception("No such constructor");
            //     
            // return (BaseEffectAction)constructorInfo.Invoke(new object[] { effectActionSO,visualComponent });

            switch (effectActionSO.ActionType)
            {
                case EffectActionType.Transform:
                    return new TransformEffectAction(effectActionSO);
                    break;
                case EffectActionType.Color:
                    return new ColorEffectAction(effectActionSO);
                    break;
                case EffectActionType.Outline:
                    throw  new NotImplementedException();
                    break;
                case EffectActionType.PopUp:
                    throw  new NotImplementedException();
                    break;
                case EffectActionType.ParticleEffects:
                    throw  new NotImplementedException();
                    break;
                case EffectActionType.Sound:
                    return  new SoundEffectAction(effectActionSO);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}