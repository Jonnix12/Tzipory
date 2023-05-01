using Tzipory;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPositions; 

    [SerializeField]
    WaveGroup[] waveGroups;

    public bool IsSpawnning;

    WaveGroup _currentWaveGroup;
    EnemyGroup _currentEnemyGroup;


    public UnityEvent OnSpawnStart;
    public UnityEvent OnSpawnEnd;
    //Should either Sub to LevelManager -> or report themeselves and then do nothing "of their own accord"
    private void Start()
    {
        LevelManager.Instance.RegisterWaveSpawner(this);
    }

    public void CallSpawnRandomWave()
    {
        
        Debug.Log($"{name} began spawning.");

        _currentWaveGroup = waveGroups.GetRandomElementFromArray();
        _currentWaveGroup.SetAllTickers();
        _currentWaveGroup.isLive = true;// should move to the init/SetAllTickers
        IsSpawnning = true;
        OnSpawnStart.Invoke();
        TEMP_TIME.OnGameTimeTick += OnTick_SpawnCurrentWaveGroup;
    }


    //happens every game-tick
    void OnTick_SpawnCurrentWaveGroup()
    {
        //_currentWaveGroup.TickAllGroups();
        int doneCount = 0;
        foreach (var enemyGroup in _currentWaveGroup.enemyGroups)
        {
            if (enemyGroup.ticker.IsDone)
            {
                doneCount++;
                continue;
            }
            //enemyGroup.tick++;
            //if(enemyGroup.tick >= enemyGroup.spawnRate.y)
            if (enemyGroup.ticker.DoTick())
            {
                //enemyGroup.tick = 0f;
                GameObject go = Instantiate(enemyGroup.prefab, spawnPositions.GetRandomElementFromArray());
                //enemyGroup.spawnRate.x--; //to reduce the remaining enemies to spawn - likely temp. TBF - should be a part of the ticker
            }
        }
        if (doneCount == _currentWaveGroup.enemyGroups.Length)
        {
            Debug.Log($"{name} completed spawning and is unsubbing from tick");
            IsSpawnning = false;
            OnSpawnEnd.Invoke();
            _currentWaveGroup.isLive = false;
            TEMP_TIME.OnGameTimeTick -= OnTick_SpawnCurrentWaveGroup;
        }
    }


}
