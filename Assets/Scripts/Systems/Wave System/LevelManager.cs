using System.Collections.Generic;
using Sirenix.OdinInspector;
using Tzipory.SerializeData.LevalSerializeData;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _waveSpawnerPrefab;
    [SerializeField] private LevelSerializeData _levelSerializeData;

    private static Dictionary<Color,bool> SpawnerColors = new()
    {
        {Color.red,false},
        {Color.gray,false},
        {Color.green,false},
        {Color.blue,false},
        {Color.magenta,false},
        {Color.cyan,false},
        {Color.yellow,false},
    };
    
    private int _waveCount;

    public static List<WaveSpawner> WaveSpawners { get; private set; }

    private void Awake()
    {
        
    }
    
   
    [Button("Add new wave spawner")]
    private void AddWaveSpawner()
    {
        var waveSpawner = Instantiate(_waveSpawnerPrefab, transform, true).GetComponent<WaveSpawner>();
        waveSpawner.Init();

        WaveSpawners ??= new List<WaveSpawner>();
        WaveSpawners.Add(waveSpawner);
        _levelSerializeData.AddWaveSpawner(waveSpawner);
    }
    
    private void OnValidate()
    {
        // if (_waveSpawners == null)
        //     _waveSpawners  = new List<WaveSpawner>();
        //
        // foreach (var waveSpawner in _waveSpawners)
        // {
        //     if (waveSpawner == null)
        //     {
        //         Debug.Log("waveSpawner is null");
        //         _waveSpawners.Remove(waveSpawner);
        //     }
        // }
        //
        // var waveSpwners = FindObjectsOfType<WaveSpawner>();
        //
        // foreach (var waveSpawner in waveSpwners)
        // {
        //     if (!_waveSpawners.Contains(waveSpawner))
        //     {
        //         _waveSpawners.Add(waveSpawner);
        //         foreach (var waveSerializeData in _levelSerializeData.Waves)
        //             waveSerializeData.AddWaveSpawner(waveSpawner);
        //     }
        // }
    }

    public static Color GetSpawnerColor()
    {
        foreach (var color in SpawnerColors)
        {
            if (!color.Value)
            {
                SpawnerColors[color.Key] = true;
                return color.Key;
            }
        }

        Debug.Log("No free color!");
        return Color.white;
    }

    public static void ReturnColor(Color color) =>
        SpawnerColors[color] = false;
    

    private static void ResetColors()
    {
        foreach (var color in SpawnerColors)
            SpawnerColors[color.Key] = false;
    }

    void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    private void OnDisable()
    {
        WaveSpawners = null;
        ResetColors();
    }

    bool AreAllSpawnersDone()
    {
        bool toReturn = true;
        
        foreach (var waveSpawner in WaveSpawners)
        {
            if(waveSpawner.IsSpawnning)
            {
                toReturn = false;
            }
        }
        return toReturn;
    }
}