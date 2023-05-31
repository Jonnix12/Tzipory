using System.Collections;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO;

namespace Tzipory.VisualSystem.EffectSequence.EffectType
{
    public class TransformEffectAction : BaseEffectAction
    {
        public TransformEffectAction(BaseEffectActionSO effectActionSO) : base(effectActionSO)
        {
            var config = GetConfig<TransformEffectActionSO>(effectActionSO);
        }

        protected override IEnumerator ProcessEffect(IEntityVisualComponent visualComponent)
        {
            throw new System.NotImplementedException();
        }
    }
}