using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private float _speed = 10f;
   private float _lifeTime = 5f;

   private void Update()
   {
      _lifeTime -= Time.deltaTime;
      if (_lifeTime <= 0)
      {
         Destroy(gameObject);
         Debug.Log(("d"));
         return;
      }
      transform.position += Vector3.right * (_speed * Time.deltaTime);
   }

}