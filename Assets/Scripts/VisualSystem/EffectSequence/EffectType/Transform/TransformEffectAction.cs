using System;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence.EffectType
{
    public class TransformEffectAction : BaseEffectAction
    {
        private TransformEffectActionSO _transformEffectActionSO;

        private Vector3 _originalPosition;
        private Vector3 _originalScale;
        private Vector3 _originalRotation;

        public override float Duration
        {
            get
            {
                float moveDuration = 0;
                float scaleDuration = 0;
                float rotationDuration = 0;
                
                if (_transformEffectActionSO.HaveMovement)
                    moveDuration = _transformEffectActionSO.Movement.TimeToTransition;
                if (_transformEffectActionSO.HaveScale)
                    scaleDuration = _transformEffectActionSO.Scale.TimeToTransition;
                if (_transformEffectActionSO.HaveRotation)
                    rotationDuration  = _transformEffectActionSO.Rotation.TimeToTransition;
                
                
                return Mathf.Max(moveDuration,scaleDuration,rotationDuration);
            }
        }

        public TransformEffectAction(BaseEffectActionSO effectActionSO) : base(effectActionSO)
        {
            var config = GetConfig<TransformEffectActionSO>(effectActionSO);
            
            _transformEffectActionSO  = config;
        }

        public override void ProcessEffect(IEntityVisualComponent visualComponent)
        {
            _originalPosition = visualComponent.EntityTransform.position;
            _originalScale = visualComponent.EntityTransform.localScale;
            _originalRotation = visualComponent.EntityTransform.eulerAngles;
            
            visualComponent.EntityTransform.Transition(_transformEffectActionSO);
            base.ProcessEffect(visualComponent);
        }

        public override void RestEffect(IEntityVisualComponent visualComponent)
        {
            visualComponent.EntityTransform.Move(_originalPosition,_transformEffectActionSO);
            visualComponent.EntityTransform.Scale(_originalScale,_transformEffectActionSO);
            visualComponent.EntityTransform.Rotate(_originalRotation,_transformEffectActionSO);
        }
    }
    
    [Serializable]
    public class Transition3D 
    {
        [SerializeField] private float _timeToTransition = 1.0f;

        [SerializeField] private AnimationCurve _animationCurveX;
            
        [SerializeField] private AnimationCurve _animationCurveY;
            
        [SerializeField] private AnimationCurve _animationCurveZ;
        
        public float TimeToTransition
        {
            get { return _timeToTransition;}
#if UNITY_EDITOR
            set { _timeToTransition = value; }
        
#endif
        }
        public AnimationCurve AnimationCurveX
        {
            get { return _animationCurveX;}
#if UNITY_EDITOR
            set { _animationCurveX = value; }

#endif
        }
        public AnimationCurve AnimationCurveY
        {
            get { return _animationCurveY;}
#if UNITY_EDITOR
            set { _animationCurveY = value; }

#endif
        }
        public AnimationCurve AnimationCurveZ
        {
            get { return _animationCurveZ;}
#if UNITY_EDITOR
            set { _animationCurveZ = value; }

#endif
        }
    } 

}