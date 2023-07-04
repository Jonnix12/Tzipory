using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSpawnerSerializeData
    {
        [SerializeField,ReadOnly] private Color _waveSpawnerColor;
        [SerializeField] private EnemyGroupSerializeData[] _enemyGroups;
        [SerializeField] private float _delayBetweenEnemyGroup;
        
        public EnemyGroupSerializeData[] EnemyGroups => _enemyGroups;

        public float DelayBetweenEnemyGroup => _delayBetweenEnemyGroup;

        public WaveSpawnerSerializeData(WaveSpawner waveSpawner)
        {
            _waveSpawnerColor = waveSpawner.WaveSpawnerColor;
        }
    }
}