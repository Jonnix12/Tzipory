using Tzipory.BaseSystem.TimeSystem;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void Quit()
   {
      Application.Quit();
   }

   public void Reset()
   {
      GAME_TIME.SetTimeStep(1);
      LevelManager.Instance = null;
      SceneManager.LoadScene(0);
   }
}
