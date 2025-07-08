using System;
using UnityEngine;
using Work.CHUH._01Scripts.Combat;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Enemies
{
    public class MeleeEnemy : Enemy
    {
        private EntityMover _entityMover;
        private EntityAttackCompo _entityAttackCompo;

        private IDamageable _target;
        
        protected override void Awake()
        {
            base.Awake();
            _entityMover = GetCompo<EntityMover>();
            _entityAttackCompo = GetCompo<EntityAttackCompo>();
        }

        private void Update()
        {
            
        }
    }
}