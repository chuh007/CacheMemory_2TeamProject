using System;
using Chuh007Lib.Dependencies;
using UnityEngine;
using Work.CHUH._01Scripts.Combat;
using Work.CHUH._01Scripts.Entities;

namespace Work.CHUH._01Scripts.Enemies
{
    public enum AnimationParam
    {
        Idle,
        Move,
        Attack,
        Dead
    }
    
    public abstract class Enemy : Entity
    {
        [Inject] protected CastleHealth _target;
        
        protected EntityMover _entityMover;
        protected EntityAttackCompo _entityAttackCompo;
        protected EntityHealth _entityHealth;
        protected EntityAnimator _entityAnimator;

        protected Rigidbody2D _rbCompo;
        
        protected AnimationParam _currentAnimation;
        protected int _currentAnimationHash;
        
        private readonly int idleHash = Animator.StringToHash("IDLE");
        private readonly int moveHash = Animator.StringToHash("MOVE");
        private readonly int attackHash = Animator.StringToHash("ATTACK");
        private readonly int deadHash = Animator.StringToHash("DEAD");
        
        protected override void Awake()
        {
            base.Awake();
            _entityMover = GetCompo<EntityMover>();
            _entityAttackCompo = GetCompo<EntityAttackCompo>(true);
            _rbCompo = GetComponent<Rigidbody2D>();
            _entityHealth = GetCompo<EntityHealth>();
            _entityAnimator = GetCompo<EntityAnimator>();
            OnHitEvent.AddListener(OnHit);
            OnDeadEvent.AddListener(OnDead);
        }

        private void OnDestroy()
        {
            OnHitEvent.RemoveListener(OnHit);
            OnDeadEvent.RemoveListener(OnDead);
        }

        protected virtual void Update()
        {
            if (IsDead) return;
            if (_entityAttackCompo.IsInRange(_target))
            {
                _entityMover.Stop();
                if (_entityAttackCompo.CanAttack(_target))
                {
                    ChangeAnimation(attackHash);
                    _entityAttackCompo.Attack(_target);
                }
                // else if(_currentAnimationHash != idleHash) ChangeAnimation(idleHash);
            }
            else
            {
                if(_currentAnimationHash != moveHash) ChangeAnimation(moveHash);
                _entityMover.Move(Vector2.left);
            }
        }

        private void ChangeAnimation(int newHash)
        {
             _entityAnimator.SetParam(_currentAnimationHash, false);
             _currentAnimationHash = newHash;
             _entityAnimator.SetParam(_currentAnimationHash, true);
        }
        
        private void OnHit()
        {
            
        }

        private void OnDead()
        {
            _entityAnimator.SetParam(deadHash, true);
        }
    }
}