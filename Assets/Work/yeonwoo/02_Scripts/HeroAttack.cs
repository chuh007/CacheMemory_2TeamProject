using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroAttack : MonoBehaviour
{
    
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.5f;
    
    
    private bool _canFire = true;


    private void Start()
    {
        StartCoroutine((AutoFire()));
    }


    private void Fire()
    {

        if (!_canFire) return;
        
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = _firePoint.right * 10f;
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

  
}
