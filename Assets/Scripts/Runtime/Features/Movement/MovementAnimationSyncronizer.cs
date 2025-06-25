using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Generic;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.Movement
{
    public sealed class MovementAnimationSyncronizer : PocoComponent
    {
        private const string IdleKey = "Idle";
        private const string RunningKey = "Run";
        private MovementComponent _movement;
        private AnimatorStorageComponent _animatorStorage;
        private bool _isLocked;

        private Animator Animator => _animatorStorage.Animator;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _movement = Owner.GetComponent<MovementComponent>();
            _animatorStorage = Owner.GetComponent<AnimatorStorageComponent>();
            _movement.MovementProperty.Subscribe(OnMovementChanged).AddTo(CompositeDisposable);
            _movement.IsLockedProperty.Subscribe(OnIsLockedChanged).AddTo(CompositeDisposable);
            return Task.CompletedTask;
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        private void OnIsLockedChanged(bool value)
        {
            if (value)
            {
                return;
            }

            OnMovementChanged(_movement.MovementProperty.Value);
        }

        private void OnMovementChanged(Vector2 value)
        {
            if (_isLocked)
            {
                return;
            }

            if (value == Vector2.zero)
            {
                if (Animator.GetCurrentAnimatorStateInfo(0).IsName(IdleKey))
                {
                    return;
                }

                Animator.Play(IdleKey);
                return;
            }

            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(RunningKey))
            {
                return;
            }

            Animator.Play(RunningKey);
        }
    }
}