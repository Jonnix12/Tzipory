using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_RotFixer : MonoBehaviour
{
    Quaternion ogRot;
    Vector3 ogScale;

    void Awake()
    {
        ogRot = transform.rotation;
        ogScale = transform.localScale;
    }

    
    void LateUpdate()
    {
        transform.rotation = ogRot;
        Vector3 newPos = transform.position;
        newPos.y = transform.position.z / -100f;
        newPos.y += transform.position.x / 300f;
        transform.position = newPos;

        //transform.localScale = ogScale * (1f+newPos.y*4f);
    }
}
