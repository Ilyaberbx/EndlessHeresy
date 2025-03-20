using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Facing;
using EndlessHeresy.Gameplay.Services.Camera;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Gameplay.Tags;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities
{
    public sealed class DashAbility : AbilityWithCooldown
    {
        private const float StopDashThreshold = 2f;

        private ICameraService _cameraService;
        private IGameUpdateService _gameUpdateService;

        private FacingComponent _facingComponent;
        private RigidbodyStorageComponent _rigidbodyStorage;
        private TaskCompletionSource<bool> _forceCompletionSource;
        private EnemyTriggerObserver _enemyTriggerObserver;

        private int _force;
        private int _collisionForce;

        private Rigidbody2D Rigidbody => _rigidbodyStorage.Rigidbody;

        [Inject]
        public void Construct(ICameraService cameraService, IGameUpdateService gameUpdateService)
        {
            _cameraService = cameraService;
            _gameUpdateService = gameUpdateService;
        }

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _facingComponent = Owner.GetComponent<FacingComponent>();
            _rigidbodyStorage = Owner.GetComponent<RigidbodyStorageComponent>();
            _enemyTriggerObserver = Owner.GetComponent<EnemyTriggerObserver>();
        }

        public override void Dispose()
        {
            base.Dispose();
            _forceCompletionSource?.TrySetResult(true);
            _gameUpdateService.OnUpdate -= OnDashUpdate;
        }

        public override async Task UseAsync(CancellationToken token)
        {
            var ownerToMouseDirection = GetOwnerToMouseDirection();
            _forceCompletionSource = new TaskCompletionSource<bool>();
            PreDash(ownerToMouseDirection);
            await ExecuteAsync(ownerToMouseDirection);
            PostDash();
        }

        public void SetForce(int force) => _force = force;
        public void SetCollisionForce(int force) => _collisionForce = force;

        private Vector2 GetOwnerToMouseDirection()
        {
            var camera = _cameraService.MainCamera;
            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            return Owner.Transform.position.DirectionTo(mouseWorldPosition).ToVector2();
        }

        private Task<bool> ExecuteAsync(Vector2 ownerToMouseDirection)
        {
            var forceDirection = ownerToMouseDirection.normalized;
            var processedForce = forceDirection * _force;
            Rigidbody.AddForce(processedForce, ForceMode2D.Impulse);
            _gameUpdateService.OnUpdate += OnDashUpdate;
            return _forceCompletionSource.Task;
        }

        private void OnDashUpdate(float deltaTime)
        {
            var dashStopped = Rigidbody.velocity.magnitude <= StopDashThreshold;

            if (dashStopped)
            {
                _forceCompletionSource.TrySetResult(true);
            }
        }

        private void PreDash(Vector2 ownerToMouseDirection)
        {
            _facingComponent.Face(ownerToMouseDirection.x > 0);
            _enemyTriggerObserver.OnTriggerEnter += OnEnemyTriggerEnter;
            SetState(AbilityState.InUse);
        }

        private void PostDash()
        {
            _gameUpdateService.OnUpdate -= OnDashUpdate;
            _enemyTriggerObserver.OnTriggerEnter -= OnEnemyTriggerEnter;
            SetState(AbilityState.Cooldown);
        }

        private void OnEnemyTriggerEnter(EnemyTagComponent enemyTag)
        {
            if (!enemyTag.Owner.TryGetComponent(out RigidbodyStorageComponent rigidbodyStorage))
            {
                return;
            }

            var enemyPosition = enemyTag.transform.position;
            var ownerPosition = Owner.Transform.position;
            var ownerToEnemyDirection = ownerPosition
                .DirectionTo(enemyPosition)
                .normalized
                .ToVector2();

            var rigidbody = rigidbodyStorage.Rigidbody;
            var processedForce = ownerToEnemyDirection * _collisionForce;
            rigidbody.AddForce(processedForce, ForceMode2D.Impulse);
        }
    }
}