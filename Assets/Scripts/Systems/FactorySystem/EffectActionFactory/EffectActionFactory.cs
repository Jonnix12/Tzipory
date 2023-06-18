using System;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence;
using Tzipory.VisualSystem.EffectSequence.EffectType;
using Tzipory.VisualSystem.EffectSequence.EffectType.Sound;

namespace Factory.EffectActionFactory
{
    public class EffectActionFactory
    {
        public static BaseEffectAction GetEffectAction(EffectActionContainer actionContainer,IEntityVisualComponent visualComponent)//need to change happens in update 
        {
            //may need to add null check
            var effectActionSO = actionContainer.EffectActionSo;
            
            switch (effectActionSO.ActionType)
            {
                case EffectActionType.Transform:
                    return new TransformEffectAction(actionContainer);
                case EffectActionType.Color:
                    return new ColorEffectAction(actionContainer);
                case EffectActionType.Outline:
                    throw  new NotImplementedException();
                case EffectActionType.PopUp:
                    throw  new NotImplementedException();
                case EffectActionType.ParticleEffects:
                    throw  new NotImplementedException();
                case EffectActionType.Sound:
                    return new SoundEffectAction(actionContainer);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}