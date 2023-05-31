using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;

namespace Tzipory.VisualSystem.EffectSequence
{
    public class EffectSequenceHandler
    {
        private readonly Dictionary<int, EffectSequence> _sequencesDictionary;

        private List<EffectSequence> _activeSequences;

        public EffectSequenceHandler(IEntityVisualComponent visualComponent,IEnumerable<EffectSequence> sequences)
        {
            _sequencesDictionary = new();
            _activeSequences = new();

            foreach (var sequence in sequences)
            {
                sequence.Init(visualComponent);
                _sequencesDictionary.Add(sequence.ID, sequence);
            }
        }

        public void PlaySequenceById(int id)
        {
            for (int i = 0; i < _activeSequences.Count; i++)
            {
                if (_activeSequences[i].ID == id)
                    return;
            }

            if(_sequencesDictionary.TryGetValue(id, out var effectSequence)){
                effectSequence.PlaySequence();
                effectSequence.OnEffectSequenceComplete += RemoveActiveEffectSequence;
                _activeSequences.Add(effectSequence);
            }
        }

        private void RemoveActiveEffectSequence(int id)
        {
            for (int i = 0; i < _activeSequences.Count; i++)
            {
                if (_activeSequences[i].ID == id)
                {
                    _activeSequences[i].OnEffectSequenceComplete -= RemoveEffectSequence;
                    _activeSequences.RemoveAt(i);
                    return;
                }
            }
        }

        public void RemoveEffectSequence(int effectSequenceId)
        {
            if (_sequencesDictionary.TryGetValue(effectSequenceId, out var effectSequence))
            {
                effectSequence.StopSequence();
                _sequencesDictionary.Remove(effectSequenceId);
            }
        }

        public void ActiveEffectSequence(EffectSequence effectSequence)
        {
            effectSequence.PlaySequence();
            _activeSequences.Add(effectSequence);
        }
    }
}