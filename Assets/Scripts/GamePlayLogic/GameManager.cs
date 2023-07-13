using System;
using System.Collections.Generic;
using System.Linq;
using GameplayeLogic.Managers;
using Shamans;
using Sirenix.OdinInspector;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.GamePlayLogic.ObjectPools;
using Tzipory.Leval;
using Tzipory.SerializeData.LevalSerializeData;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private PoolManager _poolManager;

   private void Awake()
   {
      _poolManager = new PoolManager();
   }

   public void Quit()
   {
      Application.Quit();
   }
    public static PartyManager PartyManager { get; private set; }
    public static EnemyManager EnemyManager { get; private set; }
    public static PlayerManager PlayerManager { get; private set; }
    public static LevelManager LevelManager { get; private set; }
    public static CoreTemple CoreTemplete { get; private set; }
    
    
    [SerializeField,TabGroup("Party manager")] private List<Shaman> _party;//temp!!!

    [SerializeField, TabGroup("Level manager")]
    private Transform _levelParent;
    [SerializeField, TabGroup("Level manager")]
    private LevelSerializeData _levelSerializeData;
    
    private void Awake()
    {
        EnemyManager = new EnemyManager();
        PlayerManager = new PlayerManager();
        PartyManager = new PartyManager(_party);
        LevelManager  = new LevelManager(_levelSerializeData,_levelParent);//temp!
        CoreTemplete = FindObjectOfType<CoreTemple>();//temp!!!
    }

    private void Update()
    {
        LevelManager.UpdateLevel();
    }

    public static List<WaveSpawner> GetWaveSpawners() => FindObjectsOfType<WaveSpawner>().ToList();

    private void OnDestroy()
    {
        PartyManager = null;
        EnemyManager = null;
        PlayerManager = null;
        LevelManager = null;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        GAME_TIME.SetTimeStep(1);
        //LevelManager.Instance = null;
        SceneManager.LoadScene(0);
    }
}