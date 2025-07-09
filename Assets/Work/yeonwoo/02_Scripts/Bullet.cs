using System.Collections.Generic;
using UnityEngine;
using Work.CHUH._01Scripts.Enemies;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private bool _followTarget = true; 
    private float _lifeTime = 5f;
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (_followTarget && _target != null)
        {
            
            Vector2 direction = (_target.position - transform.position).normalized;
            transform.position += (Vector3)direction * (_speed * Time.deltaTime);
            
            
            if (Vector2.Distance(transform.position, _target.position) < 0.1f)
            {
                
                Destroy(gameObject);
            }
        }
        else
        {
            
            transform.position += Vector3.right * (_speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponent<MeleeEnemy>() != null)
        {
            Destroy(gameObject);
        }
    }
}