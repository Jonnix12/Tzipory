using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    public static T GetRandomElementFromList<T>(List<T> ts)
    {
        if(ts == null || ts.Count == 0)
        {
            return default(T);
        }

        int randomIndex = Random.Range(0, ts.Count);
        return ts[randomIndex];
    }
    public static T GetRandomElementFromArray<T>(T[] tArray)
    {
        if(tArray == null || tArray.Length == 0)
        {
            return default(T);
        }

        int randomIndex = Random.Range(0, tArray.Length);
        return tArray[randomIndex];
    }

}
