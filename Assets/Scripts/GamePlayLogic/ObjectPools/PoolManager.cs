using System;
using SerializeData.VisualSystemSerializeData;
using Tzipory.Factory;
using Tzipory.Systems.PoolSystem;
using Tzipory.VisualSystem.EffectSequence;
using Tzipory.VisualSystem.EffectSequence.EffectType;

namespace Tzipory.GamePlayLogic.ObjectPools
{
    public class PoolManager
    {
        private static  ObjectPool<EffectSequence> EffectActionPool { get; set; }
        private static ObjectPool<ColorEffectAction> ColorEffectPool { get; set; }
        private static ObjectPool<SoundEffectAction> SoundEffectPool { get; set; }
        private static ObjectPool<TransformEffectAction> TransformEffectPool { get; set; }
        
        public PoolManager()
        {
            EffectActionPool = new ObjectPool<EffectSequence>(new EffectSequenceFactory(),10);
            ColorEffectPool = new ObjectPool<ColorEffectAction>(new ColorEffectActionFactory(),15);
            SoundEffectPool = new ObjectPool<SoundEffectAction>(new SoundEffectActionFactory(),15);
            TransformEffectPool = new ObjectPool<TransformEffectAction>(new TransformEffectActionFactory(),15);
        }

        public static BaseEffectAction GetEffectAction(EffectActionContainerData actionContainerData)
        {
            return actionContainerData.EffectActionSo.ActionType switch
            {
                EffectActionType.Transform => TransformEffectPool.GetObject(),
                EffectActionType.Color => ColorEffectPool.GetObject(),
                EffectActionType.Outline => throw new NotImplementedException(),
                EffectActionType.PopUp => throw new NotImplementedException(),
                EffectActionType.ParticleEffects => throw new NotImplementedException(),
                EffectActionType.Sound => SoundEffectPool.GetObject(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static EffectSequence GetEffectSequence(EffectSequenceData sequenceData)
        {
            return  EffectActionPool.GetObject();
        }
    }
}