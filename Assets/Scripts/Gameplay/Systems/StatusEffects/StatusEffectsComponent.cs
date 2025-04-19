using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Gameplay.Stats;
using VContainer;

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
            _gameUpdatesService.OnUpdate -= OnUpdate;
        }

        public void Add(IStatusEffect effect)
        {
            var hasEffect = _activeEffects.Contains(effect);

            if (hasEffect)
                return;

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
    }
}