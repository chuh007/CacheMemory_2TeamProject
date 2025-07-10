using System;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private LayerMask Enemy; //적 레이어
    [SerializeField] private float _height; //궤적의 높이
    private float _t = 0f;//궤적의 진행도
    private Vector2 _arrow;//화살의 시작점
    private Vector2 _enemy;//가장 가까운 적의 위치를 저장함
    private float _timeElapsed = 0f;//화살 생성후 경과 시간
    private float _duration = 1.7f;//화살이 목표까지 날아가는데 걸리는 사간
    private void OnEnable()
    {
        _arrow = transform.position;
        _enemy = FindEnemy(Enemy).position;
        Parabola(_arrow,_enemy,_height,_t);
    }
    
    private void Update()
    {
        var enemyTransform = FindEnemy(Enemy);
        if (enemyTransform == null) return;
        _timeElapsed += Time.deltaTime;
        float t = Mathf.Clamp01(_timeElapsed / _duration);
        Vector2 prevPos = transform.position;
        Vector2 nextPos = Parabola(_arrow, enemyTransform.position, _height, t);
        Vector2 direction = nextPos - prevPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = nextPos;
    }

    private Transform FindEnemy(LayerMask enemy)
    {
        Vector2 currentPos = transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(currentPos, 200f, enemy);
        Transform nearEnemyTransform = null;
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(currentPos, hit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearEnemyTransform = hit.transform;
            }
        }
        return nearEnemyTransform;
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.gameObject.CompareTag("Enemy")) //Enemy는 임시적인 레이어 명이라 추후에 적의 레이어로 변경해야함.
        {
            //맞은적에게 데미지를 주는 메서드
            Debug.Log("die");
            Destroy(gameObject);
        }
    }
}
