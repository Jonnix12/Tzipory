using UnityEngine;
using UnityEngine.Serialization;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectActionSO : ScriptableObject
    {
        [SerializeField] public bool  _haveUndo;//temp
        
        public abstract EffectActionType ActionType { get;  }
        public float StartDelay { get; set; }
        public float EndDelay { get; set; }
        public bool HaveUndo => _haveUndo;
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