using Chuh007Lib.StatSystem;
using UnityEngine;

namespace Work.CHUH._01Scripts.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO speedStat;
        
        private Entity _entity;
        private EntityStat _statCompo;
        private Rigidbody2D _rbCompo;
        private float _speed;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = _entity.GetCompo<EntityStat>();
            _rbCompo = entity.GetComponent<Rigidbody2D>();
        }
        
        public void AfterInitialize()
        {
            _speed = _statCompo.GetStat(speedStat).Value;
        }
        
        public void Move(Vector2 dir)
        {
            _rbCompo.linearVelocity = dir * _speed;
        }

        public void Stop()
        {
            _rbCompo.linearVelocity = Vector2.zero;
        }

    }
}