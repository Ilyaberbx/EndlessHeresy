using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Extensions;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Components;
using EndlessHeresy.Gameplay.Effects;
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
        private TrailsComponent _trailsComponent;

        private int _force;
        private int _collisionForce;
        private TrailData _trailData;
        private float _trailsRatio;

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
            _trailsComponent = Owner.GetComponent<TrailsComponent>();
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
            var spawnTrailsCts = CancellationTokenSource.CreateLinkedTokenSource(token);
            ShowTrailsLoopAsync(spawnTrailsCts.Token).Forget();
            await ExecuteDashAsync(ownerToMouseDirection);
            spawnTrailsCts.Cancel();
            PostDash();
        }

        public void SetForce(int force) => _force = force;
        public void SetCollisionForce(int force) => _collisionForce = force;
        public void SetTrailData(TrailData trailData) => _trailData = trailData;
        public void SetTrailsRatio(float trailsRatio) => _trailsRatio = trailsRatio;

        private Vector2 GetOwnerToMouseDirection()
        {
            var camera = _cameraService.MainCamera;
            var mouseWorldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            return Owner.Transform.position.DirectionTo(mouseWorldPosition).ToVector2();
        }

        private Task<bool> ExecuteDashAsync(Vector2 ownerToMouseDirection)
        {
            var forceDirection = ownerToMouseDirection.normalized;
            var processedForce = forceDirection * _force;
            Rigidbody.AddForce(processedForce, ForceMode2D.Impulse);
            _gameUpdateService.OnUpdate += OnDashUpdate;
            return _forceCompletionSource.Task;
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

        private async Task ShowTrailsLoopAsync(CancellationToken cancellationToken)
        {
            var delay = (int)(_trailsRatio * 1000);

            while (true)
            {
                await Task.Yield();

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var delayTask = Task.Delay(delay, cancellationToken);
                var timeOutTask = Task.Delay(Timeout.Infinite, cancellationToken);
                await Task.WhenAny(delayTask, timeOutTask);
                var showTrailTask = _trailsComponent.ShowTrailsAsync(_trailData);
                showTrailTask.Forget();
            }
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

        private void OnDashUpdate(float deltaTime)
        {
            var dashStopped = Rigidbody.velocity.magnitude <= StopDashThreshold;

            if (dashStopped)
            {
                _forceCompletionSource.TrySetResult(true);
            }
        }
    }
}