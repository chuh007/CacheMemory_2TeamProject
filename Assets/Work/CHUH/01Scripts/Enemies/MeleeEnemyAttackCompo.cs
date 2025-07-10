using System;
using UnityEngine;
using Work.CHUH._01Scripts.Combat;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Enemies
{
    public class MeleeEnemyAttackCompo : EntityAttackCompo
    {
        private float _cooldownTimer;
        
        public bool IsCollTime => _cooldownTimer > 0f;

        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _cooldownTimer = _attackDelay;
        }

        public override bool CanAttack(CastleHealth target)
        {
            return base.CanAttack(target) && !IsCollTime;
        }

        private void Update()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
            }
        }

        public override void Attack(CastleHealth target)
        {
            base.Attack(target);
            _cooldownTimer = _attackDelay;
            Debug.Log("공격");
        }
    }
}