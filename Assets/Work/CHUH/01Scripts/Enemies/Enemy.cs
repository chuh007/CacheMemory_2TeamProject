using Chuh007Lib.Dependencies;
using UnityEngine;
using Work.CHUH._01Scripts.Combat;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Enemies
{
    public abstract class Enemy : Entity
    {
        [Inject] protected CastleHealth _target;
        
        protected EntityMover _entityMover;
        protected EntityAttackCompo _entityAttackCompo;

        protected float _attackRange;
        
        protected override void Awake()
        {
            base.Awake();
            _entityMover = GetCompo<EntityMover>();
            _entityAttackCompo = GetCompo<EntityAttackCompo>();
        }
    }
}