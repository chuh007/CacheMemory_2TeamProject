using System;
using UnityEngine;

namespace Work.CHUH._01Scripts.Entities
{
    public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponent
    {
        public Action OnAnimationEndTrigger;
        
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        private void AnimationEnd()
        {
            OnAnimationEndTrigger?.Invoke();
        }

    }
}