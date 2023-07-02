using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSpawnerSerializeData
    {
        [SerializeField,ReadOnly] private Color _waveSpawnerColor;
        [SerializeField] private EnemyGroupSerializeData[] _enemyGroups;

        private int _currentEnemyGroup;

        private WaveSpawner _waveSpawner;
        
        public EnemyGroupSerializeData[] EnemyGroups => _enemyGroups;

        public Color WaveSpawnerColor => _waveSpawner.WaveSpawnerColor;

        public WaveSpawnerSerializeData(WaveSpawner waveSpawner)
        {
            _waveSpawnerColor = LevelManager.GetSpawnerColor();
            _currentEnemyGroup = 0;
        }
        
        public bool GetNextEnemyGroup(out EnemyGroupSerializeData enemyGroup)
        {
            enemyGroup = null;
            
            if (_enemyGroups.Length == 0) return false;
            if (_currentEnemyGroup >= _enemyGroups.Length) return false;
            
            enemyGroup = _enemyGroups[_currentEnemyGroup];
            _currentEnemyGroup++;
            return true;
        }
    }
}