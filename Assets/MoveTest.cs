using UnityEngine;
using ProjectDawn.Navigation.Hybrid;
using PathCreation;
using Sirenix.OdinInspector;

public class MoveTest : MonoBehaviour
{
    //[SerializeField]
    //AgentNavMeshAuthoring agentNav;
    [SerializeField]
    private AgentAuthoring agent;
    [SerializeField]
    private float privateRabbitSpeed;
    [SerializeField]
    private float finalLoopSpeed;

    private float privateRabbitProgress;

    static Camera cam;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
     private Transform _followRabbit;
    
    private PathCreator pathCreator;
    private PathCreator finalDestinaion;
#if UNITY_EDITOR
    [Header("Test")] [SerializeField] private Transform _target;
#endif
    
    public void SetPath(PathCreator pc, PathCreator finalDest)
    {
        finalDestinaion = finalDest;
        privateRabbitProgress = 0;
        pathCreator = pc;
        
        TEMP_TIME.OnGameTimeTick += AdvanceOnPath;
    }

    private void OnDisable()
    {
        //TEMP_TIME.OnGameTimeTick -= GoToRabbit;
        TEMP_TIME.OnGameTimeTick -= AdvanceOnPath;
        TEMP_TIME.OnGameTimeTick -= CircleFinalDestination;
    }

    private void AdvanceOnPath()
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

    private void CircleFinalDestination()
    {
        Vector3 pointOnPath = finalDestinaion.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Loop);
        if (Vector3.Distance(transform.position, finalDestinaion.path.GetClosestPointOnPath(transform.position)) <= 2f)
        {
            privateRabbitProgress += finalLoopSpeed;
        }
        agent.SetDestination(pointOnPath);
    }
    
    [Button("Test movement")]
    private void TestMovement()
    {
        agent.SetDestination(_target.position);
    }
}
