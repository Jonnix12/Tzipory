using System;
using SerializeData.VisualSystemSerializeData;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.Tools.Enums;

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
        
        public ActionStartType ActionStartType { get; }

        public bool IsActive { get; private set; }

        protected BaseEffectAction(EffectActionContainerData actionContainerData)
        {
            _startDelay = actionContainerData.StartDelay;
            _endDelay = actionContainerData.EndDelay;
            ActionStartType = actionContainerData.ActionStart;
            IsStackable = actionContainerData.IsStackable;
            DisableUndo = actionContainerData.DisableUndo;
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
            IsActive = false;
        }
    }
}