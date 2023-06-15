using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.BaseSystem.TimeSystem;
using PathCreation;
using Shamans;
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
    public void SetPath(PathCreator pc, PathCreator finalDest, IEntityTargetAbleComponent target)
    {
        finalDestinaion = finalDest;
        privateRabbitProgress = 0;
        pathCreator = pc;
        attackTarget = target;

        GAME_TIME.OnGameTimeTick += AdvanceOnPath;
    }

    /// <summary>
    /// GameTickHooks (temp name)
    /// Logic that subscribes to TEMP_TIME.OnGameTimeTick. Basically the "GameUpdate" that works on Ticks instead of Update().
    /// Usually only one such hook/method would be subbed to TEMP_TIME.OnGameTimeTick, but that is not a rule.
    /// One entity/component could have as many GameTickHooks subbed as needed.
    /// With a preference of keeping it to "one 'major' hook pre component/script"
    /// 
    /// In this case we have 2 major hooks:
    /// AdvanceOnPath - assumes this unity has a legit SetPath(). Once hooked, this method maintains movement along the set path (with some logic and parameters for when and how to advance along the path)
    ///     This also maintains the exit-condition("are we there yet?") for this hook.
    ///     Once this Unit is within acceptableDistanceToCompletion from the target -> this method un-subbs from TEMP_TIME.OnGameTimeTick
    ///     and instead subbs the next hook:
    /// 
    /// CircleFinalDestination - assumes this unity has a legit FinalDestinaionPath. Once hooked, this method maintains movement through the loop around the target destination.
    ///     This loop CAN also be the one to call the attack component (since it would have to handle decisions about "being in attack Range" anyways -> it might aswell give the firing-orders)
    /// 
    /// </summary>
    #region GameTickHooks

    /// privateRabbitProgress is the amount of path this unit is allowed to progress, this starts at 0f.
    /// Evenry tick, these Units will try to get to a point on the path that is [privateRabbitProgress out of fullPathLength].
    /// Every tick, the Unit checks if it is within acceptableDistanceFromPath.
    /// (!!!!from PATH not from the "rabbit" point on the path, since they may be pushed further along the path, and should not be punished if they can't return)
    /// If the Unit is close enough to any point on the path -> privateRabbitProgress increases by privateRabbitIncrement.
    private void AdvanceOnPath()
    {
        _currentPointOnPath = pathCreator.path.GetPointAtDistance(privateRabbitProgress, EndOfPathInstruction.Stop);

        basicMoveComponent.SetDestination(_currentPointOnPath, MoveType.Guided);

        Vector3 closestPointOnPath = pathCreator.path.GetClosestPointOnPath(transform.position);

        //if (Vector3.Distance(transform.position, pointOnPath) <= acceptableDistanceFromPath)
        if (Vector3.Distance(transform.position, closestPointOnPath) <= acceptableDistanceFromPath)
        {
            privateRabbitProgress += privateRabbitIncrement;
            if (privateRabbitProgress > pathCreator.path.length && Vector3.Distance(transform.position, _currentPointOnPath) <= acceptableDistanceToCompletion)
            {
                GAME_TIME.OnGameTimeTick -= AdvanceOnPath;
                privateRabbitProgress = 0f;
                GAME_TIME.OnGameTimeTick += CircleFinalDestination;
            }
        }
    }
    #endregion
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
    private void OnDisable()
    {
        GAME_TIME.OnGameTimeTick -= AdvanceOnPath;
        GAME_TIME.OnGameTimeTick -= CircleFinalDestination;
    }

    private void OnDrawGizmos()
    {
        if (alwaysShowGizmo || UnityEditor.Selection.activeGameObject == gameObject)
            Gizmos.DrawCube(_currentPointOnPath, Vector3.one * rabbitGizmoBoxSize);
    }
    #endregion
}
