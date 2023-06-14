using System;
using System.Collections.Generic;
using Factory.EffectActionFactory;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Tzipory.VisualSystem.EffectSequence
{
    [Serializable]
    public class EffectSequence
    {
        public event Action<int> OnEffectSequenceComplete;

        public UnityEvent OnEffectSequenceStart; 
        public UnityEvent OnEffectSequenceCompleteUnityEvent;

        [SerializeField] private string _sequenceName;
        [SerializeField] private int _id;
        
        [SerializeField] private float _startDelay;
        [SerializeField] private float _endDelay;
        
        [FormerlySerializedAs("_inInterruptable")] [SerializeField] private bool isInterruptable;
        
        [SerializeField] private List<BaseEffectActionSO> _effectActionSos;

        private List<BaseEffectAction> _effectActions;
        
        private List<BaseEffectAction> _activeEffectActions;

        private IEntityVisualComponent _entityVisualComponent;
        
        private int _currentEffectActionIndex;

        public bool IsActive { get; private set; }

        public bool IsInterruptable => isInterruptable;

        public string SequenceName => _sequenceName;

        public int ID {get => _id;
#if UNITY_EDITOR
            set => _id = value;}
#endif

        public void Init(IEntityVisualComponent visualComponent)
        {
            _entityVisualComponent  = visualComponent;
            _activeEffectActions = new List<BaseEffectAction>();
            _effectActions = new List<BaseEffectAction>();

            foreach (var effectActionSO in _effectActionSos)
            {
                var effectAction = EffectActionFactory.GetEffectAction(effectActionSO,visualComponent);
                _effectActions.Add(effectAction);
            }
            
            _currentEffectActionIndex = 0;
        }
        
        public void PlaySequence()
        {
            StartEffectSequence();
        }

        private void StartEffectSequence()
        {
            IsActive = true;
            
            OnEffectSequenceStart.Invoke();

            _entityVisualComponent.GameEntity.EntityTimer.StartNewTimer(_startDelay, PlayAction);
        }

        private void OnCompleteEffectSequence()
        {
            IsActive = false;
            
            OnEffectSequenceComplete?.Invoke(ID);
            OnEffectSequenceCompleteUnityEvent.Invoke();
            
            ResetSequence();
        }

        private void ResetSequence()
        {
            _currentEffectActionIndex = 0;
            //_activeEffectActions.Clear();
        }

        private void PlayAction()
        {
            if (_effectActions.Count == 0)
                return;

            if (_currentEffectActionIndex == _effectActions.Count)
                ResetSequence();
            
            var effectAction = _effectActions[_currentEffectActionIndex];
            
            if (!effectAction.IsActive || effectAction.IsStackable && effectAction.IsActive)
            {
                if(effectAction.IsActive)
                    effectAction.InterruptEffectAction(_entityVisualComponent);
                
                effectAction.StartEffectAction();
                _activeEffectActions.Add(effectAction);
                effectAction.ProcessEffect(_entityVisualComponent);
                effectAction.OnEffectActionComplete += OnActionComplete;
                effectAction.OnEffectActionInterrupt += OnEffectActionInterrupt;
            }
            _currentEffectActionIndex++;

            if (_currentEffectActionIndex == _effectActions.Count)
                return;

            if (_effectActions[_currentEffectActionIndex].ActionStartType == EffectActionStartType.WithPrevious)
                PlayAction();
        }
        
        private void OnActionComplete(BaseEffectAction effectAction)
        {
            if (!effectAction.DisableUndo)
                effectAction.RestEffect(_entityVisualComponent);
            
            CompleteAction(effectAction);

            if (_currentEffectActionIndex == 0)
                return;

            if (!_effectActions[_currentEffectActionIndex - 1].IsActive)
                PlayAction();
        }

        private void OnEffectActionInterrupt(BaseEffectAction effectAction)
        {
            CompleteAction(effectAction);
        }

        private void CompleteAction(BaseEffectAction effectAction)
        {
            effectAction.OnEffectActionInterrupt -= OnEffectActionInterrupt;
            effectAction.OnEffectActionComplete -= OnActionComplete;
            
            _activeEffectActions.Remove(effectAction);
            
            if (_currentEffectActionIndex == _effectActions.Count)
                _entityVisualComponent.GameEntity.EntityTimer.StartNewTimer(_endDelay, OnCompleteEffectSequence);
        }

        public void StopSequence()
        {
            ResetSequence();
        }
    }
}