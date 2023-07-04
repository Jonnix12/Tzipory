using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "ScriptableObjects/New level config", order = 0)]
    public class LevelSerializeData : ScriptableObject
    {
        [SerializeField] private Level _level;
        [SerializeField] private List<WaveSerializeData> _waves;
        [SerializeField] private float _levelStartDelay = 0.2f;
        [SerializeField] private float _delayBetweenWaves;
        [SerializeField] private int _maxSimultaniousSpawners;

        public Level Level => _level;

        public List<WaveSerializeData> Waves => _waves;

        public float LevelStartDelay => _levelStartDelay;

        public float DelayBetweenWaves => _delayBetweenWaves;

        public int MaxSimultaniousSpawners => _maxSimultaniousSpawners;

        [Button("Add new wave")]
        public void AddWave()
        {
            var waveData = new WaveSerializeData();
            waveData.SetWaveSpawners(_level.WaveSpawners);
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
