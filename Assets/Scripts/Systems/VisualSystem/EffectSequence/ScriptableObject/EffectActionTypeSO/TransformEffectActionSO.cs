using Tzipory.VisualSystem.EffectSequence.EffectType;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO
{
    [CreateAssetMenu(fileName = "NewTransformEffectAction", menuName = "ScriptableObjects/VisualSystem/EffectAction/New transform effect action")]
    public class TransformEffectActionSO : BaseEffectActionSO
    {
        #region Position

        private bool _haveMovement = true; 
        private Transition3D _movement;
        private PositionMoveType _positionMoveTypeEnum;
        private Vector3 _moveOffSet;

        public bool HaveMovement
        {
            get => _haveMovement;
#if UNITY_EDITOR
            set => _haveMovement = value;
#endif
        }

        public Transition3D Movement
        {
            get => _movement;
#if UNITY_EDITOR
            set => _movement = value;
#endif
        }

        public PositionMoveType PositionMoveTypeEnum
        {
            get => _positionMoveTypeEnum;
#if UNITY_EDITOR
            set => _positionMoveTypeEnum = value;
#endif
        }

        public Vector3 MoveOffSet
        {
            get => _moveOffSet;
#if UNITY_EDITOR
            set => _moveOffSet = value;
#endif
        }

        #endregion
        
        #region Scale
        
        private ScaleTypeEnum _scaleType;
        private bool _haveScale = false; 
        private float _scaleMultiplier;
        private Vector3 _scaleVector;
        private Transition3D _scale;

        public ScaleTypeEnum ScaleType
        {
            get => _scaleType;
#if UNITY_EDITOR
            set => _scaleType = value;
#endif
        }

        public bool HaveScale
        {
            get => _haveScale;
#if UNITY_EDITOR
            set => _haveScale = value;
#endif
        }

        public float ScaleMultiplier
        {
            get => _scaleMultiplier;
#if UNITY_EDITOR
            set => _scaleMultiplier = value;
#endif
        }

        public Vector3 ScaleVector
        {
            get => _scaleVector;
#if UNITY_EDITOR
            set => _scaleVector = value;
#endif
        }

        public Transition3D Scale
        {
            get => _scale;
#if UNITY_EDITOR
            set => _scale = value;
#endif
        }

        #endregion

        #region Rotation

        private bool _haveRotation = false; 
        private Vector3 _rotate; 
        private Transition3D _rotation;

        public bool HaveRotation
        {
            get => _haveRotation;
#if UNITY_EDITOR
            set => _haveRotation = value;
#endif
        }

        public Vector3 Rotate
        {
            get => _rotate;
#if UNITY_EDITOR
            set => _rotate = value;
#endif
        }

        public Transition3D Rotation
        {
            get => _rotation;
#if UNITY_EDITOR
            set => _rotation = value;
#endif
        }

        #endregion
        
        public override EffectActionType ActionType => EffectActionType.Transform;
        
        public enum PositionMoveType
        {
            Local,
            Word
        }
        
        public enum ScaleTypeEnum
        {
            ByFloat,
            ByVector
        };
    }
}