using UnityEngine;

namespace Tzipory.WaveSystem
{
    public struct EnemyGroup 
    {
        public GameObject EnemyPrefab { get; }

        public int SpawnAmount { get; }

        public float SpawnInterval { get; }

        public EnemyGroup(GameObject enemyPrefab, int spawnAmount, float spawnInterval)
        {
            EnemyPrefab = enemyPrefab;
            SpawnAmount = spawnAmount;
            SpawnInterval = spawnInterval;
        }
    }
}


