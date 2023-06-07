using System;
using System.Collections.Generic;
using Factory.EffectActionFactory;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;
using UnityEngine.Events;

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
        
        [SerializeField] private List<BaseEffectActionSO> _effectActionSos;

        private List<BaseEffectAction> _effectActions;
        
        private List<BaseEffectAction> _activeEffectActions;

        private IEntityVisualComponent _visualComponent;
        
        private int _currentEffectActionIndex;

        public bool IsActive { get; private set; }

        public string SequenceName => _sequenceName;

        public int ID => _id;
        
        public void Init(IEntityVisualComponent visualComponent)
        {
            _visualComponent  = visualComponent;
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

            _visualComponent.GameEntity.EntityTimer.StartNewTimer(_startDelay, PlayAction);
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
            _activeEffectActions.Clear();
        }

        private void PlayAction()
        {
            if (_currentEffectActionIndex == _effectActions.Count)
                ResetSequence();
            
            var effectAction = _effectActions[_currentEffectActionIndex];
            
            if (!effectAction.IsActive)
            {
                effectAction.PlayAction();
                _activeEffectActions.Add(effectAction);
                effectAction.OnEffectActionComplete += OnActionComplete;
                _currentEffectActionIndex++;
            }

            if (_currentEffectActionIndex == _effectActions.Count)
            {
                return;
            }

            if (_effectActions[_currentEffectActionIndex].ActionStartType == EffectActionStartType.WithPrevious && _currentEffectActionIndex + 1 < _effectActions.Count)
                PlayAction();
        }
        
        private void OnActionComplete(BaseEffectAction effectAction)
        {
            effectAction.OnEffectActionComplete -= OnActionComplete;
            _activeEffectActions.Remove(effectAction);

            if (_currentEffectActionIndex == _effectActions.Count)
            {
                _visualComponent.GameEntity.EntityTimer.StartNewTimer(_endDelay, OnCompleteEffectSequence);
                return;
            }

            if (_currentEffectActionIndex == 0)
                return;

            if (!_effectActions[_currentEffectActionIndex - 1].IsActive)
                PlayAction();
        }

        public void StopSequence()
        {
            ResetSequence();
        }
    }
}