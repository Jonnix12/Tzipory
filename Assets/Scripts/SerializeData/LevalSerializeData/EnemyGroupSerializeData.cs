using Enemes;
using Sirenix.OdinInspector;
using Tzipory.Tools.Enums;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class EnemyGroupSerializeData
    {
        [SerializeField,AssetsOnly,Required] private Enemy _enemyPrefab;
        [SerializeField] private int _totalSpawnAmount;
        [SerializeField] private int _spawnAmountPreInterval;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private ActionStartType _startType;

        public Enemy EnemyPrefab => _enemyPrefab;

        public int TotalSpawnAmount => _totalSpawnAmount;
        
        public float SpawnInterval => _spawnInterval;

        public int SpawnAmountPreInterval => _spawnAmountPreInterval;

        public ActionStartType StartType => _startType;
    }
}