using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence.EffectType
{
    public class ColorEffectAction : BaseEffectAction
    {
        private Color _color;
        private Color _originalColor;
        private float _alpha;
        private float _duration;

        public override float Duration => _duration;
        
        public ColorEffectAction(EffectActionContainer actionContainer) : base(actionContainer)
        {
            var config = GetConfig<ColorEffectActionSO>(actionContainer.EffectActionSo);
            
            _color = config.Color;
            _alpha = config.Alpha;
            _duration = config.Duration;
        }


        public override void ProcessEffect(IEntityVisualComponent visualComponent)
        {
            var newColor = new Color(_color.r, _color.g, _color.b, _alpha);
            _originalColor = visualComponent.SpriteRenderer.color;
            
            visualComponent.SpriteRenderer.color = newColor;
            
            base.ProcessEffect(visualComponent);
        }

        public override void UndoEffect(IEntityVisualComponent visualComponent)
        {
            visualComponent.SpriteRenderer.color = _originalColor;
            
            visualComponent.GameEntity.EntityTimer.StartNewTimer(_endDelay,OnCompleteEffectAction);
        }
    }
}