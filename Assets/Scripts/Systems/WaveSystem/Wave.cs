using System;
using System.Collections.Generic;
using System.Linq;
using Tzipory.SerializeData.LevalSerializeData;

namespace Tzipory.WaveSystem
{
    public class Wave : WaveComponent<WaveSerializeData>
    {
        private readonly WaveSpawner[] _waveSpawners;
        
        public override WaveSerializeData Data { get; }

        public bool IsStarted { get; private set; }
        
        public override float CompletionPercentage => throw new NotImplementedException();
        public override bool IsDone => _waveSpawners.All(waveSpawner => waveSpawner.IsDone);

        public int NumberOfEnemiesInWave
        {
            get
            {
                int allEnemiesInWave = 0;

                foreach (var waveSpawner in _waveSpawners)
                    allEnemiesInWave += waveSpawner.TotalNumberOfEnemiesPreWave;
                
                return allEnemiesInWave;
            }
        }

        public Wave(IEnumerable<WaveSpawner> waveSpawners,WaveSerializeData waveSerializeData)
        {
            Data = waveSerializeData;
            _waveSpawners  = waveSpawners.ToArray();
        }

        public void StartWave()
        {
            for (int i = 0; i < _waveSpawners.Length; i++) 
                _waveSpawners[i].Init(Data.WaveSpawnerSerializeDatas[i]);
            
            IsStarted = true;
        }
        
        public void EndWave()
        {
        }
    }
}