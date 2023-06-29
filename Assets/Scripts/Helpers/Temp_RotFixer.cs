using UnityEngine;
using ProjectDawn.Navigation.Hybrid;


public class Temp_RotFixer : MonoBehaviour
{
    //EXTRA TEMP! Flip with direction
    [SerializeField] private AgentAuthoring _agentAuthoring;
    [SerializeField] private SpriteRenderer _toFlip;


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
        // Vector3 newPos = transform.position;
        // newPos.y = transform.position.y / -100f;
        // newPos.y += transform.position.x / 300f;
        // transform.position = newPos;
        Vector3 _vel = _agentAuthoring.EntityBody.Velocity;

        if(_vel.sqrMagnitude != 0) //sqrMagnitude is cheaper than just magnitude. magnitude returns the root of magnitude.
        {
            if(_vel.x < 0)
            {
                //left
                _toFlip.flipX = false;
            }
            else
            {
                //right
                _toFlip.flipX = true;

            }
        }
        //transform.localScale = ogScale * (1f+newPos.y*4f);
    }
}
