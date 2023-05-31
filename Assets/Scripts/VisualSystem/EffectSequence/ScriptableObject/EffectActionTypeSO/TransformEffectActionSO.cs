using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class TransformEffectActionSO : BaseEffectActionSO
    {
        public override EffectActionType ActionType => EffectActionType.Transform;
    }
}