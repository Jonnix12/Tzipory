using Tzipory.VisualSystem.EffectSequence;
using UnityEngine;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityVisualComponent : IEntityComponent
    {
        public EffectSequenceHandler EffectSequenceHandler { get; }
        public SpriteRenderer SpriteRenderer { get; }
        public Transform ParticleEffectPosition { get; }
        public Transform VisualQueueEffectPosition { get; }
    }
}