using System;
using System.Collections;
using UnityEngine;

public class TEMP_TIME : MonoBehaviour
{
    //[SerializeField, Tooltip("Base duration for one Tick of game time.")]
    public static float TimeStep = 1f;

    //[SerializeField, Tooltip("Similar to TimeScale. Defualt value 1f. This is used to effect game time. The duration of one Tick is [_timeStep] divided by [_timeRate]. So a higher value, means time goes by 'quicker'.")]
    public static float TimeRate = 1f;


    //SACLED DELTA TIME (smoothStep)
    float Tick => TimeStep / TimeRate;
    public static float GameTimeDelta;

    public static Action OnGameTimeTick; //tbd static or private and static un/sub methods? - no added funtionality is really required to un/subbing that I can think of... 

    void Start()
    {
        StartCoroutine(nameof(RunTime));
    }

    public void StartTimer(float time)
    {
        StartCoroutine(nameof(RunTimer), time);
    }

    private IEnumerator RunTimer(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator RunTime()
    {
        GameTimeDelta = 0f;
        while(true)
        {
            yield return new WaitForSeconds(Tick);
//            Debug.Log("tick.");
            GameTimeDelta += Tick;
            OnGameTimeTick?.Invoke();
        }
    }
}