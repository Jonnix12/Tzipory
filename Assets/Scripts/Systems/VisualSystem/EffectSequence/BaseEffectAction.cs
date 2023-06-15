using System;
using System.Timers;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectAction
    {
        public event Action OnEffectActionStart;
        public event Action<BaseEffectAction> OnEffectActionComplete;
        

        private ITimer _completeTimer;
        
        private readonly float _startDelay;
        protected readonly float _endDelay;

        public abstract float Duration { get; }

        public bool IsStackable { get; }

        public bool DisableUndo { get; }
        
        public EffectActionStartType ActionStartType { get; }

        public bool IsActive { get; private set; }

        protected BaseEffectAction(BaseEffectActionSO effectActionSO)
        {
            _startDelay = effectActionSO.StartDelay;
            _endDelay = effectActionSO.EndDelay;
            ActionStartType = effectActionSO.ActionStartType;
            IsStackable = effectActionSO.IsStackable;
            DisableUndo = effectActionSO.DisableUndo;
        }

        public virtual void StartEffectAction()
        {
            IsActive = true;

            OnEffectActionStart?.Invoke();
        }


        protected virtual void OnCompleteEffectAction()
        {
            IsActive = false;
            
            OnEffectActionComplete?.Invoke(this);
        }

        protected T GetConfig<T>(BaseEffectActionSO effectActionSO) where T : BaseEffectActionSO
        {
            if (effectActionSO is T effectActionSo)
                return  effectActionSo;

            throw new Exception($"Can't cast {effectActionSO.GetType()} to {typeof(T)}");
        }

        public virtual void ProcessEffect(IEntityVisualComponent visualComponent)
        { 
            _completeTimer = visualComponent.GameEntity.EntityTimer.StartNewTimer(Duration, OnCompleteEffectAction);
        }

        public virtual void UndoEffect(IEntityVisualComponent visualComponent)
        {
            
        }

        public virtual void InterruptEffectAction(IEntityVisualComponent visualComponent)
        {
            visualComponent.GameEntity.EntityTimer.StopTimer(_completeTimer);
        }
    }

    public enum EffectActionStartType
    {
        WithPrevious,
        AfterPrevious,
    }
}