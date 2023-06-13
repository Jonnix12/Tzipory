using System;
using DG.Tweening;
using Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO;
using Tzipory.VisualSystem.EffectSequence.EffectType;
using UnityEngine;

public static class TransitionManager
{
    #region PublicFunction
    
    #region Transitions
    
    public static Sequence Transition(this Transform transform ,TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        Vector3 destination = Vector3.zero;
        
        switch (transitionPackSo.PositionMoveTypeEnum)
        {
            case TransformEffectActionSO.PositionMoveType.Local:
                destination = transform.localPosition + transitionPackSo.MoveOffSet;
                break;
            case TransformEffectActionSO.PositionMoveType.Word:
                destination = transform.position + transitionPackSo.MoveOffSet;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return transform.Transition(destination, transitionPackSo, onComplete);
    }
    
    public static Sequence Transition(this Transform transform , Transform destination, TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        Sequence sequence = DOTween.Sequence();

        if (transitionPackSo.HaveMovement)
            sequence.Join(transform.DoMove(destination.position, transitionPackSo));
        
        sequence.Join(transform.Scale(destination.localScale, transitionPackSo));
        
        if (transitionPackSo.HaveRotation)
            sequence.Join(transform.Rotate(destination.rotation.eulerAngles, transitionPackSo.Rotation));

        if (onComplete != null)
            sequence.OnComplete(onComplete.Invoke);

        return sequence;
    }

    public static Sequence Transition(this Transform transform ,Vector3 destination, TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        Sequence sequence = DOTween.Sequence();

        if (transitionPackSo.HaveMovement)
            sequence.Join(transform.DoMove(destination, transitionPackSo));

        if (transitionPackSo.HaveScale)
        {
            switch (transitionPackSo.ScaleType)
            {
                case TransformEffectActionSO.ScaleTypeEnum.ByFloat:
                    sequence.Join(transform.Scale(transitionPackSo.ScaleMultiplier, transitionPackSo));
                    break;
                case TransformEffectActionSO.ScaleTypeEnum.ByVector:
                    sequence.Join(transform.Scale(transitionPackSo.ScaleVector, transitionPackSo));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (transitionPackSo.HaveRotation)
            sequence.Join(transform.Rotate(transitionPackSo.Rotate, transitionPackSo.Rotation));

        if (onComplete != null)
            sequence.OnComplete(onComplete.Invoke);

        return sequence;
    }

    #endregion

    #region SetPositionAndScale

    public static Sequence SetPosition(this Transform transform ,Transform destination, Action onComplete = null)
    {
        return transform.SetPosition(destination.position,onComplete);
    }

    public static Sequence SetPosition(this Transform transform ,Vector3 destination, Action onComplete = null)
    {
        return transform.DoMove(destination,null,onComplete);
    }

    public static Tween SetScale(this Transform transform ,float scale, Action onComplete = null)
    {
        return transform.Scale(scale, 0, null, onComplete);
    }
    
    // public static Tween SetScale(this Transform transform ,Vector3 scale, Action onComplete = null)
    // {
    //     return transform.Scale(scale,null,onComplete);
    // }

    #endregion

    #region Move
    
    public static Sequence Move(this Transform transform, TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        Vector3 destination = Vector3.zero;
        
        switch (transitionPackSo.PositionMoveTypeEnum)
        {
            case TransformEffectActionSO.PositionMoveType.Local:
                destination = transform.localPosition + transitionPackSo.MoveOffSet;
                break;
            case TransformEffectActionSO.PositionMoveType.Word:
                destination = transform.position + transitionPackSo.MoveOffSet;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return transform.Move(destination, transitionPackSo, onComplete);
    }
    
    public static Sequence Move(this Transform transform,Transform destination, TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        return transform.Move(destination.position, transitionPackSo, onComplete);
    }

    public static Sequence Move(this Transform transform,Vector3 destination, TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        return transform.DoMove(destination, transitionPackSo, onComplete);
    }

    #endregion

    #region Scale

    public static Tween Scale(this Transform transform,TransformEffectActionSO transitionPackSo, Action onComplete = null)//transitionPackSo scale input
    {
        switch (transitionPackSo.ScaleType)
        {
            case TransformEffectActionSO.ScaleTypeEnum.ByFloat:
                return transform.Scale(transitionPackSo.ScaleMultiplier, transitionPackSo, onComplete);
            case TransformEffectActionSO.ScaleTypeEnum.ByVector:
                return transform.Scale(transitionPackSo.ScaleVector, transitionPackSo, onComplete);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static Tween Scale(this Transform transform,float multiply, TransformEffectActionSO transitionPackSo, Action onComplete = null)//multiply scale input
    {
        return transform.Scale(multiply, transitionPackSo.Scale, onComplete);
    }

    public static Tween Scale(this Transform transform,Vector3 scaleVector, TransformEffectActionSO transitionPackSo, Action onComplete = null)//scaleVector scale input
    {
        return transform.Scale(scaleVector, transitionPackSo.Scale, onComplete);
    }

    #endregion

    #region Rotation

    public static Tween Rotate(this Transform transform,TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        return transform.Rotate(transitionPackSo.Rotate, transitionPackSo, onComplete);
    }

    public static Tween Rotate(this Transform transform,Vector3 destination, TransformEffectActionSO transitionPackSo, Action onComplete = null)
    {
        return transform.Rotate(destination, transitionPackSo.Rotation, onComplete);
    }

    #endregion

    #endregion

    #region PrivateFunction

    #region Move
    
    private static Sequence DoMove(this Transform transform, Vector3 destination, TransformEffectActionSO transitionPackSo = null,
        Action onComplete = null)
    {
        Transition3D param = null;
        
        if (transitionPackSo != null)
        {
            param = transitionPackSo.Movement;
        }

        switch (transitionPackSo != null ? transitionPackSo.PositionMoveTypeEnum : (TransformEffectActionSO.PositionMoveType?)null)
        {
            case TransformEffectActionSO.PositionMoveType.Local:
                return transform.MoveLocalPosition(destination, param?.TimeToTransition ?? 0, param?.AnimationCurveX, param?.AnimationCurveY,
                    param?.AnimationCurveZ,
                    onComplete);
            case TransformEffectActionSO.PositionMoveType.Word:
                return transform.MoveWordPosition(destination, param?.TimeToTransition ?? 0, param?.AnimationCurveX, param?.AnimationCurveY,
                    param?.AnimationCurveZ,
                    onComplete);
            case null:
                return transform.MoveWordPosition(destination,0, null, null, null, onComplete);
            default:
                throw  new ArgumentOutOfRangeException();
                break;
        }
    }

    private static Sequence MoveWordPosition(this Transform transform, Vector3 destination, float timeToTransition,
        AnimationCurve animationCurveX = null,
        AnimationCurve animationCurveY = null, AnimationCurve animationCurveZ = null, Action onComplete = null)
    {
        if (timeToTransition == 0)
        {
            transform.position = destination;

            onComplete?.Invoke();
            return null;
        }

        var sequence = DOTween.Sequence();

        Tween tweenX;
        Tween tweenY;
        Tween tweenZ = null;

        sequence.Join(tweenX = transform.DOMoveX(destination.x, timeToTransition));
        sequence.Join(tweenY = transform.DOMoveY(destination.y, timeToTransition));

        if (destination.z != 0) sequence.Join(tweenZ = transform.DOMoveZ(destination.z, timeToTransition));
        
        if (animationCurveX != null) tweenX.SetEase(animationCurveX);

        if (animationCurveY != null) tweenY.SetEase(animationCurveY);

        if (tweenZ != null)
            if (animationCurveZ != null)
                tweenZ.SetEase(animationCurveZ);


        if (onComplete != null) sequence.OnComplete(() => onComplete?.Invoke());

        return sequence;
    }
    
    private static Sequence MoveLocalPosition(this Transform transform, Vector3 destination, float timeToTransition,
        AnimationCurve animationCurveX = null,
        AnimationCurve animationCurveY = null, AnimationCurve animationCurveZ = null, Action onComplete = null)
    {
        if (timeToTransition == 0)
        {
            transform.localPosition = destination;

            onComplete?.Invoke();
            return null;
        }

        var sequence = DOTween.Sequence();

        Tween tweenX;
        Tween tweenY;
        Tween tweenZ = null;

        sequence.Join(tweenX = transform.DOLocalMoveX(destination.x, timeToTransition));
        sequence.Join(tweenY = transform.DOLocalMoveY(destination.y, timeToTransition));

        if (destination.z != 0) sequence.Join(tweenZ = transform.DOLocalMoveZ(destination.z, timeToTransition));


        if (animationCurveX != null) tweenX.SetEase(animationCurveX);

        if (animationCurveY != null) tweenY.SetEase(animationCurveY);

        if (tweenZ != null)
            if (animationCurveZ != null)
                tweenZ.SetEase(animationCurveZ);


        if (onComplete != null) sequence.OnComplete(() => onComplete?.Invoke());

        return sequence;
    }
    
    #endregion

    #region Scale

    private static Tween Scale(this Transform transform, float scaleMultiplier, Transition3D param = null,
        Action onComplete = null)
    {
        return transform.Scale(scaleMultiplier, param?.TimeToTransition ?? 0, param?.AnimationCurveX,
            onComplete);
    }

    private static Sequence Scale(this Transform transform, Vector3 scaleVector, Transition3D param,
        Action onComplete = null)
    {
        return transform.Scale(scaleVector, param?.TimeToTransition ?? 0, param?.AnimationCurveX, param?.AnimationCurveY,
            param?.AnimationCurveZ,
            onComplete);
    }

    private static Sequence Scale(this Transform transform, Vector3 scaleVector, float timeToTransition,
        AnimationCurve animationCurveX = null, AnimationCurve animationCurveY = null,
        AnimationCurve animationCurveZ = null, Action onComplete = null)//vector scale
    {
        if (timeToTransition == 0)
        {
            transform.localScale = scaleVector;

            onComplete?.Invoke();
            return null;
        }

        var sequence = DOTween.Sequence();

        Tween tweenX;
        Tween tweenY;
        Tween tweenZ = null;


        sequence.Join(tweenX = transform.DOScaleX(scaleVector.x, timeToTransition));
        sequence.Join(tweenY = transform.DOScaleY(scaleVector.y, timeToTransition));

        if (scaleVector.z != 0) sequence.Join(tweenZ = transform.DOScaleZ(scaleVector.z, timeToTransition));

        if (animationCurveX != null) tweenX.SetEase(animationCurveX);

        if (animationCurveY != null) tweenY.SetEase(animationCurveY);

        if (tweenZ != null)
            if (animationCurveZ != null)
                tweenZ.SetEase(animationCurveZ);


        if (onComplete != null) tweenX.OnComplete(() => onComplete?.Invoke());

        return sequence;
    }

    private static Tween Scale(this Transform transform, float scaleMultiplier, float timeToTransition,
        AnimationCurve animationCurveX = null, Action onComplete = null)//scaleMultiplier scale
    {
        if (timeToTransition == 0)
        {
            transform.localScale *= scaleMultiplier;
    
            onComplete?.Invoke();
            return null;
        }
    
        Tween tweenX = transform.DOScale(Vector3.one * scaleMultiplier, timeToTransition);
    
        if (animationCurveX != null) tweenX.SetEase(animationCurveX);
    
    
        if (onComplete != null) tweenX.OnComplete(() => onComplete?.Invoke());
    
        return tweenX;
    }

    #endregion

    #region Rotate

    private static Tween Rotate(this Transform transform, Vector3 destination, Transition3D param,
        Action onComplete = null)
    {
        return transform.Rotate(destination, param?.TimeToTransition ?? 0, param?.AnimationCurveX, param?.AnimationCurveY,
            param?.AnimationCurveZ, onComplete);
        ;
    }

    private static Tween Rotate(this Transform transform, Vector3 destination, float timeToTransition,
        AnimationCurve animationCurveX = null, AnimationCurve animationCurveY = null,
        AnimationCurve animationCurveZ = null, Action onComplete = null)
    {
        if (timeToTransition == 0)
        {
            transform.rotation = Quaternion.Euler(destination);

            onComplete?.Invoke();
            return null;
        }


        Tween tween = transform.DORotate(destination, timeToTransition);

        if (animationCurveZ != null) tween.SetEase(animationCurveZ);

        if (onComplete != null) tween.OnComplete(() => onComplete?.Invoke());

        return tween;
    }

    #endregion

    #endregion   

    #region TweenManagnent

    private static void Kill(ref Tween tween)
    {
        if (tween != null) tween.Kill();
    }

    private static void Kill(ref Sequence sequence)
    {
        if (sequence != null) sequence.Kill();
    }

    #endregion
}

#region HelperClass

public static class RectTransformHelper
{
    public static Vector2 GetLocalPosition(this RectTransform rectTransform)
    {
        return rectTransform.transform.localPosition;
    }

    public static Vector2 GetWorldPosition(this RectTransform rectTransform)
    {
        return rectTransform.transform.TransformPoint(rectTransform.rect.center);
    }
}

#endregion