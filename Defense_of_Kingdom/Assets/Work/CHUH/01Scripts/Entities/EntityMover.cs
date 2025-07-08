using UnityEngine;

namespace Work.CHUH._01Scripts.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        
    }
}