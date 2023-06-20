using System.Collections.Generic;
using Helpers.Consts;
using SerializeData.VisualSystemSerializeData;
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

        public EffectSequenceHandler(IEntityVisualComponent visualComponent,IEnumerable<EffectSequenceData> sequencesDatas)
        {
            _sequencesDictionary = new();
            _activeSequences = new();
            
            _entityVisualComponent = visualComponent;

            foreach (var sequenceData in sequencesDatas)
                _sequencesDictionary.Add(sequenceData.ID, new EffectSequence(visualComponent,sequenceData));
        }

        private void PlaySequence(EffectSequence effectSequence)
        {
            effectSequence.StartEffectSequence();
            effectSequence.OnEffectSequenceComplete += RemoveActiveEffectSequence;
            _activeSequences.Add(effectSequence);
        }

        private void InterrupterEffectSequence(int interrupterSequenceIndex)
        {
            _activeSequences[interrupterSequenceIndex].StopSequence();
            _activeSequences[interrupterSequenceIndex].OnEffectSequenceComplete -= RemoveEffectSequence;
            _activeSequences.RemoveAt(interrupterSequenceIndex);
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

        private bool CanPlaySequence(EffectSequenceData sequenceData,out int interrupterSequenceIndex)
        {
            for (var i = 0; i < _activeSequences.Count; i++)
            {
                if (_activeSequences[i].ID != sequenceData.ID) 
                    continue;

                if (_activeSequences[i].IsInterruptable)
                {
                    interrupterSequenceIndex = i;
                    return true;
                }

                interrupterSequenceIndex = -1;
                return false;
            }

            interrupterSequenceIndex = -1;
            return true;
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

        public void RemoveEffectSequence(int effectSequenceId)
        {
            if (_sequencesDictionary.TryGetValue(effectSequenceId, out var effectSequence))
            {
                effectSequence.StopSequence();
                _sequencesDictionary.Remove(effectSequenceId);
            }
        }

        public void ActiveEffectSequence(EffectSequenceData sequenceData)
        {
            if (sequenceData.EffectActionContainers.Count == 0)
                return;

            if (CanPlaySequence(sequenceData,out var interrupterSequenceIndex))
            {
                if (interrupterSequenceIndex != -1)
                    InterrupterEffectSequence(interrupterSequenceIndex);
                
                var sequence = new EffectSequence(_entityVisualComponent,sequenceData);
                PlaySequence(sequence);
            }
        }
    }
}