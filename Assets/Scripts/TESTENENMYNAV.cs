using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTENENMYNAV : MonoBehaviour
{
   
    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Physics.Raycast(r, out hit, 100f);
        }
    }
}
