using System;
using Tzipory.SerializeData.LevalSerializeData;

namespace Systems.Wave_System
{
    public class Wave
    {
        public event Action OnWaveStart;
        public event Action OnWaveEnd;
        
        
        private WaveSpawner[] _waveSpawners;
        
        public Wave(WaveSpawner[] waveSpawners,WaveSerializeData waveSerializeData)
        {
            _waveSpawners  = waveSpawners;
            
            StartWave();
        }

        private void StartWave()
        {
            OnWaveStart?.Invoke();
        }
        
        public void EndWave()
        {
            OnWaveEnd?.Invoke();
        }
    }
}