using SerializeData.VisualSystemSerializeData;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.VisualSystem.EffectSequence.EffectActionTypeSO;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence.EffectType.Sound
{
    public class SoundEffectAction : BaseEffectAction
    {
        private AudioClip[] _audioClips;
        
        private bool _randomPitch = false;
        private bool _randomVolume = false;
        
        private Vector2 _volume;
        private Vector2 _pitchRange;

        private AudioClip _selectedAudioClip;
        
        public override float Duration => _selectedAudioClip.length;
        
        public SoundEffectAction(EffectActionContainerData actionContainerData) : base(actionContainerData)
        {
            var config = GetConfig<SoundEffectActionSO>(actionContainerData.EffectActionSo);

            _audioClips = config.AudioClips;
            _volume = config.VolumeRange;
            _randomPitch  = config.RandomPitch;
            _pitchRange = config.PitchRange;
            _randomVolume = config.RandomVolume;
        }
        
        public override void ProcessEffect(IEntityVisualComponent visualComponent)
        {
            float pitch = 1;
            float volume = 1;
            
            if (_randomPitch)
                pitch = Random.Range(_pitchRange.x, _pitchRange.y);
            if (_randomVolume)
                volume  = Random.Range(_volume.x, _volume.y);

            _selectedAudioClip = _audioClips[Random.Range(0, _audioClips.Length)];

            visualComponent.SoundHandler.PlayAudioClip(_selectedAudioClip,pitch ,volume);

            base.ProcessEffect(visualComponent);
        }
    }
}