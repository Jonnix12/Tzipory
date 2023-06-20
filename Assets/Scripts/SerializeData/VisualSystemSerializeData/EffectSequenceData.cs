using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SerializeData.VisualSystemSerializeData
{
    [Serializable]
    public class EffectSequenceData
    {
        [SerializeField,ReadOnly] private string _sequenceName;//need to see if can stay readOnly
        [SerializeField,ReadOnly] private int _id;
        
        [SerializeField] private float _startDelay;
        [SerializeField] private float _endDelay;
        
        [SerializeField] private bool isInterruptable;
        
        [SerializeField] private List<EffectActionContainerData> _effectActionContainers;

        public string SequenceName
        {
            get => _sequenceName;
#if UNITY_EDITOR
            set => _sequenceName = value;
#endif
        }

        public int ID {get => _id;
#if UNITY_EDITOR
            set => _id = value;}
#endif

        public float StartDelay => _startDelay;

        public float EndDelay => _endDelay;

        public bool IsInterruptable => isInterruptable;

        public List<EffectActionContainerData> EffectActionContainers => _effectActionContainers;
    }
}