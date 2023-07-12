using System.Collections.Generic;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.SerializeData.LevalSerializeData;
using Tzipory.WaveSystem;
using UnityEngine;

namespace Tzipory.Leval
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelSerializeData _levelSerializeData;
        [SerializeField] private Transform _levelPerant;
        private List<Wave> _waves;
    
        private IEnumerable<WaveSpawner> _waveSpawners;

        private float _levelStartDelay;
        private float _delayBetweenWaves;
        
        private int _currentWaveIndex;

        public int WaveNumber => _currentWaveIndex + 1;

        public int TotalNumberOfWaves => _waves.Count;
        
        private Wave CurrentWave => _waves[_currentWaveIndex];
    
        private void Awake()
        {
            _currentWaveIndex = 0;
            _levelStartDelay = _levelSerializeData.LevelStartDelay;
            _delayBetweenWaves = _levelSerializeData.DelayBetweenWaves;

            Instantiate(_levelSerializeData.Level,_levelPerant);
            _waveSpawners = FindObjectsOfType<WaveSpawner>();//temp!
            
            _waves = new List<Wave>();
        
            foreach (var waveSerialize in _levelSerializeData.Waves)
                _waves.Add(new Wave(_waveSpawners,waveSerialize));
        }

        private void Update()
        {
            if (_levelStartDelay > 0)
            {
                _levelStartDelay -= GAME_TIME.GameDeltaTime;
                return;
            }

            if (!CurrentWave.IsStarted)
                CurrentWave.StartWave();
            
            if (CurrentWave.IsDone)
            {
                if (_delayBetweenWaves > 0)
                    return;

                _delayBetweenWaves = _levelSerializeData.DelayBetweenWaves;
                CurrentWave.EndWave();
                _currentWaveIndex++;
            }
        }
    }
}
