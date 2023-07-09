using System;
using System.Collections.Generic;
using System.Linq;
using Factory;
using SerializeData.VisualSystemSerializeData;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.VisualSystem.EffectSequence
{
    public class EffectSequence
    {
        public event Action<EffectSequence> OnEffectSequenceComplete;

        #region Fields

        private readonly float _startDelay;
        
        private List<BaseEffectAction> _effectActions;

        private IEntityVisualComponent _entityVisualComponent;
        
        private ITimer _startDelayTimer;
        
        private int _currentEffectActionIndex;

        private bool _isStarted;

        #endregion

        #region Properties

        public bool IsActive { get; private set; }


        public bool IsInterruptable { get; }
        
        public string SequenceName { get; }

        public int ID { get; }

        public bool AllEffectActionDone => _effectActions.All(effectAction => !effectAction.IsActive);
        
        #endregion

        #region PublicMethod

        public EffectSequence(IEntityVisualComponent visualComponent,EffectSequenceData sequenceData)
        {
            SequenceName = sequenceData.SequenceName;
            ID = sequenceData.ID;
            _startDelay = sequenceData.StartDelay;
            IsInterruptable = sequenceData.IsInterruptable;

            IsActive = false;
            _isStarted = false;
            
            _entityVisualComponent  = visualComponent;
            _effectActions = new List<BaseEffectAction>();

            foreach (var effectActionContainer in sequenceData.EffectActionContainers)
            {
                var effectAction = EffectActionFactory.GetEffectAction(effectActionContainer,visualComponent);
                effectAction.OnEffectActionComplete += OnActionComplete;
                _effectActions.Add(effectAction);
            }
            
            _currentEffectActionIndex = 0;
        }
        

        #endregion

        #region PrivateMethod

        private void OnCompleteEffectSequence()
        {
            IsActive = false;
            
            OnEffectSequenceComplete?.Invoke(this);
            
            ResetSequence();
        }
        
        private void PlayActions()
        {
            if (_effectActions.Count == 0)
                return;
            
            var effectAction = _effectActions[_currentEffectActionIndex];
            
            if (!effectAction.IsActive)
                effectAction.ActivateActionEffect();
            
            _currentEffectActionIndex++;

            if (_currentEffectActionIndex == _effectActions.Count)
                return;

            if (_effectActions[_currentEffectActionIndex].ActionStartType == EffectActionStartType.WithPrevious)
                PlayActions();
        }
        
        private void OnActionComplete(BaseEffectAction effectAction)
        {
            if (_currentEffectActionIndex == _effectActions.Count && !_effectActions[_currentEffectActionIndex - 1].IsActive)
            {
                ResetSequence();
                return;
            }

            if (!_effectActions[_currentEffectActionIndex - 1].IsActive)
                PlayActions();
        }

        #endregion

        #region PublicMethod
        
        public void ResetSequence()
        {
            foreach (var baseEffectAction in _effectActions)
            {
                if (baseEffectAction.IsActive)
                    baseEffectAction.InterruptEffectAction();
            }
            
            _currentEffectActionIndex = 0;
            IsActive = false;
        }

        public void StartEffectSequence()
        {
            _startDelayTimer = _entityVisualComponent.GameEntity.EntityTimer.StartNewTimer(_startDelay);
            
            IsActive = true;
        }

        public void UpdateEffectSequence()
        {
            if (!IsActive)
                return;

            if (_startDelayTimer.IsDone && !_isStarted)
            {
                _isStarted  = true;
                PlayActions();
            }

            for (int i = 0; i < _effectActions.Count; i++)
                _effectActions[i].UpdateEffectAction();
            
            if (_currentEffectActionIndex == _effectActions.Count && AllEffectActionDone)
                OnCompleteEffectSequence();
        }

        #endregion

        ~EffectSequence()
        {
            foreach (var effectAction in _effectActions)
            {
                effectAction.OnEffectActionComplete -= OnActionComplete;
            }
        }
    }
}