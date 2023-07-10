using UnityEngine;
using ProjectDawn.Navigation.Hybrid;


public class Temp_RotFixer : MonoBehaviour
{
    //EXTRA TEMP! Flip with direction
    [SerializeField] private Rigidbody2D _agentBody;
    [SerializeField] private SpriteRenderer _toFlip;


    Quaternion ogRot;
    Vector3 ogScale;

    void Awake()
    {
        ogScale = transform.localScale;
    }
    
    void LateUpdate()
    {
        Vector3 _vel = _agentBody.velocity;

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
    }
}
