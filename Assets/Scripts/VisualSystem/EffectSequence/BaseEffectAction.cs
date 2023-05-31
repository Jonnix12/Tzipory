using System;
using System.Collections;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.Helpers;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence
{
    public abstract class BaseEffectAction
    {
        public event Action OnEffectActionStart;
        public event Action<BaseEffectAction> OnEffectActionComplete;

        private readonly float _startDelay;
        private readonly float _endDelay;
        
        private float _currentStartDelay;
        private float _currentEndDelay;

        public EffectActionStartType ActionStartType { get; }

        public bool IsActive { get; private set; }

        protected BaseEffectAction(BaseEffectActionSO effectActionSO)
        {
            _startDelay = effectActionSO.StartDelay;
            _endDelay = effectActionSO.EndDelay;
            ActionStartType = effectActionSO.ActionStartType;
        }
       
        public void PlayAction(IEntityVisualComponent visualComponent)
        {
            IsActive = true;
            
            _currentStartDelay = _startDelay;
            
            while (_currentStartDelay > 0)
                _currentStartDelay -= Time.deltaTime;

            OnEffectActionStart?.Invoke();
            
            CoroutineHelper.Instance.StartCoroutineHelper(ProcessEffect(visualComponent));
            
            _currentEndDelay = _endDelay;
            
            while (_currentEndDelay > 0)
                _currentEndDelay -= Time.deltaTime;
            
            IsActive = false;
            
            OnEffectActionComplete?.Invoke(this);
        }
        
        protected T GetConfig<T>(BaseEffectActionSO effectActionSO) where T : BaseEffectActionSO
        {
            if (effectActionSO is T effectActionSo)
                return  effectActionSo;

            throw  new Exception($"Can't cast {effectActionSO.GetType()} to {typeof(T)}");
        }

        protected abstract IEnumerator ProcessEffect(IEntityVisualComponent visualComponent);
    }

    public enum EffectActionStartType
    {
        WithPrevious,
        AfterPrevious,
    }
}