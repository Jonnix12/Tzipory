using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSpawnerSerializeData
    {
        private Color _waveSpawnerColor;
        [SerializeField,GUIColor("GetColor")] private EnemyGroupSerializeData[] _enemyGroups;
        [SerializeField,GUIColor("GetColor")] private float _delayBetweenEnemyGroup;
        
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