using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class EnemyGroupSerializeData
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _totalSpawnAmount;
        [SerializeField] private int _spawnAmountPreInterval;
        [SerializeField] private float _spawnInterval;

        public GameObject EnemyPrefab => _enemyPrefab;

        public int TotalSpawnAmount => _totalSpawnAmount;
        
        public float SpawnInterval => _spawnInterval;

        public int SpawnAmountPreInterval => _spawnAmountPreInterval;
    }
}