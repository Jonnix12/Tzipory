using System.Collections.Generic;
using Helpers.Consts;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.Helpers;
using UnityEngine;

namespace Tzipory.VisualSystem.EffectSequence
{
    public class EffectSequenceHandler
    {
        private readonly Dictionary<int, EffectSequence> _sequencesDictionary;

        private List<EffectSequence> _activeSequences;

        private IEntityVisualComponent _entityVisualComponent;

        public EffectSequenceHandler(IEntityVisualComponent visualComponent,IEnumerable<EffectSequence> sequences)
        {
            _sequencesDictionary = new();
            _activeSequences = new();
            
            _entityVisualComponent = visualComponent;

            foreach (var sequence in sequences)
            {
                sequence.Init(visualComponent);
                _sequencesDictionary.Add(sequence.ID, sequence);
            }
        }

        private void PlaySequence(EffectSequence effectSequence)
        {
            for (var i = 0; i < _activeSequences.Count; i++)
            {
                if (_activeSequences[i].ID != effectSequence.ID) continue;

                if (!effectSequence.IsInterruptable) return;

                _activeSequences[i].StopSequence();
                _activeSequences.RemoveAt(i);
                break;
            }
            
            effectSequence.StartEffectSequence();
            effectSequence.OnEffectSequenceComplete += RemoveActiveEffectSequence;
            _activeSequences.Add(effectSequence);
        }

        public void PlaySequenceById(int id)
        {
            if (!_sequencesDictionary.TryGetValue(id, out var effectSequence))
            {
                Debug.LogWarning($"Sequence with id {id} not found");
                return;
            }
            
            PlaySequence(effectSequence);
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
            effectSequence.Init(_entityVisualComponent);
            PlaySequence(effectSequence);
        }
    }
}