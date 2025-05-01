using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.StatusEffects;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Services.Tick;
using EndlessHeresy.Gameplay.Stats;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using VContainer;

namespace EndlessHeresy.Gameplay.StatusEffects
{
    public sealed class StatusEffectsComponent : PocoComponent, IStatusEffects
    {
        private IGameplayStaticDataService _gameStaticDataService;
        private IGameUpdateService _gameUpdatesService;
        private StatsComponent _stats;

        private ReactiveProperty<Locator<StatusEffectType, IStatusEffect>> _activeEffectsProperty;

        public ReadOnlyReactiveProperty<Locator<StatusEffectType, IStatusEffect>> ActiveStatusEffects
        {
            get;
            private set;
        }

        [Inject]
        public void Construct(IGameUpdateService gameUpdateService,
            IGameplayStaticDataService gameplayStaticDataService)
        {
            _gameStaticDataService = gameplayStaticDataService;
            _gameUpdatesService = gameUpdateService;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stats = Owner.GetComponent<StatsComponent>();
            InitializeEffectsProperty();
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

            foreach (var activeEffect in _activeEffectsProperty.Value.GetElements())
            {
                activeEffect.Remove(_stats);
            }

            _gameUpdatesService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            foreach (var activeEffect in _activeEffectsProperty.Value.GetElements())
            {
                if (activeEffect.TryGet(out IUpdatableStatusEffect statusEffect))
                {
                    statusEffect.Update(Owner);
                }
            }
        }

        public void Add(StatusEffectType identifier)
        {
            if (!TryGetEffectData(identifier, out var data))
            {
                return;
            }

            if (!_activeEffectsProperty.Value.TryGet(identifier, out var exisingStatusEffect))
            {
                var newStatusEffect = data.GetStatusEffect();
                newStatusEffect.Apply(_stats);
                _activeEffectsProperty.Value.Add(identifier, newStatusEffect);
                _activeEffectsProperty.SetDirty();
                return;
            }

            if (exisingStatusEffect.TryGet<StackableStatusEffect>(out var stackableStatusEffect))
            {
                stackableStatusEffect.Apply(_stats);
                _activeEffectsProperty.SetDirty();
                return;
            }

            if (exisingStatusEffect.TryGet<TemporaryStatusEffect>(out var temporaryStatusEffect))
            {
                temporaryStatusEffect.Reset();
                _activeEffectsProperty.SetDirty();
            }
        }

        public void Remove(StatusEffectType identifier)
        {
            if (!_activeEffectsProperty.Value.TryGet(identifier, out var exisingStatusEffect))
            {
                return;
            }

            exisingStatusEffect.Remove(_stats);
            _activeEffectsProperty.Value.Remove(identifier);
            _activeEffectsProperty.SetDirty();
        }

        private void InitializeEffectsProperty()
        {
            var defaultLocator = new Locator<StatusEffectType, IStatusEffect>();
            _activeEffectsProperty =
                new ReactiveProperty<Locator<StatusEffectType, IStatusEffect>>(defaultLocator);
            ActiveStatusEffects =
                new ReadOnlyReactiveProperty<Locator<StatusEffectType, IStatusEffect>>(_activeEffectsProperty);
        }

        private bool TryGetEffectData(StatusEffectType identifier, out StatusEffectConfiguration data)
        {
            data = _gameStaticDataService.GetStatusEffectConfiguration(identifier);
            return data != null;
        }
    }
}