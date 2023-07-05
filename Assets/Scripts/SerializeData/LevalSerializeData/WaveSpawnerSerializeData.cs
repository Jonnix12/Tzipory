using System;
using System.Linq;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSpawnerSerializeData
    {
        private Color _waveSpawnerColor;
        [SerializeField] private float _delayBetweenEnemyGroup;
        [SerializeField] private EnemyGroupSerializeData[] _enemyGroups;
        public float TotalSpawnTime
        {
            get
            {
                float  totalTime = 0;
                
                if (_enemyGroups == null)
                    return 0;

                foreach (var groupSerializeData in _enemyGroups)
                {
                    if (groupSerializeData == null)
                        continue;
                    totalTime += groupSerializeData.TotalGroupSpawnTime;
                }    
                
                return  totalTime;
            }
        }

        public EnemyGroupSerializeData[] EnemyGroups => _enemyGroups;

        public float DelayBetweenEnemyGroup => _delayBetweenEnemyGroup;

        public WaveSpawnerSerializeData(WaveSpawner waveSpawner)
        {
            _waveSpawnerColor = waveSpawner.WaveSpawnerColor;
        }
        
        public void OnValidate()
        {
            for (int i = 0; i < _enemyGroups.Length; i++)
            {
                if (i == 0)
                {
                    _enemyGroups[i].OnValidate(0);
                    continue;
                }
                _enemyGroups[i].OnValidate(_enemyGroups[i - 1].EndTime);
            }
        }

        private Color GetColor()
            => _waveSpawnerColor;
    }
}