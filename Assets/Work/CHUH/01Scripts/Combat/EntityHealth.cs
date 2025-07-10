using Chuh007Lib.StatSystem;
using UnityEngine;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInitialize
    {

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        
        private Entity _entity;
        private EntityStat _statCompo;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = entity.GetCompo<EntityStat>();
        }
        
        public void AfterInitialize()
        {
            maxHealth = currentHealth = _statCompo.GetStat(hpStat).Value;
        }

        private void OnDestroy()
        {
        }

        private void HandleMaxHPChange(StatSO stat, float currentvalue, float prevvalue)
        {
            float changed = currentvalue - prevvalue;
            maxHealth = currentvalue;
            if (changed > 0)
            {
                currentHealth = Mathf.Clamp(currentHealth + changed, 0, maxHealth);
            }
            else
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
        }

        public void ApplyDamage(float damage)
        {
            if(_entity.IsDead) return;
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            if (currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }

            _entity.OnHitEvent?.Invoke();
        }

        
    }
}