using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSerializeData
    {
        [SerializeField] private List<WaveSpawnerSerializeData> _waveSpawners;
        [SerializeField] private float _delayBetweenGroups;

        public List<WaveSpawnerSerializeData> WaveSpawners => _waveSpawners;

        public float DelayBetweenGroups => _delayBetweenGroups;
        
        public void SetWaveSpawners(IEnumerable<WaveSpawner> waveSpawners)
        {
            if (_waveSpawners == null)
                _waveSpawners = new List<WaveSpawnerSerializeData>();

            foreach (var waveSpawner in waveSpawners)
            {
                _waveSpawners.Add(new WaveSpawnerSerializeData(waveSpawner));
            }
        }

        public void AddWaveSpawner(WaveSpawner waveSpawner)
        {
            _waveSpawners.Add(new WaveSpawnerSerializeData(waveSpawner));
        }
        
        public void RemoveWaveSpawner(WaveSpawner waveSpawner)
        {
            
        }

        [Button("Delete wave")]
        public void DeleteWave()
        {
            
        }
    }
}