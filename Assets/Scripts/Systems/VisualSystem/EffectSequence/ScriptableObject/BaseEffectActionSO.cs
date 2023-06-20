using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectActionSO : ScriptableObject
    {
        public abstract EffectActionType ActionType { get; }
    }

    public enum EffectActionType
    {
        Transform,
        Color,
        Outline,
        PopUp,
        ParticleEffects,
        Sound,
    }
}