using System;
using System.Collections.Generic;
using Enemes;
using UnityEngine;
using PathCreation;
using Tzipory.SerializeData.LevalSerializeData;
using Tzipory.Tools.Interface;
using Tzipory.WaveSystem;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour , IProgress
{
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private PathCreator myPathCreator;

    private List<IProgress> _completedEnemyGroups;

    private EnemyGroupSerializeData[] _enemyGroups;

    private int _currentEnemyGroupIndex;
    
    private float _delayBetweenEnemyGroup;

    private EnemyGroup _currentEnemyGroup;
    
    public Color WaveSpawnerColor { get; private set; }

    public bool IsSpawning { get; private set; }
    
    public float CompletionPercentage { get; }

    public int TotalNumberOfEnemiesPreWave { get; private set; }

    public bool IsDone
    {
        get
        {
            if (_completedEnemyGroups == null)
                return false;
            
            if (_completedEnemyGroups.Count != _enemyGroups.Length) 
                return false;
            
            foreach (var completedEnemyGroup in _completedEnemyGroups)
            {
                if (!completedEnemyGroup.IsDone)
                    return false;
            }
            return true;
        }
    }

    public void Init(WaveSpawnerSerializeData waveSpawnerSerializeData)
    {
        _completedEnemyGroups = new List<IProgress>();
        _enemyGroups = waveSpawnerSerializeData.EnemyGroups;
        _delayBetweenEnemyGroup = waveSpawnerSerializeData.DelayBetweenEnemyGroup;
        _currentEnemyGroupIndex = 0;
        
        foreach (var enemyGroupSerializeData in _enemyGroups)
            TotalNumberOfEnemiesPreWave += enemyGroupSerializeData.TotalSpawnAmount;

        if (TryGetNextEnemyGroup(out var enemyGroup))
            _currentEnemyGroup = enemyGroup;
        else
            throw new Exception("Not enemy group for the spawner");
    }

    private void Update()
    {
        if (IsDone || _currentEnemyGroup == null)
            return;
        
        if (_currentEnemyGroup.IsDone)
        {
            _completedEnemyGroups.Add(_currentEnemyGroup);
            
            if (TryGetNextEnemyGroup(out var enemyGroup))
                _currentEnemyGroup = enemyGroup;
        }

        if (!_currentEnemyGroup.TryGetEnemy(out var enemyPrefab))
            return;

        var enemy = Instantiate(enemyPrefab, _spawnPositions[Random.Range(0, _spawnPositions.Length)].position,
            Quaternion.identity); //temp!! need to add pool system

        var enemyMoveComponent = enemy.GetComponent<MovementOnPath>();
        enemyMoveComponent.SetPath(myPathCreator);
#if UNITY_EDITOR
        var enemyComponent = enemy.GetComponent<Enemy>();
        enemy.gameObject.name = $"Enemy InstanceID: {enemyComponent.EntityInstanceID}";
#endif
    }

    private bool TryGetNextEnemyGroup(out EnemyGroup enemyGroup)
    {
        enemyGroup = default;
        
        if (_enemyGroups.Length == 0) return false;
        if (_currentEnemyGroupIndex >= _enemyGroups.Length) return false;
            
        enemyGroup = new EnemyGroup(_enemyGroups[_currentEnemyGroupIndex]);
        _currentEnemyGroupIndex++;
        return true;
    }
    
    public void SetColor(Color color)=>
        WaveSpawnerColor = color;
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = WaveSpawnerColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}