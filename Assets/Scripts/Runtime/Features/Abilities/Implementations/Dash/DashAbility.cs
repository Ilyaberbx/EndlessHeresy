using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Extensions;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Camera;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Supporting.Tags;
using EndlessHeresy.Runtime.Tick;
using EndlessHeresy.Runtime.Vfx;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class DashAbility : AbilityWithCooldown
    {
        private const float StopDashThreshold = 2f;

        private ICameraService _cameraService;
        private IGameUpdateService _gameUpdateService;

        private RigidbodyStorageComponent _rigidbodyStorage;
        private TaskCompletionSource<bool> _forceCompletionSource;
        private EnemyTriggerObserver _enemyTriggerObserver;
        private TrailsSpawnerComponent _trailsSpawnerComponent;
        private SpriteRendererComponent _spriteRendererComponent;
        private MouseFacingComponent _mouseFacingComponent;

        private int _force;
        private int _collisionForce;
        private float _trailsRatio;
        private TrailData _trailData;

        private Rigidbody2D Rigidbody => _rigidbodyStorage.Rigidbody;
        private FacingComponent FacingComponent => _mouseFacingComponent.FacingComponent;

        [Inject]
        public void Construct(ICameraService cameraService, IGameUpdateService gameUpdateService)
        {
            _cameraService = cameraService;
            _gameUpdateService = gameUpdateService;
        }

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            _rigidbodyStorage = Owner.GetComponent<RigidbodyStorageComponent>();
            _enemyTriggerObserver = Owner.GetComponent<EnemyTriggerObserver>();
            _trailsSpawnerComponent = Owner.GetComponent<TrailsSpawnerComponent>();
            _spriteRendererComponent = Owner.GetComponent<SpriteRendererComponent>();
            _mouseFacingComponent = Owner.GetComponent<MouseFacingComponent>();
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
            _forceCompletionSource = new TaskCompletionSource<bool>(token);
            PreDash();
            var showTrailsCts = CancellationTokenSource.CreateLinkedTokenSource(token);
            ShowTrailsLoopAsync(showTrailsCts.Token).Forget();
            await ExecuteDashAsync(ownerToMouseDirection);
            showTrailsCts.Cancel();
            PostDash();
        }

        public void SetForce(int force) => _force = force;
        public void SetCollisionForce(int force) => _collisionForce = force;
        public void SetTrailData(TrailData trailData) => _trailData = trailData;
        public void SetTrailsRatio(float trailsRatio) => _trailsRatio = trailsRatio;

        private Vector2 GetOwnerToMouseDirection()
        {
            var camera = _cameraService.MainCamera;
            var mouseWorldPosition = camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
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

        private void PreDash()
        {
            _mouseFacingComponent.UpdateFacing();
            FacingComponent.Lock(GetType());
            _enemyTriggerObserver.OnTriggerEnter += OnEnemyTriggerEnter;
            SetState(AbilityState.InUse);
        }

        private void PostDash()
        {
            FacingComponent.Unlock(GetType());
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
                var spawnTrailsDto = CollectSpawnDto();
                var spawnTrailTask = _trailsSpawnerComponent.SpawnTrailsAsync(spawnTrailsDto);
                spawnTrailTask.Forget();
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

        private SpawnTrailQuery CollectSpawnDto()
        {
            return new SpawnTrailQuery(_trailData.LifeTime,
                _trailData.Color,
                Owner.Transform.position,
                _spriteRendererComponent.SpriteRenderer.sprite,
                _trailData.Name);
        }
    }
}