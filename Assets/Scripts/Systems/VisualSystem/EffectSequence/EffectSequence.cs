using System;
using System.Collections.Generic;
using System.Linq;
using Factory;
using SerializeData.VisualSystemSerializeData;
using Sirenix.OdinInspector;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence
{
    public class EffectSequence
    {
        public event Action<int> OnEffectSequenceComplete;

        #region Fields

        private List<BaseEffectAction> _effectActions;
        
        private List<BaseEffectAction> _activeEffectActions;

        private IEntityVisualComponent _entityVisualComponent;
        
        private float _startDelay;
        private float _endDelay;
        
        private int _currentEffectActionIndex;

        #endregion

        #region Properties

        public bool IsActive { get; private set; }

        public bool IsInterruptable { get; }

        public string SequenceName { get; }

        public int ID { get; }

        public bool IsAllEffectActionDone => _activeEffectActions.All(effectAction => !effectAction.IsActive);
        
        #endregion

        #region PublicMethod

        public EffectSequence(IEntityVisualComponent visualComponent,EffectSequenceData sequenceData)
        {
            SequenceName = sequenceData.SequenceName;
            ID = sequenceData.ID;
            _startDelay = sequenceData.StartDelay;
            _endDelay = sequenceData.EndDelay;
            IsInterruptable = sequenceData.IsInterruptable;
            
            _entityVisualComponent  = visualComponent;
            _activeEffectActions = new List<BaseEffectAction>();
            _effectActions = new List<BaseEffectAction>();

            foreach (var effectActionContainer in sequenceData.EffectActionContainers)
            {
                var effectAction = EffectActionFactory.GetEffectAction(effectActionContainer,visualComponent);
                _effectActions.Add(effectAction);
            }
            
            _currentEffectActionIndex = 0;
            
           
        }

        public void StopSequence()
        {
            for (int i = 0; i < _activeEffectActions.Count; i++)
            {
                InterruptEffectAction(_activeEffectActions[i]);   
            }

            ResetSequence();
        }

        #endregion

        #region PrivateMethod

        public void StartEffectSequence()
        {
            IsActive = true;
            
           // OnEffectSequenceStart.Invoke();

            _entityVisualComponent.GameEntity.EntityTimer.StartNewTimer(_startDelay, PlayActions);
        }

        private void OnCompleteEffectSequence()
        {
            IsActive = false;
            
            OnEffectSequenceComplete?.Invoke(ID);
           // OnEffectSequenceCompleteUnityEvent.Invoke();
            
            ResetSequence();
        }

        private void ResetSequence()
        {
            _currentEffectActionIndex = 0;
            _activeEffectActions.Clear();
            IsActive = false;
        }

        private void PlayActions()
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
            }
            _currentEffectActionIndex++;

            if (_currentEffectActionIndex == _effectActions.Count)
                return;

            if (_effectActions[_currentEffectActionIndex].ActionStartType == EffectActionStartType.WithPrevious)
                PlayActions();
        }
        
        private void OnActionComplete(BaseEffectAction effectAction)
        {
            if (!effectAction.DisableUndo)
                effectAction.UndoEffect(_entityVisualComponent);
            
            effectAction.OnEffectActionComplete -= OnActionComplete;
            _activeEffectActions.Remove(effectAction);

            if (_currentEffectActionIndex == _effectActions.Count && IsAllEffectActionDone)
            {
                _entityVisualComponent.GameEntity.EntityTimer.StartNewTimer(_endDelay, OnCompleteEffectSequence);
                return;
            }

            if (_currentEffectActionIndex == 0)
                return;

            if (!_effectActions[_currentEffectActionIndex - 1].IsActive)
                PlayActions();
        }

        private void InterruptEffectAction(BaseEffectAction effectAction)
        {
            effectAction.OnEffectActionComplete -= OnActionComplete;
            
            effectAction.InterruptEffectAction(_entityVisualComponent);
            _activeEffectActions.Remove(effectAction);
            
        }
        
        #endregion
    }
    
   
}