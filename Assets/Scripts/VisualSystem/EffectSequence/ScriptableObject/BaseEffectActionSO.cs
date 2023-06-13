using UnityEngine;
using UnityEngine.Serialization;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectActionSO : ScriptableObject
    {
        [FormerlySerializedAs("_isHaveUnDo")] [SerializeField] private bool  haveUndo;
        
        public abstract EffectActionType ActionType { get;  }
        public float StartDelay { get; set; }
        public float EndDelay { get; set; }
        public bool HaveUndo => haveUndo;
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