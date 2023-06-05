using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO;

namespace Tzipory.VisualSystem.EffectSequence.EffectType
{
    public class TransformEffectAction : BaseEffectAction
    {
        public TransformEffectAction(BaseEffectActionSO effectActionSO,IEntityVisualComponent visualComponent) : base(effectActionSO,visualComponent)
        {
            var config = GetConfig<TransformEffectActionSO>(effectActionSO);
        }

        protected override void ProcessEffect()
        {
            throw new System.NotImplementedException();
        }

        protected override void RestEffect()
        {
            throw new System.NotImplementedException();
        }
    }
}