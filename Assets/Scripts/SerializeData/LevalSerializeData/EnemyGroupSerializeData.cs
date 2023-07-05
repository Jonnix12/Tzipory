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
        [SerializeField] private  float _groupStartDelay;
        [SerializeField] private int _totalSpawnAmount;
        [SerializeField] private int _spawnAmountPreInterval;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private ActionStartType _startType;

        [Header("Group timing")] 
        [SerializeField, ReadOnly] private float _startTime;

        [ShowInInspector, ReadOnly]
        public float TotalGroupSpawnTime
        {
            get
            {
                if (_totalSpawnAmount == 0 || _spawnAmountPreInterval == 0)
                    return  _groupStartDelay;

                float  totalTime;
                
                var numberOfPules = _totalSpawnAmount / _spawnAmountPreInterval;
                
                if (numberOfPules % 1 != 0)
                    totalTime = Mathf.Floor(numberOfPules) * _spawnInterval + _spawnInterval;
                else
                    totalTime = numberOfPules * _spawnInterval;

                totalTime += _groupStartDelay;
                
                return totalTime;
            }
        }
        [ShowInInspector, ReadOnly]
        public float EndTime => _startTime + TotalGroupSpawnTime;

        public Enemy EnemyPrefab => _enemyPrefab;

        public int TotalSpawnAmount => _totalSpawnAmount;
        
        public float SpawnInterval => _spawnInterval;

        public int SpawnAmountPreInterval => _spawnAmountPreInterval;

        public float GroupStartDelay => _groupStartDelay;
        

        public ActionStartType StartType => _startType;
        
        public void OnValidate(float startTime)
        {
            _startTime = startTime;
        }
    }
}