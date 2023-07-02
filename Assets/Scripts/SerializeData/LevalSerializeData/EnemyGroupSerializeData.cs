using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class EnemyGroupSerializeData
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private int _spawnAmount;
        [SerializeField] private float _spawnInterval;

        public GameObject EnemyPrefab => _enemyPrefab;

        public int SpawnAmount => _spawnAmount;
        
        public float SpawnInterval => _spawnInterval;
    }
}