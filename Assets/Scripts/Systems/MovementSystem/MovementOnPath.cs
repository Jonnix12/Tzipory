using UnityEngine;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.BaseSystem.TimeSystem;
using PathCreation;
using Enemes;

public class MovementOnPath : MonoBehaviour
{
    #region TempRefs
    [SerializeField]
    BasicMoveComponent basicMoveComponent; //will be set with init

    [SerializeField]
    private float privateRabbitIncrement; //will be set with config
    [SerializeField]
    private float acceptableDistanceFromPath; //?
    [SerializeField]
    private float acceptableDistanceToCompletion =2f; //??

    [SerializeField]
    private float finalLoopSpeed; //I think I need to clean this spot a bit more

    private float privateRabbitProgress; //this is actually fine... the name can change
    private Vector3 _currentPointOnPath; //considering caging this in some #if UNITY_EDITOR with elses just so these don't bother us later...

    private PathCreator pathCreator; //will be set with init
    private PathCreator finalDestinaion; //will be set with init

    #endregion

    public Vector3 CurrentPointOnPath => _currentPointOnPath;

#if UNITY_EDITOR
    [SerializeField]
    private float rabbitGizmoBoxSize= 1f;
    [SerializeField, Tooltip("Set to true if you want this Unit's Rabbit-gizmo to draw at all times (Default: false, only draws gizmo for this Unit when it is Selected in the editor (inspector)")]
    private bool alwaysShowGizmo = false;
#endif
    
    IEntityTargetAbleComponent attackTarget;
    /// <summary>
    /// Basically THE method which sets the Unit on a path and hooks the AdvanceOnPath method.
    /// Starts following the path.

    /// </summary>
    /// <param name="pc"></param>
    /// <param name="finalDest"></param>
    public void SetPath(PathCreator pc)
    {
        privateRabbitProgress = 0;
        pathCreator = pc;
        //finalDestinaion = finalDest;
        //attackTarget = target;
    }

    
    public void AdvanceOnPath()
    {
        if (pathCreator == null)
            return;

        _currentPointOnPath = pathCreator.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Stop);

        basicMoveComponent.SetDestination(_currentPointOnPath, MoveType.Guided);

        Vector3 closestPointOnPath = pathCreator.path.GetClosestPointOnPath(transform.position);

        //if (Vector3.Distance(transform.position, pointOnPath) <= acceptableDistanceFromPath)
        if (Vector3.Distance(transform.position, closestPointOnPath) <= acceptableDistanceFromPath)
        {
            privateRabbitProgress += privateRabbitIncrement;
            if (privateRabbitProgress > pathCreator.path.length && Vector3.Distance(transform.position, _currentPointOnPath) <= acceptableDistanceToCompletion)
            {
                //CircleFinalDestination();
            }
        }
    }
   
    private void CircleFinalDestination()
    {
        _currentPointOnPath = finalDestinaion.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Loop);
        if (Vector3.Distance(transform.position, finalDestinaion.path.GetClosestPointOnPath(transform.position)) <= acceptableDistanceToCompletion)
        {
            privateRabbitProgress += finalLoopSpeed;
        }
        basicMoveComponent.SetDestination(_currentPointOnPath, MoveType.Free);

        //TEMP!!!!!!
        GetComponent<Enemy>().SetAttackTarget(attackTarget);
    }

    #region Callbacks
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (alwaysShowGizmo || UnityEditor.Selection.activeGameObject == gameObject)
            Gizmos.DrawCube(_currentPointOnPath, Vector3.one * rabbitGizmoBoxSize);
    }
    
#endif
    #endregion
}
