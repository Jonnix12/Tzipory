using Tzipory;
using UnityEngine;
using UnityEngine.Events;
using PathCreation;
using PathCreation.Examples;

[System.Serializable]
public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPositions; 

    [SerializeField]
    WaveGroup[] waveGroups;

    [SerializeField]
    GameObject rabbitPrefab;
    [SerializeField]
    PathCreator myPathCreator;

    public bool IsSpawnning;

    WaveGroup _currentWaveGroup;
    EnemyGroup _currentEnemyGroup;


    public UnityEvent OnSpawnStart;
    public UnityEvent OnSpawnEnd;
    //Should either Sub to LevelManager -> or report themeselves and then do nothing "of their own accord"

    PathFollower pf;
    private void Start()
    {
        LevelManager.Instance.RegisterWaveSpawner(this);
    }
    public void CallSpawnRandomWave()
    {
        
        Debug.Log($"{name} began spawning.");

        pf = Instantiate(rabbitPrefab, myPathCreator.transform).GetComponent<PathFollower>();

        pf.pathCreator = myPathCreator;

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
        
        int doneCount = 0;
     
        foreach (var enemyGroup in _currentWaveGroup.enemyGroups)
        {
            if (enemyGroup.ticker.IsDone)
            {
                doneCount++;
                continue;
            }
            
            if (enemyGroup.ticker.DoTick())
            {
                GameObject go = Instantiate(enemyGroup.prefab, spawnPositions.GetRandomElementFromArray()); //this should be pulling from an ItemPool, as Units
                go.GetComponent<MoveTest>().SetRabbit(pf.transform);
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
