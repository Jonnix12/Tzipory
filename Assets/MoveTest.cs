using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using PathCreation;
using System;

public class MoveTest : MonoBehaviour
{
    //[SerializeField]
    //AgentNavMeshAuthoring agentNav;
    [SerializeField]
    AgentAuthoring agent;
    [SerializeField]
    float privateRabbitSpeed;
    float privateRabbitProgress;

    static Camera cam;
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Transform _followRabbit;
    
    PathCreator pathCreator;
    Transform finalDestinaion;

   
    private void Start()
    {
        
        //TEMP_TIME.OnGameTimeTick += GoToRabbit;
        //TEMP_TIME.OnGameTimeTick += AdvanceOnPath;
    }
    private void OnDisable()
    {
        //TEMP_TIME.OnGameTimeTick -= GoToRabbit;
        TEMP_TIME.OnGameTimeTick -= AdvanceOnPath;

    }

    //public void SetRabbit(Transform newRabbit)
    //{
    //    _followRabbit = newRabbit;
    //}
    public void SetPath(PathCreator pc, Transform finalDest)
    {
        finalDestinaion = finalDest;
        privateRabbitProgress = 0;
        pathCreator = pc;
        //transform.position = pathCreator.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Stop);
        TEMP_TIME.OnGameTimeTick += AdvanceOnPath;

        //agent.SetDestination();

    }

    //void GoToRabbit()
    //{
    //    agent.SetDestination(_followRabbit.position);
    //}
    void AdvanceOnPath()
    {
        Vector3 pointOnPath = pathCreator.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Stop);


        agent.SetDestination(pointOnPath);
        
        if (Vector3.Distance(transform.position, pointOnPath) <= 1f)
        {
            privateRabbitProgress += privateRabbitSpeed;
            if (privateRabbitProgress > pathCreator.path.length)
            {
                TEMP_TIME.OnGameTimeTick -= AdvanceOnPath;
                CircleFinalDestination();
            }

        }
        //if(privateRabbitProgress >= 1)
        //{
        //    TEMP_TIME.OnGameTimeTick -= AdvanceOnPath;
        //    //agent.SetDestination()
        //}
    }

    void CircleFinalDestination()
    {
        agent.SetDestination(finalDestinaion.position);
    }

  
}
