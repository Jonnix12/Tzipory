using System;
using Tzipory.Tools.Enums;
using Tzipory.VisualSystem.EffectSequence;
using UnityEngine;

namespace SerializeData.VisualSystemSerializeData
{
    [Serializable]
    public class EffectActionContainerData
    {
        [HideInInspector] private bool _isStackable;//need to see what to do
        [SerializeField] private bool _disableUndo;
        [SerializeField] private float _startDelay;
        [SerializeField] private float _endDelay;
        [SerializeField] private ActionStartType actionStart; 
        
        [SerializeField] private BaseEffectActionSO _effectActionSO;

        public bool IsStackable => _isStackable;

        public bool DisableUndo => _disableUndo;

        public float StartDelay => _startDelay;

        public float EndDelay => _endDelay;

        public ActionStartType ActionStart => actionStart;
        
        public BaseEffectActionSO EffectActionSo => _effectActionSO;
    }
}