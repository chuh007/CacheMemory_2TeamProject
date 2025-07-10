using System.Linq;
using Chuh007Lib.StatSystem;
using UnityEngine;
using Work.CHUH._01Scripts.Enemies;

namespace Work.CHUH._01Scripts.Entities
{
    public class EntityStat : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private StatOverride[] statOverrides;
        private StatSO[] _stats; //real stat

        public Entity Owner { get; private set; }
        
        public void Initialize(Entity entity)
        {
            Owner = entity;
            _stats = statOverrides.Select(stat => stat.CreateStat()).ToArray(); 
        }
        
        public StatSO GetStat(StatSO targetStat)
        {
            Debug.Assert(targetStat != null, "Stats::GetStat : target stat is null");
            return _stats.FirstOrDefault(stat => stat.statName == targetStat.statName);
        }

        public bool TryGetStat(StatSO targetStat, out StatSO outStat)
        {
            Debug.Assert(targetStat != null, "Stats::GetStat : target stat is null");
            
            outStat = _stats.FirstOrDefault(stat => stat.statName == targetStat.statName);
            return outStat;
        }
    }
}