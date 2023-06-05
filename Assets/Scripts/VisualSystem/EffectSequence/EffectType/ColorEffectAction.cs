using Tzipory.BaseSystem.TimeSystem;
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
        
        public ColorEffectAction(BaseEffectActionSO effectActionSO,IEntityVisualComponent visualComponent) : base(effectActionSO,visualComponent)
        {
            var config = GetConfig<ColorEffectActionSO>(effectActionSO);
            
            _color = config.Color;
            _alpha = config.Alpha;
            _duration = config.Duration;
        }

        protected override void ProcessEffect()
        {
            var newColor = new Color(_color.r, _color.g, _color.b, _alpha);
            _originalColor = VisualComponent.SpriteRenderer.color;
            
            VisualComponent.SpriteRenderer.color = newColor;
            
            GAME_TIME.TimerHandler.StartNewTimer(_duration,RestEffect);
        }

        protected override void RestEffect()
        {
            VisualComponent.SpriteRenderer.color = _originalColor;
            GAME_TIME.TimerHandler.StartNewTimer(_endDelay,OnCompleteEffectAction);
        }
    }
}