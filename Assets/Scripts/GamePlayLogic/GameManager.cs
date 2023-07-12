using Tzipory.BaseSystem.TimeSystem;
using Tzipory.GamePlayLogic.ObjectPools;
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

   public void Reset()
   {
      GAME_TIME.SetTimeStep(1);
      //LevelManager.Instance = null;
      SceneManager.LoadScene(0);
   }
}
