using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSpawnerSerializeData
    {
        private Color _waveSpawnerColor;
        [SerializeField] private float _delayBetweenEnemyGroup;
        [SerializeField] private EnemyGroupSerializeData[] _enemyGroups;
        
        public EnemyGroupSerializeData[] EnemyGroups => _enemyGroups;

        public float DelayBetweenEnemyGroup => _delayBetweenEnemyGroup;

        public WaveSpawnerSerializeData(WaveSpawner waveSpawner)
        {
            _waveSpawnerColor = waveSpawner.WaveSpawnerColor;
        }

        private Color GetColor()
            => _waveSpawnerColor;
        
    }
}