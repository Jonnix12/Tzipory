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
    [SerializeField]
    float finalLoopSpeed;

    float privateRabbitProgress;

    static Camera cam;
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    Transform _followRabbit;
    
    PathCreator pathCreator;
    PathCreator finalDestinaion;

   
    
    private void OnDisable()
    {
        //TEMP_TIME.OnGameTimeTick -= GoToRabbit;
        TEMP_TIME.OnGameTimeTick -= AdvanceOnPath;
        TEMP_TIME.OnGameTimeTick -= CircleFinalDestination;

    }

   
    public void SetPath(PathCreator pc, PathCreator finalDest)
    {
        finalDestinaion = finalDest;
        privateRabbitProgress = 0;
        pathCreator = pc;
        
        TEMP_TIME.OnGameTimeTick += AdvanceOnPath;

        

    }

  
    void AdvanceOnPath()
    {
        Vector3 pointOnPath = pathCreator.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Stop);


        agent.SetDestination(pointOnPath);


       
        if (Vector3.Distance(transform.position, pointOnPath) <= privateRabbitSpeed/2f)
        {
            privateRabbitProgress += privateRabbitSpeed;
            //if (privateRabbitProgress > pathCreator.path.length)
            if (privateRabbitProgress > pathCreator.path.length && Vector3.Distance(transform.position, pointOnPath) <= 2)
            {
                TEMP_TIME.OnGameTimeTick -= AdvanceOnPath;
                privateRabbitProgress = 0f;
                TEMP_TIME.OnGameTimeTick += CircleFinalDestination;
            }

        }
       
    }

    void CircleFinalDestination()
    {
        Vector3 pointOnPath = finalDestinaion.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Loop);
        if (Vector3.Distance(transform.position, finalDestinaion.path.GetClosestPointOnPath(transform.position)) <= 2f)
        {
            privateRabbitProgress += finalLoopSpeed;
        }
        agent.SetDestination(pointOnPath);
    }

  
}
