using Chuh007Lib.StatSystem;
using UnityEngine;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Combat
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IDamageable, IAfterInitialize
    {
        private Entity _entity;

        [SerializeField] private StatSO hpStat;
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        public void AfterInitialize()
        {
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
            
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            if (currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }

            _entity.OnHitEvent?.Invoke();
        }

        
    }
}