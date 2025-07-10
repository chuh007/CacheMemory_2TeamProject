using System;
using Chuh007Lib.StatSystem;
using UnityEngine;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Combat
{
    public abstract class EntityAttackCompo : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO attackRangeStat, attackDelayStat, damageStat;
        
        protected EntityStat _statCompo;
        protected float _attackRange;
        protected float _attackDelay;
        protected float _damage;
        
        public virtual void Initialize(Entity entity)
        {
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            _attackRange = _statCompo.GetStat(attackRangeStat).Value;
            _damage = _statCompo.GetStat(damageStat).Value;
            _attackDelay = _statCompo.GetStat(attackDelayStat).Value;
        }
        
        public virtual void Attack(CastleHealth target)
        {
            
        }
        
        public bool IsInRange(CastleHealth target)
            => Mathf.Abs(target.transform.position.x - transform.position.x) <= _attackRange;
        
        public virtual bool CanAttack(CastleHealth target)
        {
            return IsInRange(target);
        }
        
    }
}