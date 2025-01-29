using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors.CrescentKnife;
using EndlessHeresy.Gameplay.Extensions;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Movement.Rotate;
using EndlessHeresy.Gameplay.Services.Factory;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities.CrescentStrike
{
    public sealed class CrescentStrike : Ability
    {
        private int _damage;
        private float _knifeOffset;
        private float _duration;
        private AnimationCurve _curve;

        private CrescentKnifeActor _crescentKnifeActor;
        private GameFactoryService _gameFactoryService;
        private RotationAroundStorage _rotationStorage;
        private RotateAroundComponent _rotateAround;

        public void Configure(int damage, float knifeOffset, float duration, AnimationCurve curve)
        {
            _damage = damage;
            _knifeOffset = knifeOffset;
            _duration = duration;
            _curve = curve;
        }

        public override async Task InitializeAsync(IActor owner)
        {
            await base.InitializeAsync(owner);

            _gameFactoryService = ServiceLocator.Get<GameFactoryService>();
            _crescentKnifeActor = await CreateCrescentKnifeAsync();
            _crescentKnifeActor.Hide();

            owner.TryGetComponent(out _rotateAround);
            owner.TryGetComponent(out _rotationStorage);
        }

        public override void Dispose()
        {
            base.Dispose();
            _crescentKnifeActor.OnHit -= OnHitByKnife;
            _crescentKnifeActor.Dispose();
        }

        protected override async Task CastAsync(IActor owner)
        {
            if (!IsInitialized())
            {
                return;
            }


            var previousParent = _crescentKnifeActor.GameObject.transform.parent;
            var origin = _rotationStorage.Origin;

            _crescentKnifeActor.SetParent(origin);
            _crescentKnifeActor.Show();
            _crescentKnifeActor.OnHit += OnHitByKnife;
            SetStatus(AbilityStatus.InUse);
            await _rotateAround.RotateAsync(_rotationStorage.Origin, 360f, _duration, _curve);
            SetStatus(AbilityStatus.Ready);
            _crescentKnifeActor.OnHit -= OnHitByKnife;
            _crescentKnifeActor.Hide();
            _crescentKnifeActor.SetParent(previousParent);
        }

        private bool IsInitialized() =>
            _crescentKnifeActor != null &&
            _rotationStorage != null &&
            _rotateAround != null;

        private void OnHitByKnife(HealthComponent healthComponent) => healthComponent.TakeDamage(_damage);

        private Task<CrescentKnifeActor> CreateCrescentKnifeAsync()
        {
            var ownerPosition = Owner.Transform.position;
            var at = ownerPosition.AddY(_knifeOffset);
            return _gameFactoryService.CreateCrescentKnifeAsync(at, Owner.Transform);
        }
    }
}