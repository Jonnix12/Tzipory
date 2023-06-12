using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

public class Temp_Projectile : MonoBehaviour
{
    private IEntityTargetAbleComponent _target;
    private float _speed;
    private float _elapsedTime;

    private float _damage;

    private Vector2 _startPosition;
    
    public void Init(IEntityTargetAbleComponent target,float speed,float damage)
    {
        _speed = speed;
        _target = target;
        _damage = damage;
        _startPosition  = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += GAME_TIME.GameDeltaTime;
        float percentageComplete = _elapsedTime / _speed;
        if (!_target.GameEntity.isActiveAndEnabled)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.Lerp(_startPosition, _target.EntityTransform.position, percentageComplete);   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IEntityTargetAbleComponent>(out var target))
        {
            if (target.EntityInstanceID == _target.EntityInstanceID)
            {
                target.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
