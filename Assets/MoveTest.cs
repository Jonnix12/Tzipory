using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectDawn.Navigation.Hybrid;

public class MoveTest : MonoBehaviour
{
    //[SerializeField]
    //AgentNavMeshAuthoring agentNav;
    [SerializeField]
    AgentAuthoring agent;
    
    static Camera cam;
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Transform _followRabbit;

    //private void Awake()
    //{
    //    if(!cam) //because it's static, it'll only happen once 
    //    {
    //        cam = Camera.main; //TEMP
    //    }
    //}

    private void Start()
    {
        TEMP_TIME.OnGameTimeTick += GoToRabbit;
    }
    private void OnDisable()
    {
        TEMP_TIME.OnGameTimeTick -= GoToRabbit;
    }

    public void SetRabbit(Transform newRabbit)
    {
        _followRabbit = newRabbit;
    }

    void GoToRabbit()
    {
        agent.SetDestination(_followRabbit.position);
    }
    //void Update()
    //{
    //    //temp!
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        RaycastHit hit;
    //        if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000f, layerMask))
    //        {
    //            agent.SetDestination(hit.point);
    //        }
    //    }
    //}
}
