using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Work.CHUH._01Scripts.Enemies;


public class HeroAttack : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _attackRange = 10f; 
    [SerializeField] private LayerMask _enemyLayerMask = -1; 
    
    private bool _canFire = true;
    private Transform _currentTarget;

    private void Start()
    {
        StartCoroutine(AutoFire());
    }

    private Transform FindNearestEnemy()
    {
        // 사거리 내의 모든 적 찾기
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

    private void Fire()
    {
        if (!_canFire) return;
        
        
        _currentTarget = FindNearestEnemy();
        
        if (_currentTarget == null) return; 
        
        
        Vector2 direction = (_currentTarget.position - _firePoint.position).normalized;
        
        
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        
        
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(_currentTarget);
        }
        
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * 10f;
        }

        StartCoroutine(FireCooldown());
    }

    private IEnumerator FireCooldown()
    {
        _canFire = false;
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }
    
    private IEnumerator AutoFire()
    {
        while (true)
        {
            if (_canFire)
            {
                Fire();
            }
            yield return new WaitForSeconds(_fireRate);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}