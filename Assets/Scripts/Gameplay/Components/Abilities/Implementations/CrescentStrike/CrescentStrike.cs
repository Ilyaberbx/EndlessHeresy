using System;
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
        private CrescentKnifeActor _crescentKnifeActor;
        private GameFactoryService _gameFactoryService;

        public void Configure(int damage, float knifeOffset)
        {
            _damage = damage;
            _knifeOffset = knifeOffset;
        }

        public override async void Initialize(IActor owner)
        {
            base.Initialize(owner);

            _gameFactoryService = ServiceLocator.Get<GameFactoryService>();
            _crescentKnifeActor = await CreateCrescentKnifeAsync(owner);
        }

        public override void Dispose()
        {
            base.Dispose();
            _crescentKnifeActor.OnHit -= OnHitByKnife;
            _crescentKnifeActor.Dispose();
        }

        protected override async Task CastAsync(IActor owner)
        {
            if (_crescentKnifeActor == null)
            {
                return;
            }

            owner.TryGetComponent(out RotateAroundComponent rotateAround);
            owner.TryGetComponent(out RotationAroundStorage rotateAroundStorage);

            _crescentKnifeActor.Clear();
            var knifeTransform = _crescentKnifeActor.transform;
            var around = rotateAroundStorage.Around;
            knifeTransform.SetParent(around);
            knifeTransform.localPosition = Vector2.zero + Vector2.up * 2f;
            _crescentKnifeActor.OnHit += OnHitByKnife;
            SetStatus(AbilityStatus.InUse);
            await rotateAround.RotateAsync(around, 360f);
            SetStatus(AbilityStatus.Ready);
            _crescentKnifeActor.OnHit -= OnHitByKnife;
        }

        private void OnHitByKnife(HealthComponent healthComponent) => healthComponent.TakeDamage(_damage);

        private Task<CrescentKnifeActor> CreateCrescentKnifeAsync(IActor owner)
        {
            var ownerForwardDirection = owner.Transform.forward;
            var at = ownerForwardDirection.AddY(_knifeOffset);
            return _gameFactoryService.CreateCrescentKnifeAsync(at, owner.Transform);
        }
    }
}