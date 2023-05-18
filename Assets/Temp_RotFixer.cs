using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_RotFixer : MonoBehaviour
{
    Quaternion ogRot;

    void Awake()
    {
        ogRot = transform.rotation;
    }

    
    void LateUpdate()
    {
        transform.rotation = ogRot;
    }
}
