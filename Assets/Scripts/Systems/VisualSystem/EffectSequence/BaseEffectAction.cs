using System;
using SerializeData.VisualSystemSerializeData;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectAction
    {
        public event Action<BaseEffectAction> OnEffectActionComplete;
        
        private readonly float _startDelay;
        
        private ITimer _startDelayTimer;
        private ITimer _endDelayTimer;
        private ITimer _completeTimer;
        
        public EffectActionStartType ActionStartType { get; }
        public bool IsActive { get; private set; }
        
        protected abstract float Duration { get; }
        protected IEntityVisualComponent VisualComponent { get; }

        private bool DisableUndo { get; }
        private bool IsStarted { get; set; }

        protected BaseEffectAction(EffectActionContainerData actionContainerData,IEntityVisualComponent visualComponent)
        {
            VisualComponent = visualComponent;
            _startDelay = actionContainerData.StartDelay;
            ActionStartType = actionContainerData.EffectActionStart;
            DisableUndo = actionContainerData.DisableUndo;
        }
        
        public void ActivateActionEffect()                                                           
        {                                                                                            
            IsActive = true;                                                                         
            _startDelayTimer = VisualComponent.GameEntity.EntityTimer.StartNewTimer(_startDelay);    
        }                                                                                            

        private void StartEffectAction()
        {
            IsStarted = true;
            OnStartEffectAction();
            _completeTimer = VisualComponent.GameEntity.EntityTimer.StartNewTimer(Duration);
        }
        
        private void CompleteEffectAction()
        {
            if (!DisableUndo)
                OnUndoEffectAction();
            
            OnCompleteEffectAction();
            
            IsActive = false;
            IsStarted = false;
            
            OnEffectActionComplete?.Invoke(this);
        }

        public void InterruptEffectAction()
        {
            IsStarted = false;
            IsActive = false;
            
            VisualComponent.GameEntity.EntityTimer.StopTimer(_completeTimer);
            //need to add more logic!

            if (!DisableUndo)
                OnUndoEffectAction();

            OnInterruptEffectAction();
        }
        
        public void UpdateEffectAction()
        {
            if (!IsActive)
                return;

            if (!_startDelayTimer.IsDone)
                return;

            if (!IsStarted)
                StartEffectAction();

            OnProcessEffectAction();

            if (_completeTimer.IsDone)
                CompleteEffectAction();
        }
        
        protected T GetConfig<T>(BaseEffectActionSO effectActionSO) where T : BaseEffectActionSO
        {
            if (effectActionSO is T effectActionSo)
                return  effectActionSo;

            throw new Exception($"Can't cast {effectActionSO.GetType()} to {typeof(T)}");
        }
        
        protected abstract void OnStartEffectAction();
        protected abstract void OnProcessEffectAction();
        protected abstract void OnCompleteEffectAction();
        protected abstract void OnUndoEffectAction();
        protected abstract void OnInterruptEffectAction();
    }

    public enum EffectActionStartType
    {
        WithPrevious,
        AfterPrevious,
    }
}