using System;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectAction
    {
        public event Action OnEffectActionStart;
        public event Action<BaseEffectAction> OnEffectActionComplete;

        private readonly float _startDelay;
        protected readonly float _endDelay;

        protected IEntityVisualComponent VisualComponent;
        
        public EffectActionStartType ActionStartType { get; }

        public bool IsActive { get; private set; }

        protected BaseEffectAction(BaseEffectActionSO effectActionSO,IEntityVisualComponent visualComponent)
        {
            _startDelay = effectActionSO.StartDelay;
            _endDelay = effectActionSO.EndDelay;
            ActionStartType = effectActionSO.ActionStartType;
            VisualComponent = visualComponent;
        }
        
        public void PlayAction()
        {
            StartEffectAction();
        }

        protected virtual void StartEffectAction()
        {
            IsActive = true;
            
            GAME_TIME.TimerHandler.StartNewTimer(_startDelay,ExecuteEffect);
            
            OnEffectActionStart?.Invoke();
        }

        private void ExecuteEffect()
        {
            ProcessEffect();
            //may need to add Effect time
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

        protected abstract void ProcessEffect();
        protected abstract void RestEffect();
    }

    public enum EffectActionStartType
    {
        WithPrevious,
        AfterPrevious,
    }
}