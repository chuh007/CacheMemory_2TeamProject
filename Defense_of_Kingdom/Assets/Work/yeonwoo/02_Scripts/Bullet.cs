using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private float _speed = 10f;

    
    private void Update() => transform.position += Vector3.right * (_speed * Time.deltaTime);
    
}