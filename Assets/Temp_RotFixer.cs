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
        Vector3 newPos = transform.position;
        newPos.y = transform.position.z / -100f;
        newPos.y += transform.position.x / 300f;
        transform.position = newPos;
    }
}
