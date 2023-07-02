using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class LevelSerializeData
    {
        [SerializeField] private List<WaveSerializeData> _waves;
        [SerializeField] private float _levelStartDelay = 0.2f;
        [SerializeField] private float _delayBetweenWaves;
        [SerializeField] private int _maxSimultaniousSpawners;

        public List<WaveSerializeData> Waves => _waves;

        public float LevelStartDelay => _levelStartDelay;

        public float DelayBetweenWaves => _delayBetweenWaves;

        public int MaxSimultaniousSpawners => _maxSimultaniousSpawners;
        
        [Button("Add new wave")]
        public void AddWave()
        {
            var waveData = new WaveSerializeData();
            waveData.SetWaveSpawners(LevelManager.WaveSpawners);
            _waves.Add(waveData);
        }

        public void AddWaveSpawner(WaveSpawner waveSpawner)
        {
            foreach (var waveSerializeData in _waves)
            {
                waveSerializeData.AddWaveSpawner(waveSpawner);
            }
        }
    }
}
