using System;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.SerializeData.LevalSerializeData;
using UnityEngine;

namespace Tzipory.WaveSystem
{
    public class EnemyGroup : WaveComponent<EnemyGroupSerializeData>
    {
        private GameObject _enemyPrefab;
        
        private int _spawnAmountPreInterval;

        private float _spawnInterval;
        public int TotalSpawnAmount { get; private set; }

        public override EnemyGroupSerializeData Data { get; }
        public override float CompletionPercentage => throw new NotImplementedException();
        public override bool IsDone => TotalSpawnAmount <= 0;
        
        public EnemyGroup(EnemyGroupSerializeData enemyGroupSerializeData)
        {
            Data = enemyGroupSerializeData;
            _enemyPrefab = enemyGroupSerializeData.EnemyPrefab;
            TotalSpawnAmount = enemyGroupSerializeData.TotalSpawnAmount;
            _spawnInterval = 0;
            _spawnAmountPreInterval = enemyGroupSerializeData.SpawnAmountPreInterval;
        }

        public bool TryGetEnemy(out GameObject enemyPrefab)
        {
            if (TotalSpawnAmount <= 0)
            {
                enemyPrefab = null;
                return false;
            }

            if (_spawnInterval <= 0)
            {
                while (_spawnAmountPreInterval > 0)
                {
                    _spawnAmountPreInterval--; 
                    TotalSpawnAmount--;
                    enemyPrefab = _enemyPrefab;
                    return true;
                }

                _spawnAmountPreInterval = Data.SpawnAmountPreInterval;
                _spawnInterval = Data.SpawnInterval;
            }
            else
                _spawnInterval -= GAME_TIME.GameDeltaTime;

            enemyPrefab = null;
            return false;
        }

    }
}


