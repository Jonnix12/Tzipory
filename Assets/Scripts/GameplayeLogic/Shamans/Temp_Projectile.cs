using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

public class Temp_Projectile : MonoBehaviour
{
    private IEntityTargetAbleComponent _target;
    private float _speed;
    private float _elapsedTime;

    private float _damage;

    private bool _isCrit;

    private Vector2 _startPosition;
    
    public void Init(IEntityTargetAbleComponent target,float speed,float damage,bool isCrit)
    {
        _speed = speed;
        _target = target;
        _damage = damage;
        _startPosition  = transform.position;
        _isCrit = isCrit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_target.GameEntity.isActiveAndEnabled)
        {
            Destroy(gameObject);
            return;
        }
        
        var position = transform.position;
        var dir=(_target.EntityTransform.position - position).normalized;
 
        position += dir * (_speed * GAME_TIME.GameDeltaTime);
        transform.position = position;


        // _elapsedTime += GAME_TIME.GameDeltaTime;
        // float percentageComplete = _elapsedTime / _speed;
        // transform.position = Vector3.Lerp(_startPosition, _target.EntityTransform.position, percentageComplete);   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IEntityTargetAbleComponent>(out var target))
        {
            if (target.EntityInstanceID == _target.EntityInstanceID)
            {
                target.TakeDamage(_damage,_isCrit);
                Destroy(gameObject);
            }
        }
    }
}
