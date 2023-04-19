using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_TIME : MonoBehaviour
{
    //[SerializeField, Tooltip("Base duration for one Tick of game time.")]
    public static float TimeStep = 1f;

    //[SerializeField, Tooltip("Similar to TimeScale. Defualt value 1f. This is used to effect game time. The duration of one Tick is [_timeStep] divided by [_timeRate]. So a higher value, means time goes by 'quicker'.")]
    public static float TimeRate = 1f;


    //SACLED DELTA TIME (smoothStep)

    float Tick => TimeStep / TimeRate;
    public static float GameTime;

    public static Action OnGameTimeTick; //tbd static or private and static un/sub methods? - no added funtionality is really required to un/subbing that I can think of... 

    void Start()
    {
        StartCoroutine(nameof(RunTime));
    }

    IEnumerator RunTime()
    {
        GameTime = 0f;
        while(true)
        {
            yield return new WaitForSeconds(Tick);
            Debug.Log("tick.");
            GameTime += Tick;
            OnGameTimeTick?.Invoke();
        }
    }
}
