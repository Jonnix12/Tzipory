using System;
using SerializeData.VisualSystemSerializeData;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence;
using Tzipory.VisualSystem.EffectSequence.EffectType;
using Tzipory.VisualSystem.EffectSequence.EffectType.Sound;

namespace Factory
{
    public class EffectActionFactory
    {
        public static BaseEffectAction GetEffectAction(EffectActionContainerData actionContainerData,IEntityVisualComponent visualComponent)//need to change happens in update 
        {
            //may need to add null check
            var effectActionSO = actionContainerData.EffectActionSo;
            
            switch (effectActionSO.ActionType)
            {
                case EffectActionType.Transform:
                    return new TransformEffectAction(actionContainerData,visualComponent);
                case EffectActionType.Color:
                    return new ColorEffectAction(actionContainerData,visualComponent);
                case EffectActionType.Outline:
                    throw  new NotImplementedException();
                case EffectActionType.PopUp:
                    throw  new NotImplementedException();
                case EffectActionType.ParticleEffects:
                    throw  new NotImplementedException();
                case EffectActionType.Sound:
                    return new SoundEffectAction(actionContainerData,visualComponent);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}