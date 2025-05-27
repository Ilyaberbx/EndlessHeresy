using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.StatusEffects;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UniRx;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class StatusEffectsComponent : PocoComponent, IStatusEffectsReadOnly
    {
        private IGameplayStaticDataService _gameStaticDataService;
        private IObjectResolver _resolver;
        private StatsContainer _stats;

        private ReactiveProperty<Locator<StatusEffectType, IStatusEffectRoot>> _activeEffectsProperty;

        public IReadOnlyReactiveProperty<Locator<StatusEffectType, IStatusEffectRoot>> ActiveStatusEffects
        {
            get;
            private set;
        }

        [Inject]
        public void Construct(IGameplayStaticDataService gameplayStaticDataService, IObjectResolver resolver)
        {
            _gameStaticDataService = gameplayStaticDataService;
            _resolver = resolver;
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _stats = Owner.GetComponent<StatsContainer>();
            InitializeEffectsProperty();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var activeEffect in _activeEffectsProperty.Value.GetElements())
            {
                activeEffect.Remove(_stats);
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
                var builder = new StatusEffectsBuilder();
                data.ConfigureBuilder(builder);
                var newStatusEffect = builder.Build(Owner, _resolver);
                newStatusEffect.Apply(_stats);
                _activeEffectsProperty.Value.Add(identifier, newStatusEffect);
                _activeEffectsProperty.SetDirty();
                return;
            }

            if (exisingStatusEffect.TryGet<StackableStatusEffectComponent>(out var stackableStatusEffect))
            {
                stackableStatusEffect.Apply(_stats);
                _activeEffectsProperty.SetDirty();
            }

            if (exisingStatusEffect.TryGet<TemporaryStatusEffectComponent>(out var temporaryStatusEffect))
            {
                temporaryStatusEffect.Reset();
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
            var defaultLocator = new Locator<StatusEffectType, IStatusEffectRoot>();
            _activeEffectsProperty =
                new ReactiveProperty<Locator<StatusEffectType, IStatusEffectRoot>>(defaultLocator);
            ActiveStatusEffects =
                new ReadOnlyReactiveProperty<Locator<StatusEffectType, IStatusEffectRoot>>(_activeEffectsProperty);
        }

        private bool TryGetEffectData(StatusEffectType identifier, out StatusEffectConfiguration data)
        {
            data = _gameStaticDataService.GetStatusEffectConfiguration(identifier);
            return data != null;
        }
    }
}