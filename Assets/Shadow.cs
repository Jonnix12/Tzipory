using ProjectDawn.Navigation.Hybrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _shadowRenderer;
    [SerializeField] private SpriteRenderer _proximityRenderer;
    [SerializeField] private SpriteMask _mask;

    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private NavMeshAgent _agent;
    //private AgentNavMeshAuthoring _agentNavMesh;

    public bool IsOn;

    private Transform _shamanTrans;

    public void SetShadow(Transform shaman, Sprite shadowSprite, float range)
    {
        gameObject.SetActive(true);
        IsOn = true;
        //_agentNavMesh = agentNavMesh;
        _shamanTrans = shaman;
        _shadowRenderer.sprite = shadowSprite;
        _mask.sprite = shadowSprite;
        _lineRenderer.gameObject.SetActive(true);
        _shadowRenderer.gameObject.SetActive(true);
        _proximityRenderer.transform.localScale = new Vector3(range, range, 1);
        _agent.transform.position = _shamanTrans.position;

        _agent.speed = 0; //make sure it doesnt really move
        _agent.SetDestination(transform.position);
    }

    public void ClearShadow()
    {
        IsOn = false;
        _lineRenderer.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsOn)
        {
            _agent.transform.position = _shamanTrans.position;
            _agent.SetDestination(transform.position); //resets destination, in case mouse moves? HEAVY AND BAD! 
            //_lineRenderer.positionCount = _agent.path.corners.Length;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(new Vector3[] {_agent.transform.position, transform.position });
            //TEMP!
            //END TEMP!
        }
    }
}
