using UnityEngine;

public class HeroAttack : Hero
{
    [Header("발사체 공격 설정")]
    [SerializeField] private GameObject _bulletPrefab;
    
    protected override void PerformBasicAttack()
    {
        if (!_canFire) return;
        
        SetCurrentTarget();
        
        if (_currentTarget == null) return;
        
        Vector2 direction = GetDirectionToTarget();
        
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
}