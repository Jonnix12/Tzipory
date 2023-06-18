using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectActionSO : ScriptableObject
    {
        // [SerializeField] private bool _isStackable;
        // [SerializeField] private bool _disableUndo;
        // [SerializeField] private float _startDelay;
        // [SerializeField] private float _endDelay;
        // [SerializeField] private EffectActionStartType _effectActionStart;
        //
        public abstract EffectActionType ActionType { get; }
        // public EffectActionStartType ActionStartType => _effectActionStart;
        // public float StartDelay => _startDelay;
        // public float EndDelay => _endDelay;
        //
        // public bool IsStackable => _isStackable;
        //
        // public bool DisableUndo => _disableUndo;

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