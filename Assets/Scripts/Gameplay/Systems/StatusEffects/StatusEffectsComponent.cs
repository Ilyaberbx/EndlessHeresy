using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Gameplay.Stats;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public sealed class StatusEffectsComponent : PocoComponent, IStatusEffects
    {
        public Action<IStatusEffect> OnStatusEffectAdded { get; set; }
        public Action<IStatusEffect> OnStatusEffectRemoved { get; set; }

        private IGameUpdateService _gameUpdatesService;
        private IList<IStatusEffect> _activeEffects;
        private StatsComponent _stats;

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService) => _gameUpdatesService = gameUpdateService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stats = Owner.GetComponent<StatsComponent>();
            _activeEffects = new List<IStatusEffect>();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameUpdatesService.OnUpdate += OnUpdate;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var activeEffect in _activeEffects)
            {
                if (activeEffect is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _gameUpdatesService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            foreach (var activeEffect in _activeEffects.ToArray())
            {
                if (activeEffect is IUpdatableStatusEffect updatableStatusEffect)
                {
                    updatableStatusEffect.Update(Owner);
                }
            }
        }

        public void Add(IStatusEffect effect)
        {
            var hasEffect = _activeEffects.Contains(effect);

            if (hasEffect || effect == null)
                return;

            if (effect is IInitializable initializable)
            {
                initializable.Initialize();
            }

            _activeEffects.Add(effect);
            effect.Apply(_stats);
            OnStatusEffectAdded?.Invoke(effect);
        }

        public void Remove(IStatusEffect effect)
        {
            var isRemoved = _activeEffects.Remove(effect);

            if (!isRemoved)
                return;

            effect.Remove(_stats);
            OnStatusEffectRemoved?.Invoke(effect);
        }
    }
}