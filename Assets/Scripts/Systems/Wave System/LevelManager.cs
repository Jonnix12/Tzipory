using System.Collections;
using System.Collections.Generic;
using Tzipory.Helpers;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    LevelRecepie _levelRecepie;

    int _waveCount;

    [SerializeField] List<WaveSpawner> _waveSpawners;

    //Ticker _waveTicker;

    //Temp TBF
    [SerializeField] float _levelStartDelay = 0.2f;
    [SerializeField] float _interwaveDelay;


    private void Awake()
    {
        _waveCount = _levelRecepie.NumberOfWaves;
       
    }
    
    void Start()
    {
        Invoke(nameof(StartLevel), _levelStartDelay);
    }

    void StartLevel()
    {
        StartCoroutine(nameof( LevelLoop));
    }

    bool AreAllSpawnersDone()
    {
        bool toReturn = true;
        
        foreach (var waveSpawner in _waveSpawners)
        {
            if(waveSpawner.IsSpawnning)
            {
                toReturn = false;
            }
        }
        return toReturn;
    }

    private IEnumerator LevelLoop()
    {
        while (_waveCount > 0)
        {
            for (int i = 0; i < _levelRecepie.MaxSimultaniousSpawners; i++)
            {
                WaveSpawner ws = _waveSpawners.GetRandomElementFromList();
                if (ws.IsSpawnning) //This may call the same spawner twice - sometimes causing less simultanious spawners than max
                {
                    //Debug.Log($"{ws.name} is aleady spawning!");
                    continue;
                }
                ws.CallSpawnRandomWave();
                _waveCount--;
                yield return new WaitForSecondsRealtime(_interwaveDelay);
            }
            yield return new WaitUntil(AreAllSpawnersDone);
        }
    }
}