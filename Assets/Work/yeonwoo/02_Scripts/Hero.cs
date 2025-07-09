using System.Collections;
using UnityEngine;
using Work.CHUH._01Scripts.Enemies;

public abstract class Hero : MonoBehaviour
{
    [Header("영웅 기본 설정")]
    [SerializeField] protected bool _hasBasicAttack = true;
    [SerializeField] protected float _attackRange = 10f;
    [SerializeField] protected float _fireRate = 0.5f;
    [SerializeField] protected LayerMask _enemyLayerMask = -1;
    
    [Header("기본 공격 설정")]
    [SerializeField] protected Transform _firePoint;
    
    protected bool _canFire = true;
    protected Transform _currentTarget;
    
    protected virtual void Start()
    {
        if (_hasBasicAttack)
        {
            StartCoroutine(AutoFire());
        }
    }
    
    protected Transform FindNearestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _attackRange, _enemyLayerMask);
        
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.GetComponent<MeleeEnemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }
        
        return nearestEnemy;
    }
    
    private IEnumerator AutoFire()
    {
        while (true)
        {
            if (_canFire && _hasBasicAttack)
            {
                PerformBasicAttack();
            }
            yield return new WaitForSeconds(_fireRate);
        }
    }
    
    
    protected IEnumerator FireCooldown()
    {
        _canFire = false;
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }
    
    protected abstract void PerformBasicAttack();
    
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
   
    protected void SetCurrentTarget()
    {
        _currentTarget = FindNearestEnemy();
    }
   
    protected Vector2 GetDirectionToTarget()
    {
        if (_currentTarget == null) return Vector2.zero;
        return (_currentTarget.position - _firePoint.position).normalized;
    }
}