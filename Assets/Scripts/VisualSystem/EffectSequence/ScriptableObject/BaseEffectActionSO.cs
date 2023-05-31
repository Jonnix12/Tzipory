using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectActionSO : ScriptableObject
    {
        public abstract EffectActionType ActionType { get;  }
        public float StartDelay { get; set; }
        public float EndDelay { get; set; }
        public EffectActionStartType ActionStartType { get; set; }
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